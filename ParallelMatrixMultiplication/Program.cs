using ParallelMatrixMultiplication;
using System;
using System.Collections.Generic;

class MultiplyMatrices
{
    static void Main(string[] args)
    {
        //int rowCount = 100;
        //int colCount = 0;
        //int colCount2 = 100;
        //List<long> times;
        //ParallelForMatrixMult PFMM = new ParallelForMatrixMult();
        //TaskMatrixMult TMM;
        //Console.WriteLine("Times: Sequential, Parallel.For, Par.For2, Par.ForAll, TaskParallel  Size: Rows1, Cols1/Rows2, Cols2");
        //do
        //{
        //    times = new List<long>();
        //    double[,] m1 = InitializeMatrix(rowCount, colCount);
        //    double[,] m2 = InitializeMatrix(colCount, colCount2);
        //    times = PFMM.RunMatrixMultWithTime(m1, m2);
        //    TMM = new TaskMatrixMult(m1, m2);
        //    times.Add(TMM.RunTaskMatrix());
        //    //times.Add(TMM.SetupTime());
        //    //times.Add(TMM.ParrallelSetupTime());
        //    Console.WriteLine("Times: {0} {1} {2} {3} {4} Size: {5} {6} {7}", times[0], times[1], times[2], times[3], times[4], rowCount, colCount, colCount2);
        //    colCount += 500;
        //} while (colCount <= 10000);


        string path = "C:\\Users\\caleb\\Documents\\Fall2018\\ProgLang\\Project\\CroweSkypeHeadshot";
        ImageDarkening ImgDarken = new ImageDarkening(path);
        string path2 = "C:\\Users\\caleb\\Documents\\Fall2018\\ProgLang\\Project\\Castle";
        ImageDarkening ImgDarken2 = new ImageDarkening(path2);
        long time1 = ImgDarken.DarkenSequential();
        long time2 = ImgDarken.DarkenParallel();
        long time3 = ImgDarken2.DarkenSequential();
        long time4 = ImgDarken2.DarkenParallel();

        Console.WriteLine("Sequential Image Darken: " + time1);
        Console.WriteLine("Parallel Image Darken: " + time2);
        Console.WriteLine("Sequential Image Darken: " + time3);
        Console.WriteLine("Parallel Image Darken: " + time4);
        //Console.WriteLine("Done");

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