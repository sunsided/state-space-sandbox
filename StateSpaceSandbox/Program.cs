using System;
using System.Diagnostics;
using StateSpaceSandbox.Model;
using StateSpaceSandbox.ModelImplementation;

namespace StateSpaceSandbox
{
    class Program
    {
        public static void Main(string[] args)
        {
            IStateMatrix A = new StateMatrix(2, 2);
            A[0, 0] = 0;
            A[0, 1] = 1;
            A[1, 0] = 0;
            A[1, 0] = 0;

            IInputMatrix B = new InputMatrix(2, 1);
            B[0, 0] = 0;
            B[1, 0] = 1;

            IOutputMatrix C = new OutputMatrix(2, 2);
            C[0, 0] = 1;
            C[0, 1] = 0;
            C[1, 0] = 0;
            C[1, 1] = 1;

            IFeedthroughMatrix D = new FeedthroughMatrix(2, 1);
            D[0, 0] = 0;
            D[1, 0] = 0;

            IControlVector u = new ControlVector(1);
            u[0] = 1;

            IStateVector x = new StateVector(2);

            Stopwatch watch = Stopwatch.StartNew();
            int steps = 0;

            IStateVector dx = new StateVector(x.Length);
            IStateVector dxu = new StateVector(x.Length);
            IOutputVector y = new OutputVector(C.Rows);
            IOutputVector yu = new OutputVector(C.Rows);
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
        }
    }
}
