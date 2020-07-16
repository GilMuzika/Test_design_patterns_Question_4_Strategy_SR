using System;
using System.Collections.Generic;
using System.Text;

namespace Test_design_patterns_Question_4_Strategy_SR.strategy
{
    public interface IStrategy
    {
        Worker[] DoOperation(Worker[] workers);
    }
}
