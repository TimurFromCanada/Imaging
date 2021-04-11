using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        static double[,] GetSxInSy(double[,] sx)
        {
            var width = sx.GetLength(0);
            var sy = new double[width, width];

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    sy[j, i] = sx[i, j];
                }
            }
                
            return sy;
        }

        static double[,] EnvironsXY(int x, int y, int width1, double[,] g)
        {
            var environs = new double[width1, width1];

            for (var i = -width1 / 2; i <= width1 / 2; i++)
            {
                for (var j = -width1 / 2; j <= width1 / 2; j++)
                {
                    environs[i + width1 / 2, j + width1 / 2] = g[x + i, y + j];
                }
            }
                
            return environs;
        }

        static double GetScalarProductOfMatrices(double[,] matrice1, double[,] matrice2)
        {
            var scalar = 0.0;
            var length = matrice1.GetLength(0);

            for (var i = 0; i < length; i++)
            {
                for (var j = 0; j < length; j++)
                {
                    scalar += matrice1[i, j] * matrice2[i, j];
                }
            }
                
            return scalar;
        }

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sy = GetSxInSy(sx);
            var width1 = sx.GetLength(0);

            for (int x = width1 / 2; x < width - width1 / 2; x++)
            {
                for (int y = width1 / 2; y < height - width1 / 2; y++)
                {
                    var environs = EnvironsXY(x, y, width1, g);
                    var gx = GetScalarProductOfMatrices(environs, sx);
                    var gy = GetScalarProductOfMatrices(environs, sy);
                    result[x, y] = Math.Sqrt(gx * gx + gy * gy);
                }
            }
                
            return result;
        }
    }
}