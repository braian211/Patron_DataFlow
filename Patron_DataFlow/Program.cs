using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patron_DataFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] vec = {5,19,14,4,7,23,8,22,1};

            Task<int> T1 = Task.Factory.StartNew(() => {
                int acumPar = 0;
                for (int i=0;i<vec.Length;i++)
                {
                    if (vec[i] % 2 == 0)
                    {
                        acumPar+= vec[i];
                    }
                }
                return acumPar;
                });

            Task<int> T2 = Task.Factory.StartNew(() =>
            {
                int acumImpar = 0;
                for (int i = 0; i < vec.Length; i++)
                {
                    if (vec[i] % 2 != 0)
                    {
                        acumImpar+=vec[i];
                    }
                }
                return acumImpar;
            });

                Task<int> T3 = Task.Factory.StartNew(() => {
                      int max = vec.Max();
                    return max;
                });

            Task<int> T4 = Task.Factory.ContinueWhenAll<int,int>(new[] { T1,T2,T3}, (tasks) => {
                int result=0;
                int aux = 0;
                foreach(Task<int> t in tasks)
                {
                    Console.WriteLine("resultado de tarea {0} fue {1}", t.Id, t.Result);
                    aux = t.Result;
                    result += aux;
                }
                return result;
            });
            Task.WaitAll(new[] { T1, T2, T3, T4 });
            Console.WriteLine("el resultado final fue: {0}",T4.Result);
            Console.WriteLine("press key to exit...");
            Console.ReadLine();
        }
    }
}
