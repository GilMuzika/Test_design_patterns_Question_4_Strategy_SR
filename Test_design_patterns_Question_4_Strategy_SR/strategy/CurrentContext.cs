using System;
using System.Collections.Generic;
using System.Text;

namespace Test_design_patterns_Question_4_Strategy_SR.strategy
{
    public class CurrentContext 
    {
        private IStrategy _strtegy;

        public CurrentContext(IStrategy strategy)
        {
            this._strtegy = strategy;
        }

        public Worker[] ExecuteStrategy(Worker[] workers)
        {
            return _strtegy.DoOperation(workers);
        }
    }
}
