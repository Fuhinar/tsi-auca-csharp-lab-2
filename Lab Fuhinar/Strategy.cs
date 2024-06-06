using System.Numerics;

public interface IRootFinder
{
    Complex[] FindRoots(double[] coefficients);
}

public class LinearRootFinder : IRootFinder
{
    public Complex[] FindRoots(double[] coefficients)
    {
        if (coefficients.Length != 2)
            throw new InvalidOperationException("Linear equation must have exactly 2 coefficients.");
        double a = coefficients[0];
        double b = coefficients[1];
        return new[] { new Complex(-b / a, 0) };
    }
}

public class QuadraticRootFinder : IRootFinder
{
    public Complex[] FindRoots(double[] coefficients)
    {
        if (coefficients.Length != 3)
            throw new InvalidOperationException("Quadratic equation must have exactly 3 coefficients.");
        double a = coefficients[0], b = coefficients[1], c = coefficients[2];
        double discriminant = b * b - 4 * a * c;
        double realPart = -b / (2 * a);
        if (discriminant < 0)
        {
            double imaginaryPart = Math.Sqrt(-discriminant) / (2 * a);
            return new[] { new Complex(realPart, imaginaryPart), new Complex(realPart, -imaginaryPart) };
        }
        else if (discriminant == 0)
        {
            return new[] { new Complex(realPart, 0) };
        }
        else
        {
            double rootPart = Math.Sqrt(discriminant) / (2 * a);
            return new[] { new Complex(realPart + rootPart, 0), new Complex(realPart - rootPart, 0) };
        }
    }
}

public class CubicRootFinder : IRootFinder
{
    public Complex[] FindRoots(double[] coefficients)
    {
        if (coefficients.Length != 4)
            throw new ArgumentException("Cubic equation must have exactly 4 coefficients.");

        double a = coefficients[3];
        double b = coefficients[2];
        double c = coefficients[1];
        double d = coefficients[0];

        if (a == 0)
            throw new ArgumentException("Coefficient 'a' must not be zero for a cubic equation.");

        b /= a;
        c /= a;
        d /= a;

        double p = c - b * b / 3.0;
        double q = 2.0 * b * b * b / 27.0 - b * c / 3.0 + d;

        double discriminant = (q * q) / 4.0 + (p * p * p) / 27.0;

        Complex u, v;
        if (discriminant < 0)
        {
            double r = Math.Sqrt(-p / 3.0);
            double theta = Math.Acos(-Math.Sqrt(-27.0 / p) * q / 2.0) / 3.0;
            u = new Complex(r * Math.Cos(theta), 0);
            v = new Complex(r * Math.Cos(theta + 2.0 * Math.PI / 3.0), 0);
        }
        else
        {
            u = Complex.Pow(-q / 2.0 + Complex.Sqrt(discriminant), 1.0 / 3.0);
            v = Complex.Pow(-q / 2.0 - Complex.Sqrt(discriminant), 1.0 / 3.0);
        }

        double offset = b / 3.0;
        Complex x1 = u + v - offset;
        Complex x2 = -(u + v) / 2.0 - offset + Complex.ImaginaryOne * (u - v) * Math.Sqrt(3) / 2.0;
        Complex x3 = -(u + v) / 2.0 - offset - Complex.ImaginaryOne * (u - v) * Math.Sqrt(3) / 2.0;

        return new Complex[] { x1, x2, x3 };
    }
}

public static class Strategies
{
    public static readonly IRootFinder LinearSolver = new LinearRootFinder();
    public static readonly IRootFinder QuadraticSolver = new QuadraticRootFinder();
    public static readonly IRootFinder CubicSolver = new CubicRootFinder();
}
