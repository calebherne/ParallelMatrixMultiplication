using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace ParallelMatrixMultiplication
{
    public class TaskMatrixMult
    {
        public double[,] MatA { get; set; }
        public double[,] MatB { get; set; }
        public double[,] Result { get; set; }

        public TaskMatrixMult(double[,] m1, double[,] m2)
        {
            MatA = m1;
            MatB = m2;
            Result = new double[MatA.GetLength(0), MatB.GetLength(1)];
        }

        public long RunTaskMatrix()
        {
            Task[] taskList = new Task[MatA.GetLength(0)];
            Stopwatch stopwatch = Stopwatch.StartNew();
            for(int i = 0; i < MatA.GetLength(0); i++)
            {
                var temp = i;
                var task = new Task(() => {
                    MultiplyOneRow(temp);
                });
                task.Start();
                taskList[i] = task;
            }
            Task.WaitAll(taskList);
            stopwatch.Stop();
            
            return stopwatch.ElapsedMilliseconds;
        }

        private void MultiplyOneRow(int row)
        {
            for (int j = 0; j < MatB.GetLength(1); j++)
            {
                double temp = 0;
                for (int k = 0; k < MatA.GetLength(1); k++)
                {
                    temp += MatA[row, k] * MatB[k, j];
                }
                Result[row, j] = temp;
            }

        }
    }
}
