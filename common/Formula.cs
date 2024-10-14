using System;
using Pharmacy.INST.DissolutionClient.common;

namespace Pharmacy.INST.DissolutionClient.common
{
    public static class Formula
    {
        private static readonly int _decimaldigits = 7;//小数位数保留2位
        //根据两组数，计算出y=kx+b的k,b值，用于标定
        public static void Calculate_KB(double y1, double y2, double x1, double x2, ref double kvalue, ref double bvalue)//求方程y=kx+b 系数 k ,b   标定吸  吐  参数
        {
            double coefficient = 1;//系数值
            try
            {
                if ((x1 == 0) || (x2 == 0) || (x1 == x2)) return; //排除为零的情况以及x1，x2相等时无法运算的情况
                //if (y1 == y2) return; //根据具体情况而定，如何这两个值相等，得到的就是一条直线
                double temp = 0;
                if (x1 >= x2)
                {
                    coefficient = (float)Math.Round((x1 / x2), _decimaldigits);
                    temp = y2 * coefficient; //将对应的函数乘以系数
                    bvalue = (double)Math.Round(((temp - y1) / (coefficient - 1)), _decimaldigits);
                    kvalue = (double)Math.Round(((y1 - bvalue) / x1), _decimaldigits); //求出k值
                }
                else
                {
                    coefficient = x2 / x1;
                    temp = y1 * coefficient;
                    bvalue = (double)Math.Round(((temp - y2) / (coefficient - 1)), _decimaldigits);//求出b值
                    kvalue = (double)Math.Round(((y2 - bvalue) / x2), _decimaldigits); //求出k值
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }
        public static void Calculate_KB_Ex(double y1, double y2, double x1, double x2, ref double kvalue, ref double bvalue)//求方程y=kx+b 系数 k ,b   标定吸  吐  参数
        {
            try
            {
                double kvalue1;
                double kvalue2;
                kvalue1 = y1 * y1 / x1;
                kvalue1 = (double)Math.Round(((y1 - bvalue) / x1), _decimaldigits); //求出k值

                kvalue2 = y2 * y2 / x2;
                kvalue2 = (double)Math.Round(((y1 - bvalue) / x1), _decimaldigits); //求出k值

                kvalue = (double)Math.Round((kvalue1 + kvalue2) / 2, _decimaldigits);

                bvalue = 0;

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }
        }
        //根据溶媒体积和溶出方法，计算出下降到的刻度位置
        public static double Calculate_SamplePointPosition(double SolventVolume, string DissolutionMethodName)
        {
            double postion = 0;
            double bottom_margin = 250;
            try
            {
                if (DissolutionMethodName.Equals(StaticParam.DissolutionMethod_Arr[0])
                    ) //桨法
                {
                    postion = ((Convert.ToDouble(SolventVolume) - bottom_margin) / 2) + bottom_margin;
                }
                if (DissolutionMethodName.Equals(StaticParam.DissolutionMethod_Arr[1])
                   ) //篮法
                {
                    bottom_margin = 300;
                    postion = ((Convert.ToDouble(SolventVolume) - bottom_margin) / 2) + bottom_margin;
                }
                if (DissolutionMethodName.Equals(StaticParam.DissolutionMethod_Arr[2])
                    ) //小杯法
                {
                    postion = 0.5 * SolventVolume + 33.75;
                }
                //postion = ((Convert.ToDouble(SolventVolume) - bottom_margin) / 2) + bottom_margin;
            }
            catch (Exception e)
            {
                App.WriteSystemLog(e.ToString());
            }
            return postion;
        }
    }
}
