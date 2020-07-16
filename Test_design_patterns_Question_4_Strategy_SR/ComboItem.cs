using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_design_patterns_Question_4_Strategy_SR
{
    public class ComboItem<T>
    {
        public T Item { get; private set; }

        public ComboItem(T item)
        {
            Item = item;
        }

        public override string ToString()
        {
            return typeof(T).GetProperties().FirstOrDefault().GetValue(Item).ToString();
        }
    }
}
