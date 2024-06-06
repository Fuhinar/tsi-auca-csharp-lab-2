using System;
using System.Linq;

public interface IPolynomial
{
    int Dimension { get; }
    double[] Coefficients { get; }
    Complex[] FindRoots();
}

public class Polynomial : IPolynomial
{
    private double[] coefficients;
    private IRootFinder strategy;

    public int Dimension => coefficients.Length;
    public double[] Coefficients => (double[])coefficients.Clone();

    public Polynomial(double[] coefficients)
    {
        this.coefficients = Equations.RemoveLeadingZeros(coefficients);
        this.strategy = Equations.SelectStrategy(this.coefficients);
    }

    public Complex[] FindRoots()
    {
        return strategy.FindRoots(this.coefficients);
    }
}

public static class Equations
{
    public static double[] RemoveLeadingZeros(double[] coefficients)
    {
        return coefficients.Reverse().SkipWhile(c => c == 0).Reverse().ToArray();
    }

    public static IRootFinder SelectStrategy(double[] coefficients)
    {
        int degree = coefficients.Length - 1;
        switch (degree)
        {
            case 1: return Strategies.LinearSolver;
            case 2: return Strategies.QuadraticSolver;
            case 3: return Strategies.CubicSolver;
            default: throw new UnknownEquationTypeException();
        }
    }

    public static Polynomial CreatePolynomial(double[] coefficients)
    {
        double[] cleanedCoefficients = RemoveLeadingZeros(coefficients);
        IRootFinder strategy = SelectStrategy(cleanedCoefficients);
        return new Polynomial(cleanedCoefficients, strategy);
    }
}

