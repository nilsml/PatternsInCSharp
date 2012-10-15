using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    public class Decisions
    {
        public void MakeDecision(int a)
        {
            switch (a)
            {
                case 1:
                    Console.WriteLine("Doing something");
                    break;
                case 2:
                    Debug.WriteLine("Doing something else");
                    break;
                case 9:
                    return;
            }
        }

        Dictionary<int, Action> _strategies = new Dictionary<int, Action>(); 
        private void InitializeStrategies()
        {
            _strategies[1] = () => Console.WriteLine("Doing something");
            _strategies[2] = () => Debug.WriteLine("Doing something else");
            _strategies[9] = () =>
                                 {
                                     Console.WriteLine("Jadda");
                                     Debug.WriteLine("Hey");
                                 };
        }

        public void MakeDecisionRefactored(int i)
        {
            _strategies[i]();
        }
    }
}
