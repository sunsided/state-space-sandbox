using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using StateSpaceSandbox.Compiler;
using StateSpaceSandbox.Model;
using StateSpaceSandbox.ModelImplementation;
using StateSpaceSandbox.Random;

namespace StateSpaceSandbox
{
    class Program
    {
        public static void Main(string[] args)
        {
            /*
            XorShift160 lol = new XorShift160();
            double runningSum = 0;

            while (!Console.KeyAvailable)
            {
                Console.WriteLine(lol.NextDouble());
            }
            return;
            */

            IStateMatrix A = new StateMatrix(2, 2);
            A.SetValue(0, 0, 0);
            A.SetValue(0, 1, 1);
            A.SetValue(1, 0, 0);
            A.SetValue(1, 0, 0);

            /*
            var c = new CompiledMatrix<IStateMatrix>(A);
            c.Compile();
             */

            IInputMatrix B = new InputMatrix(2, 1);
            B.SetValue(0, 0, 0);
            B.SetValue(1, 0, 1);

            IOutputMatrix C = new OutputMatrix(2, 2);
            C.SetValue(0, 0, 1);
            C.SetValue(0, 1, 0);
            C.SetValue(1, 0, 0);
            C.SetValue(1, 1, 1);

            IFeedthroughMatrix D = new FeedthroughMatrix(2, 1);
            D.SetValue(0, 0, 0);
            D.SetValue(1, 0, 0);

            IControlVector u = new ControlVector(1);
            u.SetValue(0, 1);

            IStateVector x = new StateVector(2);
            
            /*
            IStateVector dx = new StateVector(x.Length);
            IStateVector dxu = new StateVector(x.Length);
            IOutputVector y = new OutputVector(C.Rows);
            IOutputVector yu = new OutputVector(C.Rows);
            */

            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken ct = cts.Token;

            Semaphore startCalculation = new Semaphore(0, 2);
            Semaphore calculationDone = new Semaphore(0, 2);

            SimulationTime simulationTime = new SimulationTime(); 

            IStateVector dx = new StateVector(x.Length);
            Task inputToState = new Task(() =>
                                             {
                                                 IStateVector dxu = new StateVector(x.Length);
                                                 while (!ct.IsCancellationRequested)
                                                 {
                                                     startCalculation.WaitOne();
                                                     Thread.MemoryBarrier();

                                                     A.Transform(simulationTime, x, ref dx);
                                                     B.Transform(simulationTime, u, ref dxu); // TODO: TransformAndAdd()      
                                                     dx.AddInPlace(simulationTime, dxu);

                                                     Thread.MemoryBarrier();
                                                     calculationDone.Release();
                                                 }
                                             });

            IOutputVector y = new OutputVector(C.Rows);
            Task stateToOutput = new Task(() =>
                                              {
                                                  IOutputVector yu = new OutputVector(C.Rows);
                                                  while (!ct.IsCancellationRequested)
                                                  {
                                                      startCalculation.WaitOne();
                                                      Thread.MemoryBarrier();

                                                      C.Transform(simulationTime, x, ref y);
                                                      D.Transform(simulationTime, u, ref yu); // TODO: TransformAndAdd()
                                                      y.AddInPlace(simulationTime, yu);

                                                      Thread.MemoryBarrier();
                                                      calculationDone.Release();
                                                  }
                                              });

            Task control = new Task(() =>
                                        {
                                            Stopwatch watch = Stopwatch.StartNew();
                                            int steps = 0;

                                            while (!ct.IsCancellationRequested)
                                            {
                                                // wait for a new u to be applied
                                                // TODO: apply control vector
                                                startCalculation.Release(2);

                                                // wait for y
                                                calculationDone.WaitOne();
                                                calculationDone.WaitOne();
                                                Thread.MemoryBarrier();

                                                // wait for state vector to be changeable
                                                // TODO: perform real transformation
                                                x.AddInPlace(simulationTime, dx); // discrete integration, T=1
                                                
                                                // video killed the radio star
                                                if (steps % 1000 == 0)
                                                {
                                                    var localY = y;
                                                    double thingy = steps/watch.Elapsed.TotalSeconds;
                                                    Trace.WriteLine(simulationTime.Time + " Position: " + localY.GetValue(0, simulationTime) + ", Velocity: " + localY.GetValue(1, simulationTime) + ", Acceleration: " + u.GetValue(0, simulationTime) + ", throughput: " + thingy);
                                                }

                                                // cancel out acceleration
                                                if (steps++ == 10)
                                                {
                                                    u.SetValue(0, 0);
                                                }

                                                // advance simulation time
                                                simulationTime.Add(TimeSpan.FromSeconds(1));
                                            }
                                        });
            
            inputToState.Start();
            stateToOutput.Start();
            control.Start();
            
            Console.ReadKey(true);
            cts.Cancel();

            /*
            while (!Console.KeyAvailable)
            {
                A.Transform(x, ref dx);
                B.Transform(u, ref dxu); // TODO: TransformAndAdd()
                dx.AddInPlace(dxu);

                // TODO: perform transformation
                x.AddInPlace(dx); // discrete integration, T=1

                C.Transform(x, ref y);
                D.Transform(u, ref yu); // TODO: TransformAndAdd()
                y.AddInPlace(yu);

                // video killed the radio star
                if (steps%1000 == 0)
                {
                    Trace.WriteLine("Position: " + y[0] + ", Velocity: " + y[1] + ", Acceleration: " + u[0] + ", throughput: " + steps/watch.Elapsed.TotalSeconds);
                }

                // cancel out acceleration
                if (steps++ == 10)
                {
                    u[0] = 0;
                }
            }
            */
        }
    }
}
