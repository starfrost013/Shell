using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Cosmo's Useful Library

// Version 2.1.0
// © 2019 Cosmo

    
namespace Shell.Core
{
    public partial class ShellCore
    {
        public double Pi { get; set; }

        public double E { get; set; }

        public int C { get; set; } // speed of light in m/s

        public enum BaseMathOperation { Add=0, Subtract=1, Multiply=2, Divide=3, Modulus=4, Sqrt=5, Sin=6, Cos=7, Tan=8, Asin=9, Acos=10, Atan=11 }
        public double BaseMath(BaseMathOperation operationId, double num, double num2) // 2-numbers only overload
        {
            double result = num; // bug fix
            int opId = Convert.ToInt32(operationId);
                try
                {
                    switch (opId)
                    {
                        case 0: // can't use a switch statement, sadly
                            result = num + num2;
                            return result;
                        case 1:
                            result = num - num2;
                            return result;
                        case 2:
                            result = num * num2;
                            return result;
                        case 3:
                            result = num / num2;
                            return result;
                        case 4:
                            result = num % num2;
                            return result;
                        case 5:
                            result = Math.Sqrt(num);
                            return result;
                        case 6:
                            result = Math.Sin(num);
                            return result;
                        case 7:
                            result = Math.Cos(num);
                            return result; 
                        case 8:
                            result = Math.Tan(num);
                            return result;
                        case 9:
                            result = Math.Asin(num);
                            return result;
                        case 10:
                            result = Math.Acos(num);
                            return result;
                        case 11:
                            result = Math.Atan(num);
                            return result;
                        default:
                            ElmThrowException(44);
                            return -0xd15ea5e;
                    }

                }
                catch (IndexOutOfRangeException)
                {
                    ElmThrowException(45);
                }
            return result;
        }

        public double BaseMath(BaseMathOperation operationId, int numOfArguments, List<double> args) // sorta shitty basic math function
        {
            int count = 1; // start at 1
            double result = args[0]; // bug fix
            int opId = Convert.ToInt32(operationId);
            for (int i = 0; i < numOfArguments; i++) // loop
            {
                try
                {
                    switch (opId)
                    {
                        case 0: // can't use a switch statement, sadly
                            result = result + args[count];
                            continue;
                        case 1:
                            result = result - args[count];
                            continue;
                        case 2:
                            result = result * args[count];
                            continue;
                        case 3:
                            result = result / args[count];
                            continue;
                        case 4:
                            result = result % args[count];
                            continue;
                        case 5:
                            result = Math.Sqrt(args[count]);
                            continue;
                        case 6:
                            result = Math.Sin(args[count]);
                            continue;
                        case 7:
                            result = Math.Cos(args[count]);
                            continue;
                        case 8:
                            result = Math.Tan(args[count]);
                            continue;
                        case 9:
                            result = Math.Asin(args[count]);
                            continue;
                        case 10:
                            result = Math.Acos(args[count]);
                            continue;
                        case 11:
                            result = Math.Atan(args[count]);
                            continue;
                        default:
                            ElmThrowException(44);
                            return -0xd15ea5e;
                    }

                }
                catch (IndexOutOfRangeException)
                {
                    ElmThrowException(45);
                }


            }
            return result;
        }



        public double Power(int x, int y)
        {
            double result = Math.Pow(x,y);
            return result;
        }

        public List<double> Ratio(double n, double x, double y)
        {
            double z = x + y;
            double r = n / z;
            double q1 = r * x;
            double q2 = r * y;
            List<double> q = new List<double> { q1, q2 };
            return q;
        }
    }
}
