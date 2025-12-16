using System;

namespace ClumsySanta.Redist.Physic
{
    public class Pendulum
    {
            private double _pendulumMass = 1;
            private double _pendulumLength = 1;
            private double _dampingCoefficient = 0.6;
            private double _theta0 = 45;
            private double _alpha0 = 0;
            private double[] _altha0Theta0;
            private double _time = 0;
            private double _dt = 0.03;

            public bool Ticks { get; private set; }
            public double Time { get { return _time;  } }

            public double Dt { get { return _dt; } }

            public Pendulum()
            {
                Ticks = true;

                _theta0 = Math.PI * _theta0 / 180;
                _alpha0 = Math.PI * _alpha0 / 180;
                _altha0Theta0 = new double[2] { _theta0, _alpha0 };
            }

            public Position GetPosition()
            {
                if (!Ticks)
                    return new Position(0, 0);

                var functions = new ODESolver.Function[] { f1, f2 };

                double[] result = ODESolver.RungeKutta4(functions, _altha0Theta0, _time, _dt);

                if (_time > 0 && Math.Abs(result[0]) < 0.01 && Math.Abs(result[1]) < 0.001)
                {
                    Ticks = false;
                    return new Position(0, 0);
                }

                _time += _dt;

                var x = Math.Sin(result[0]); var y = Math.Cos(result[0]);

                return new Position(x, y);
            }

            private void StartAnimation(object sender, EventArgs e)
            {
                // Invoke ODE solver:
                var f = new ODESolver.Function[] { f1, f2 };

                double[] result = ODESolver.RungeKutta4(f, _altha0Theta0, _time, _dt);

                // Display moving pendulum on screen:
                var x = Math.Sin(result[0]);
                var y = Math.Cos(result[0]);

                // Reset the initial values for next calculation:
                _altha0Theta0 = result;
                _time += _dt;
            }

            private double f1(double[] xx, double t)
            {
                return xx[1];
            }

            private double f2(double[] xx, double t)
            {
                double m = _pendulumMass;
                double L = _pendulumLength;
                double g = 9.81;
                double b = _dampingCoefficient;
                return -g * Math.Sin(xx[0]) / L - b * xx[1] / m;
            }
    }
}
