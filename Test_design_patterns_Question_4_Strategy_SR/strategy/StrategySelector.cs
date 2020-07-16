using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_design_patterns_Question_4_Strategy_SR;
using Test_design_patterns_Question_4_Strategy_SR.strategy;

namespace Test_design_patterns_Question_4_Strategy.strategy
{
    public class StrategySelector
    {
        private IWorkersFactory _workersFactory = new WorkersFactory();
        private Worker[] _sortedAndClonedWorkers;

        public void SelectStrategy()
        {
            ComboBox cmbResult = (ComboBox)GlobalControlBundle.Get("cmbResult");

            List<Worker> clonedLst = new List<Worker>();
            for (int i = 0; i < WorkersFactory.WorkerList.Count; i++)
            {
                Worker cw = WorkersFactory.WorkerList[i].DeepClone();
                clonedLst.Add(WorkersFactory.WorkerList[i]);
            }

            Worker[] clonedArr = clonedLst.ToArray();

            Action a = null;
            CurrentContext context = null;
            string propertyName = string.Empty;
            if (clonedArr.Length < 100)
            {
                propertyName = "Identity Number";
                context = new CurrentContext(new Operation(new WorkerCompareByIdentityNumComparer()));
                _sortedAndClonedWorkers = context.ExecuteStrategy(clonedArr);
                a = () => { cmbResult.Text = $"Sorted by {propertyName}:"; };
                if (cmbResult.InvokeRequired) cmbResult.BeginInvoke(a);
                else a();
            }
            else if (clonedArr.Length > 200)
            {
                //context = new CurrentContext(new Operation(new WorkerCompireByNameComparer()));
                propertyName = "Name";
                context = new CurrentContext(new Operation(new ComparerByStringValuedProperty<Worker>(propertyName)));
                _sortedAndClonedWorkers = context.ExecuteStrategy(clonedArr);
                a = () => { cmbResult.Text = $"Sorted by {propertyName}:"; };
                if (cmbResult.InvokeRequired) cmbResult.BeginInvoke(a);
                else a();

            }
            else
            {
                propertyName = "Salary";
                context = new CurrentContext(new Operation(new ComparerByNumericValuedProperty<Worker>(propertyName)));
                _sortedAndClonedWorkers = context.ExecuteStrategy(clonedArr);
                a = () => { cmbResult.Text = "Sorted by salary:"; };
                if (cmbResult.InvokeRequired) cmbResult.BeginInvoke(a);
                else a();
            }


            a = () =>
            {
                cmbResult.Items.Clear();
                cmbResult.Items.AddRange(_sortedAndClonedWorkers);
            };
            if (cmbResult.InvokeRequired) cmbResult.BeginInvoke(a);
            else a();
        }
    }
}
