using Laboratory5.Constants;
using Laboratory5.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratory5.Helpers
{
    public class MatrixMultiplicationHelper
    {
        #region Public methods

        public static MultiplicationResponse InternalMultiplication(int a, int b, int c)
        {
            var fileName = $"test_{a}_{b}_{c}.txt";
            var path = System.Web.HttpContext.Current.Server.MapPath($"~/App_Data/{fileName}");

            double[,] first = null, second = null;

            if (File.Exists(path))
            {
                var fileContent = File.ReadAllLines(path);
                var sizes = fileContent[0].Split(',').Select(size => Convert.ToInt32(size)).ToList();
                int l = sizes[0];
                int m = sizes[1];
                int n = sizes[2];

                first = new double[l, m];
                second = new double[m, n];

                for (int j = 0; j < l; ++j)
                {
                    var row = fileContent[j + 2].Split(',').Select(rowElement => Convert.ToDouble(rowElement)).ToList();
                    for (int k = 0; k < row.Count; k++)
                    {
                        first[j, k] = row[k];
                    }
                }

                for (int j = 0; j < m; ++j)
                {
                    var row = fileContent[j + l + 3].Split(',').Select(rowElement => Convert.ToDouble(rowElement)).ToList();
                    for (int k = 0; k < row.Count; k++)
                    {
                        second[j, k] = row[k];
                    }
                }
            }

            return MultiplyMatrices(first, second);
        }

        public static MultiplicationResponse MultiplyMatrices(double[,] first, double[,] second)
        {
            if(first != null && second != null)
            {
                var result = Multipicate(first, second);
                if(result != null)
                {
                    var response = new MultiplicationResponse
                    {
                        ResultArray = result,
                        Status = MultiplicationResult.SUCCESS
                    };
                    return response;
                }
            }
            return new MultiplicationResponse { ResultArray = null, Status = MultiplicationResult.FAILED };
        }

        #endregion

        #region Private methods

        private static double[,] Multipicate(double[,] a, double[,] b)
        {
            if (a.Length / a.GetLength(0) != b.GetLength(0)) return null;
            int l = a.GetLength(0);
            int m = a.Length / l;
            int n = b.Length / m;
            double[,] c = new double[l,n];

            Parallel.For(0, l, delegate (int i)
            {
                for (int j = 0; j < n; j++)
                {
                    double v = 0;

                    for (int k = 0; k < m; k++)
                    {
                        v += a[i,k] * b[k,j];
                    }

                    c[i,j] = v;
                }
            });
            return c;
        }

        #endregion
    }
}