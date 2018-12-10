using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ParallelMatrixMultiplication
{
    public class ParallelForMatrixMult
    {

        public void MultiplyMatricesSequential(double[,] matA, double[,] matB,
                                                double[,] result)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            for (int i = 0; i < matARows; i++)
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matA[i, k] * matB[k, j];
                    }
                    result[i, j] += temp;
                }
            }
        }

        public void MultiplyMatricesParallel(double[,] matA, double[,] matB, double[,] result)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            // A basic matrix multiplication.
            // Parallelize the outer loop to partition the source array by rows.
            Parallel.For(0, matARows, i =>
            {
                for (int j = 0; j < matBCols; j++)
                {
                    double temp = 0;
                    for (int k = 0; k < matACols; k++)
                    {
                        temp += matA[i, k] * matB[k, j];
                    }
                    result[i, j] = temp;
                }
            }); // Parallel.For
        }

        public void MultiplyMatricesParallelTwo(double[,] matA, double[,] matB, double[,] result)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            // A basic matrix multiplication.
            // Parallelize the outer loop to partition the source array by rows.
            Parallel.For(0, matARows, i =>
            {
                Parallel.For(0, matBCols, j =>
                 {
                     double temp = 0;
                     for (int k = 0; k < matACols; k++)
                     {
                         temp += matA[i, k] * matB[k, j];
                     }
                     result[i, j] = temp;
                 });
            }); // Parallel.For
        }

        public void MultiplyMatricesParallelAll(double[,] matA, double[,] matB, double[,] result)
        {
            int matACols = matA.GetLength(1);
            int matBCols = matB.GetLength(1);
            int matARows = matA.GetLength(0);

            // A basic matrix multiplication.
            // Parallelize the outer loop to partition the source array by rows.
            Parallel.For(0, matARows, i =>
            {
                Parallel.For(0, matBCols, j =>
                {
                    double temp = 0;
                    Parallel.For(0, matACols, k =>
                    {
                        temp += matA[i, k] * matB[k, j];
                    });
                    result[i, j] = temp;
                });
            }); // Parallel.For
        }

        public List<long> RunMatrixMultWithTime(double[,] m1, double[,] m2)
        {

            int rowCount = m1.GetLength(0);
            int colCount2 = m2.GetLength(1);
 
            double[,] result = new double[rowCount, colCount2];

            // sequential version.
            Stopwatch stopwatch = Stopwatch.StartNew();

            MultiplyMatricesSequential(m1, m2, result);
            stopwatch.Stop();
            List<long> times = new List<long>();
            times.Add(stopwatch.ElapsedMilliseconds);

            // Reset timer and results matrix. 
            result = new double[rowCount, colCount2];

            // parallel loop.
            stopwatch = Stopwatch.StartNew();
            MultiplyMatricesParallel(m1, m2, result);
            stopwatch.Stop();
            times.Add(stopwatch.ElapsedMilliseconds);

            stopwatch = Stopwatch.StartNew();
            MultiplyMatricesParallelTwo(m1, m2, result);
            stopwatch.Stop();
            times.Add(stopwatch.ElapsedMilliseconds);

            stopwatch = Stopwatch.StartNew();
            MultiplyMatricesParallelAll(m1, m2, result);
            stopwatch.Stop();
            times.Add(stopwatch.ElapsedMilliseconds);

            return times;
        }
    }
}
