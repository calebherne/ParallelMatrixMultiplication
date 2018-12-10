using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            //long initial = GC.GetTotalMemory(false);
            //long[] mem = new long[100];

            Stopwatch stopwatch = Stopwatch.StartNew();
            Task[] taskList = new Task[MatA.GetLength(0)];
            for(int i = 0; i < MatA.GetLength(0); i++)
            {
                var temp = i;
                var task = new Task(() => {
                    MultiplyOneRow(temp);
                    //mem[temp] = GC.GetTotalMemory(false) - initial;
                });
                task.Start();
                taskList[i] = task;
            }
            Task.WaitAll(taskList);
            stopwatch.Stop();

            //Console.WriteLine("Memory Increase due to task parallel implementation:" + mem.Max());
            return stopwatch.ElapsedMilliseconds;
        }

        public long SetupTime()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Task[] taskList = new Task[MatA.GetLength(0)];
            for (int i = 0; i < MatA.GetLength(0); i++)
            {
                var temp = i;
                var task = new Task(() => {
                    MultiplyOneRow(temp);
                });
                taskList[i] = task;
            }
            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        public long ParrallelSetupTime()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            Task[] taskList = new Task[MatA.GetLength(0)];
            Parallel.For(0, MatA.GetLength(0)-1, i =>
            {
                var temp = i;
                var task = new Task(() => {
                    MultiplyOneRow(temp);
                });
                taskList[i] = task;
            });
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
