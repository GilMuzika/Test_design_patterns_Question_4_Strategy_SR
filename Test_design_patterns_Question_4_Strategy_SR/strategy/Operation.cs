using System;
using System.Collections.Generic;
using System.Text;

namespace Test_design_patterns_Question_4_Strategy_SR.strategy
{
    public class Operation : IStrategy
    {
        private IComparer<Worker> _comparer;

        public Operation(IComparer<Worker> comparer)
        {
            _comparer = comparer;
        }

        public Worker[] DoOperation(Worker[] workers)
        {
            Array.Sort(workers, _comparer);
            return workers;
        }
    }
}
