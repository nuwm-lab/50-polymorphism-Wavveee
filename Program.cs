using System;

class Line
{
protected float coefficientA, coefficientB, coefficientC;
// Конструктор
public Line(float a, float b, float c)
{
    SetCoefficients(a, b, c);
}

// Віртуальний метод для задання коефіцієнтів
public virtual void SetCoefficients(float a, float b, float c)
{
    if (a == 0 && b == 0)
        throw new ArgumentException("Коефіцієнти A і B не можуть бути одночасно нульовими.");

    coefficientA = a;
    coefficientB = b;
    coefficientC = c;
}

// Віртуальний метод для виведення коефіцієнтів
public virtual void PrintCoefficients()
{
    Console.WriteLine($"Line: {coefficientA}*x + {coefficientB}*y + {coefficientC} = 0");
}

// Метод перевірки належності точки
public virtual bool Belongs(float x, float y)
{
    return Math.Abs(coefficientA * x + coefficientB * y + coefficientC) < 1e-6;
}

}

class HyperPlane : Line
{
private float coefficientD, coefficientE;
// Конструктор
public HyperPlane(float a, float b, float c, float d, float e)
    : base(a, b, c)
{
    coefficientD = d;
    coefficientE = e;
}

// Перевантаження методу для встановлення всіх коефіцієнтів
public void SetCoefficients(float a, float b, float c, float d, float e)
{
    base.SetCoefficients(a, b, c);
    coefficientD = d;
    coefficientE = e;
}

// Перевизначений метод для виведення коефіцієнтів
public override void PrintCoefficients()
{
    Console.WriteLine($"HyperPlane: {coefficientA}*x1 + {coefficientB}*x2 + {coefficientC}*x3 + {coefficientD}*x4 + {coefficientE} = 0");
}

// Перевизначений метод перевірки належності точки (4D)
public override bool Belongs(float x1, float x2)
{
    // для демонстрації поліморфізму параметри не важливі
    Console.WriteLine("Неможливо перевірити належність точки для 4D без усіх координат.");
    return false;
}

// Окремий метод для 4D варіанту
public bool Belongs(float x1, float x2, float x3, float x4)
{
    return Math.Abs(coefficientA * x1 + coefficientB * x2 + coefficientC * x3 + coefficientD * x4 + coefficientE) < 1e-6;
}

}

class Program
{
static void Main()
{
Console.Write("Оберіть режим роботи (1 - Line, 2 - HyperPlane): ");
char userChoose = Console.ReadKey().KeyChar;
Console.WriteLine();

    Line obj; // покажчик на базовий клас

    if (userChoose == '1')
    {
        // Динамічне створення об’єкта класу Line
        obj = new Line(1, -2, 3);
        obj.PrintCoefficients();
        Console.WriteLine($"Точка (1, 1) належить прямій: {obj.Belongs(1, 1)}");
    }
    else
    {
        // Динамічне створення об’єкта класу HyperPlane
        obj = new HyperPlane(1, 2, -3, 4, 5);
        obj.PrintCoefficients();

        // Виклик віртуального методу через покажчик
        obj.Belongs(1, 2);
    }

    Console.WriteLine("\n--- Кінець роботи програми ---");
}
}
