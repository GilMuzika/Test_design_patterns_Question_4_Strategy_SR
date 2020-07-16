using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_design_patterns_Question_4_Strategy_SR
{
    public interface IWorkersFactory
    {
        Random Rnd { get; set; }

        Worker CreateRandomWorker();
        Task<List<Worker>> GenerateRandomWorkerList();
    }
}
