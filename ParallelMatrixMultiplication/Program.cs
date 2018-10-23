using ParallelMatrixMultiplication;
using System;
using System.Collections.Generic;

class MultiplyMatrices
{
    static void Main(string[] args)
    {
        int rowCount = 500;
        int colCount = 510;
        int colCount2 = 520;
        List<long> times;
        ParallelForMatrixMult PFMM = new ParallelForMatrixMult();
        TaskMatrixMult TMM;
        do
        {
            times = new List<long>();
            double [,] m1 = InitializeMatrix(rowCount, colCount);
            double[,] m2 = InitializeMatrix(colCount, colCount2);
            times = PFMM.RunMatrixMultWithTime(m1,m2);
            TMM = new TaskMatrixMult(m1, m2);
            times.Add(TMM.RunTaskMatrix());
            colCount++;
            Console.WriteLine("{0} {1} {2}: {3} {4} {5} \n", times[0],times[1],times[2], rowCount, colCount, colCount2 );
        } while (times[0]<times[1]);
        Console.ReadKey();
    }

    public static double[,] InitializeMatrix(int rows, int cols)
    {
        double[,] matrix = new double[rows, cols];

        Random r = new Random();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = r.Next(100);
            }
        }
        return matrix;
    }




}