using System;

namespace Geometry
{
    class Line
    {
        private double _a, _b, _c;

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

        // Поліморфний метод — приймає точку як масив
        public virtual bool Belongs(double[] point)
        {
            if (point.Length != 2)
                throw new ArgumentException("Для прямої необхідно передати 2 координати (x, y).");

            double x = point[0];
            double y = point[1];

            return Math.Abs(_a * x + _b * y + _c) < 1e-9;
        }
    }

    class HyperPlane : Line
    {
        private double _d, _e;

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
            Console.WriteLine($"HyperPlane: a*x1 + b*x2 + c*x3 + d*x4 + e = 0");
        }

        public override bool Belongs(double[] point)
        {
            if (point.Length != 4)
                throw new ArgumentException("Для гіперплощини необхідно 4 координати (x1, x2, x3, x4).");

            double x1 = point[0], x2 = point[1], x3 = point[2], x4 = point[3];

            return Math.Abs(
                x1 * GetA() + x2 * GetB() + x3 * GetC() + x4 * _d + _e
            ) < 1e-9;
        }

        // Доступ до батьківських полів (щоб не робити їх protected)
        private double GetA() => typeof(Line)
            .GetField("_a", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(this) as double? ?? 0;

        private double GetB() => typeof(Line)
            .GetField("_b", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(this) as double? ?? 0;

        private double GetC() => typeof(Line)
            .GetField("_c", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(this) as double? ?? 0;
    }

    class Program
    {
        static void Main()
        {
            Console.Write("Оберіть режим роботи (1 - Line, 2 - HyperPlane): ");
            char choice = Console.ReadKey().KeyChar;
            Console.WriteLine();

            if (choice == '1')
            {
                Console.WriteLine("\nВведіть коефіцієнти A, B, C:");
                double a = double.Parse(Console.ReadLine());
                double b = double.Parse(Console.ReadLine());
                double c = double.Parse(Console.ReadLine());

                Line line = new Line(a, b, c);
                line.PrintCoefficients();

                Console.WriteLine("Введіть координати точки (x, y):");
                double x = double.Parse(Console.ReadLine());
                double y = double.Parse(Console.ReadLine());

                Console.WriteLine($"Точка належить прямій: {line.Belongs(new double[] { x, y })}");
            }
            else
            {
                Console.WriteLine("\nВведіть коефіцієнти A, B, C, D, E:");
                double a = double.Parse(Console.ReadLine());
                double b = double.Parse(Console.ReadLine());
                double c = double.Parse(Console.ReadLine());
                double d = double.Parse(Console.ReadLine());
                double e = double.Parse(Console.ReadLine());

                HyperPlane hp = new HyperPlane(a, b, c, d, e);
                hp.PrintCoefficients();

                Console.WriteLine("Введіть координати точки (x1, x2, x3, x4):");
                double x1 = double.Parse(Console.ReadLine());
                double x2 = double.Parse(Console.ReadLine());
                double x3 = double.Parse(Console.ReadLine());
                double x4 = double.Parse(Console.ReadLine());

                Console.WriteLine($"Точка належить гіперплощині: {hp.Belongs(new double[] { x1, x2, x3, x4 })}");
            }

            Console.WriteLine("\n--- Кінець роботи програми ---");
        }
    }
}
