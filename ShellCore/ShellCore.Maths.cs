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

        // yea hyeah yeah yeah yeah yeah yeah yeah yeah yeah yeah yeah yeah yeah yea hyeah 
        public int BaseMath(int operationId, int numOfArguments, List<int> args) // sorta shitty basic math function
        {
            int count = 1; // start at 1
            int result = args[0]; // bug fix

            for (int i = 0; i < numOfArguments; i++) // loop
            {
                try
                {
                    if (operationId == 0) // can't use a switch statement, sadly
                    {
                        result = result + args[count];
                    }
                    else if (operationId == 1)
                    {
                        result = result - args[count];
                    }
                    else if (operationId == 2)
                    {
                        result = result * args[count];
                    }
                    else if (operationId == 3)
                    {
                        result = result / args[count];
                    }
                    else if (operationId == 4)
                    {
                        result = result % args[count];
                    }
                    else if (operationId == 5)
                    {
                        result = result & args[count];
                    }
                    else if (operationId == 6)
                    {
                        result = result | args[count];
                    }
                    else if (operationId == 7)
                    {
                        result = result ^ args[count];
                    }

                    count++;

                }
                catch (IndexOutOfRangeException err)
                {
                    return 0xD15EA5E;
                }


            }
            return result;
        }
       // 8 FUNCTIONS removed (2.1? 2.5?)
        public double Sqrt(int num) // Sqrt
        {
            double num3 = Math.Sqrt(num);
            return num3;
        }

        public double Sin(int num) // Sin
        {
            double num3 = Math.Sin(num);
            return num3;
        }

        public double Cos(int num) // Cos
        {
            double num3 = Math.Cos(num);
            return num3;
        }

        public double Tan(int num) // Tan
        {
            double num3 = Math.Tan(num);
            return num3;
        }
        

        // TODO: add file APIs
        /*
        public int UsefulLibraryMaths() // Class Constructor for 1.9-3.9
        {
            Pi = Math.PI; // pi
            E = Math.E;
            C = 299792458; // speed of light in m/s
        }
        */
    }
}
