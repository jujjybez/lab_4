using System;

class MyMatrix
{
    private int[,] matrix;
    private int n, m;

    public MyMatrix(int n, int m, int min_val, int max_val)
    {
        this.n = n;
        this.m = m;
        this.matrix = new int[n, m];
        Random rand = new Random();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++) { matrix[i, j] = rand.Next(min_val, max_val + 1); }
        }
    }
    
    public int this[int i, int j]
    {
        get { return matrix[i, j]; }
        set { matrix[i, j] = value; }
    }

    public static MyMatrix operator +(MyMatrix a, MyMatrix b)
    {
        if (a.n != b.n || a.m != b.m) { throw new Exception("Matrices have different dimensions"); }
        MyMatrix result = new MyMatrix(a.n, a.m, 0, 0);
        for (int i = 0; i < a.n; i++)
        {
            for (int j = 0; j < a.m; j++) { result.matrix[i, j] = a.matrix[i, j] + b.matrix[i, j]; }
        }
        return result;
    }

    public static MyMatrix operator -(MyMatrix a, MyMatrix b)
    {
        if (a.n != b.n || a.m != b.m) { throw new Exception("Matrices have different dimensions"); }
        MyMatrix result = new MyMatrix(a.n, a.m, 0, 0);
        for (int i = 0; i < a.n; i++)
        {
            for (int j = 0; j < a.m; j++) { result.matrix[i, j] = a.matrix[i, j] - b.matrix[i, j]; }
        }
        return result;
    }

    public static MyMatrix operator *(MyMatrix a, MyMatrix b)
    {
        if (a.n != b.m) { throw new Exception("Matrices dimensions are not suitable for multiplication"); }
        MyMatrix result = new MyMatrix(a.n, b.m, 0, 0);
        for (int i = 0; i < a.n; i++)
        {
            for (int j = 0; j < b.m; j++)
            {
                for (int k = 0; k < a.m; k++) { result.matrix[i, j] += a.matrix[i, k] * b.matrix[k, j]; }
            }
        }
        return result;
    }

    public static MyMatrix operator *(MyMatrix a, int scalar)
    {
        MyMatrix result = new MyMatrix(a.n, a.m, 0, 0);
        for (int i = 0; i < a.n; i++)
        {
            for (int j = 0; j < a.m; j++) { result.matrix[i, j] = a.matrix[i, j] * scalar; }
        }
        return result;
    }

    public static MyMatrix operator /(MyMatrix a, int scalar)
    {
        MyMatrix result = new MyMatrix(a.n, a.m, 0, 0);
        for (int i = 0; i < a.n; i++)
        {
            for (int j = 0; j < a.m; j++) { result.matrix[i, j] = a.matrix[i, j] / scalar; }
        }
        return result;
    }

    public void Print()
    {
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++) { Console.Write($"{matrix[i, j]} "); }
            Console.WriteLine();
        }
    }

    static void Main(string[] args)
    {
        int n, m, min_val, max_val;

        Console.WriteLine("Enter the number of rows:");
        n = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the number of columns:");
        m = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the minimum value for random numbers:");
        min_val = int.Parse(Console.ReadLine());
        Console.WriteLine("Enter the maximum value for random numbers:");
        max_val = int.Parse(Console.ReadLine());

        MyMatrix matrixA = new MyMatrix(n, m, min_val, max_val);
        MyMatrix matrixB = new MyMatrix(n, m, min_val, max_val);

        Console.WriteLine("Matrix A:");
        matrixA.Print();
        Console.WriteLine("Matrix B:");
        matrixB.Print();

        Console.WriteLine("Sum of matrices:");
        (matrixA + matrixB).Print();

        Console.WriteLine("Difference of matrices:");
        (matrixA - matrixB).Print();

        Console.WriteLine("Product of matrices:");
        (matrixA * matrixB).Print();

        int scalar = 5;
        Console.WriteLine($"Matrix A multiplied by {scalar}:");
        (matrixA * scalar).Print();

        int divisor = 2;
        Console.WriteLine($"Matrix A divided by {divisor}:");
        (matrixA / divisor).Print();
    }
}
