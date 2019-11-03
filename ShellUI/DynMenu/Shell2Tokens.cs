using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Shell2 Language Tokeniser
/// Spits out a list of tokens, variables, keywords, commands, operations and function for the Shell2 language.
/// File created: 2019-11-03
/// </summary>
namespace Shell
{
    partial class ShellUI
    {
        public enum S2Tokens { If = 0, For, While, Colon, Number, Variable, Command, Operation, Function }
        public List<S2Var> S2Variables;
        public List<S2Command> S2Commands;
        public List<S2Operation> S2Operations;
        public List<S2Function> S2Functions;
        // first thing it does is parse every function, before running them.

        public void S2Init()
        {
            S2Variables = new List<S2Var>();
            S2Commands = new List<S2Command>();
            S2Operations = new List<S2Operation>();
            S2Functions = new List<S2Function>();
        }

        public void S2Parse(string file)
        {
            S2Function main = new S2Function();

            //run through every function...
        }

        public void Run(S2Function function)
        {
            // go through every function...
        }
    }
    public class S2Var
    {
        object value; 
    }
    public class S2Command
    {
        public void Run(int id, string[] args)
        {

        }
    }
    public class S2Operation
    {

    }
    public class S2Function
    {

    }
}
