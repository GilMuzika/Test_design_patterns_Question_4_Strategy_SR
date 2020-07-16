using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.ComponentModel.Com2Interop;

namespace Test_design_patterns_Question_4_Strategy_SR
{
    public class Worker
    {
        public string Name { get; set; }
        public string Identity_Number { get; set; }        
        public int Salary { get; set; }
        public int Age { get; set; }
        public string Image { get; set; }

        public Worker(string name, string identityNum, int age, int salary, string image)
        {
            Name = name;
            Identity_Number = identityNum;
            Age = age;
            Salary = salary;
            Image = image;
        }

        public Worker()
        {
            Name = "default";            
            Identity_Number = "default";
            Age = -1;
            Salary = -1;
            Image = "default";

        }

        public override string ToString()
        {
            string str = string.Empty;
            foreach (var s in this.GetType().GetProperties())
                if(s.GetValue(this).ToString().Length < 200)
                str += $"{ s.Name}: { s.GetValue(this)}    \n";

            return str;
        }

        public static bool operator ==(Worker w1, Worker w2)
        {
            if (ReferenceEquals(w1, null) && ReferenceEquals(w2, null)) return true;

            if (ReferenceEquals(w1, null) || ReferenceEquals(w2, null)) return false;

            return w1.Name == w2.Name;
        }
        public static bool operator !=(Worker w1, Worker w2)
        {
            return !(w1 == w2);
        }

        public override bool Equals(object obj)
        {
            var thisType = obj as Worker;
            return this == thisType;
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(this.Identity_Number);
        }

    }
}
