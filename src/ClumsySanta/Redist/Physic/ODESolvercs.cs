namespace ClumsySanta.Redist.Physic
{
    public class ODESolver
    {
        public static double[] RungeKutta4(Function[] f, double[] x0, double t0, double dt)
        {
            int length = x0.Length;
            double[] numArray1 = new double[length];
            double[] numArray2 = new double[length];
            double[] numArray3 = new double[length];
            double[] numArray4 = new double[length];
            double t = t0;
            double[] x1 = new double[length];
            double[] x2 = x0;
            for (int index = 0; index < length; ++index)
                numArray1[index] = dt * f[index](x2, t);
            for (int index = 0; index < length; ++index)
                x1[index] = x2[index] + numArray1[index] / 2.0;
            for (int index = 0; index < length; ++index)
                numArray2[index] = dt * f[index](x1, t + dt / 2.0);
            for (int index = 0; index < length; ++index)
                x1[index] = x2[index] + numArray2[index] / 2.0;
            for (int index = 0; index < length; ++index)
                numArray3[index] = dt * f[index](x1, t + dt / 2.0);
            for (int index = 0; index < length; ++index)
                x1[index] = x2[index] + numArray3[index];
            for (int index = 0; index < length; ++index)
                numArray4[index] = dt * f[index](x1, t + dt);
            for (int index = 0; index < length; ++index)
                x2[index] += (numArray1[index] + 2.0 * numArray2[index] + 2.0 * numArray3[index] + numArray4[index]) / 6.0;
            return x2;
        }

        public delegate double Function(double[] x, double t);
    }
}
