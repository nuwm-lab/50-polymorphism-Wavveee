using System;

namespace Geometry
{
    class Line
    {
        protected double _a, _b, _c;
        protected const double EPS = 1e-9;

        public Line(double a, double b, double c)
        {
            SetCoefficients(a, b, c);
        }

        public virtual void SetCoefficients(double a, double b, double c)
        {
            if (a == 0 && b == 0)
                throw new ArgumentException("Коефіцієнти A і B не можуть бути одночасно нульовими.");

            _a = a;
            _b = b;
            _c = c;
        }

        public virtual void PrintCoefficients()
        {
            Console.WriteLine($"Line: {_a}*x + {_b}*y + {_c} = 0");
        }

        public virtual bool Belongs(double[] point)
        {
            if (point.Length != 2)
                throw new ArgumentException("Для прямої потрібно передати 2 координати (x, y).");

            double x = point[0];
            double y = point[1];

            return Math.Abs(_a * x + _b * y + _c) < EPS;
        }
    }

    class HyperPlane : Line
    {
        protected double _d, _e;

        public HyperPlane(double a, double b, double c, double d, double e)
            : base(a, b, c)
        {
            _d = d;
            _e = e;
        }

        public void SetCoefficients(double a, double b, double c, double d, double e)
        {
            base.SetCoefficients(a, b, c);
            _d = d;
            _e = e;
        }

        public override void PrintCoefficients()
        {
            Console.WriteLine($"HyperPlane: {_a}*x1 + {_b}*x2 + {_c}*x3 + {_d}*x4 + {_e} = 0");
        }

        public override bool Belongs(double[] point)
        {
            if (point.Length != 4)
                throw new ArgumentException("Для гіперплощини потрібно 4 координати (x1, x2, x3, x4).");

            double x1 = point[0], x2 = point[1], x3 = point[2], x4 = point[3];

            return Math.Abs(_a * x1 + _b * x2 + _c * x3 + _d * x4 + _e) < EPS;
        }
    }

    class Program
    {
        static double ReadDouble(string text)
        {
            double value;
            Console.Write(text);
            while (!double.TryParse(Console.ReadLine(), out value))
                Console.Write("Помилка. Спробуйте ще раз: ");
            return value;
        }

        static void Main()
        {
            Console.Write("Оберіть режим (1 - Line, 2 - HyperPlane): ");
            char mode = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (mode == '1')
            {
                double a = ReadDouble("A = ");
                double b = ReadDouble("B = ");
                double c = ReadDouble("C = ");

                Line line = new Line(a, b, c);
                line.PrintCoefficients();

                double x = ReadDouble("x = ");
                double y = ReadDouble("y = ");

                Console.WriteLine($"Точка належить прямій: {line.Belongs(new[] { x, y })}");
            }
            else
            {
                double a = ReadDouble("A = ");
                double b = ReadDouble("B = ");
                double c = ReadDouble("C = ");
                double d = ReadDouble("D = ");
                double e = ReadDouble("E = ");

                HyperPlane hp = new HyperPlane(a, b, c, d, e);
                hp.PrintCoefficients();

                double x1 = ReadDouble("x1 = ");
                double x2 = ReadDouble("x2 = ");
                double x3 = ReadDouble("x3 = ");
                double x4 = ReadDouble("x4 = ");

                Console.WriteLine($"Точка належить гіперплощині: {hp.Belongs(new[] { x1, x2, x3, x4 })}");
            }

            Console.WriteLine("\n--- Кінець роботи програми ---");
        }
    }
}
