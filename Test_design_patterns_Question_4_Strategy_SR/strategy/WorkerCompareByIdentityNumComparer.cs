using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_design_patterns_Question_4_Strategy_SR.strategy
{
    class WorkerCompareByIdentityNumComparer : IComparer<Worker>
    {
        public int Compare(Worker x, Worker y)
        {
            int num1 = Convert.ToInt32(x.Identity_Number);
            int num2 = Convert.ToInt32(y.Identity_Number);

            return num1 - num2;

           

        }
    }
}
