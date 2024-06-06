
using System;

public class Complex
{
    public static readonly Complex Zero = new Complex(0, 0);
    public static readonly Complex One = new Complex(1, 0);
    public static readonly Complex ImaginaryOne = new Complex(0, 1);
    public double X { get; }
    public double Y { get; }
    public double Length => Math.Sqrt(X * X + Y * Y);
    public Complex(double x, double y) { X = x; Y = y; }
    public Complex(double x) : this(x, 0) { }
    public Complex() : this(0, 0) { }

    public static Complex Re(double x) => new Complex(x, 0);
    public static Complex Im(double y) => new Complex(0, y);
    public static Complex Sqrt(double value)
    {
        if (value >= 0) return new Complex(Math.Sqrt(value), 0);
        else return new Complex(0, Math.Sqrt(-value));
    }

    public static Complex operator +(Complex a, Complex b) => new Complex(a.X + b.X, a.Y + b.Y);
    public static Complex operator -(Complex a, Complex b) => new Complex(a.X - b.X, a.Y - b.Y);
    public static Complex operator *(Complex a, Complex b) => new Complex(a.X * b.X - a.Y * b.Y, a.X * b.Y + a.Y * b.X);
    public static Complex operator /(Complex a, Complex b)
    {
        if (b.X == 0 && b.Y == 0) throw new DivideByZeroException("Нельзя делить на 0");
        double denominator = b.X * b.X + b.Y * b.Y;
        return new Complex((a.X * b.X + a.Y * b.Y) / denominator, (a.Y * b.X - a.X * b.Y) / denominator);
    }

    public static Complex operator +(Complex a) => a;
    public static Complex operator -(Complex a) => new Complex(-a.X, -a.Y);
    public override string ToString() => $"{X} + {Y}i";
    public override bool Equals(object obj)
    {
        if (obj is Complex other) return X == other.X && Y == other.Y;
        return false;
    }
    public override int GetHashCode() => X.GetHashCode() ^ Y.GetHashCode();
}