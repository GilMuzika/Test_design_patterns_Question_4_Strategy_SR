using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test_design_patterns_Question_4_Strategy_SR
{
    public class WorkersFactory : IWorkersFactory
    {
        public Random Rnd { get; set; } = new Random();
        private string[] _namesToUsing = null;

        public static List<Worker> WorkerList { get; set; } = new List<Worker>();

        public WorkersFactory()
        {
            ReadFromFile();
        }

        private void ReadFromFile()
        {
            string names = string.Empty;
            try
            {
                names = File.ReadAllText("_names.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} \n\nSo the program will use the defult names");
                names = " Alfred Benny Connnor Daniel Eran ";
            }
            _namesToUsing = names.Split(new char[] { ' ', '\t', '\n' }).Where(x => !String.IsNullOrEmpty(x)).ToArray();
        }

        private string GenerateidentityNum()
        {
            StringBuilder strBldr = new StringBuilder();
            for(int i  = 0; i < 9; i++)
            {
                strBldr.Append(Rnd.Next(10));
            }
            return strBldr.ToString();

        }
            

        public Worker CreateRandomWorker()
        {
            string name = _namesToUsing[Rnd.Next(_namesToUsing.Length)];
            string identityNum = GenerateidentityNum();
            int age = Rnd.Next(18, 80);
            int salary = Rnd.Next(5000, 50000);
            string image = ImageProvider.GetResizedImageAs64BaseString(10);


            Worker worker = new Worker(name, identityNum, age, salary, image);
            lock(Rnd)
            {
                WorkerList.Add(worker);
            }
            return worker;
        }

        public async Task<List<Worker>> GenerateRandomWorkerList()
        {
            return await Task.Run(() => 
            {
                int num = Rnd.Next(50, 150);
                Worker w = null;
                for (int i = 0; i < num; i++)
                {
                    do
                    {
                        w = CreateRandomWorker();
                        if (!WorkerList.Contains(w))
                        {
                            lock (Rnd)
                            {
                                WorkerList.Add(w);
                            }
                        }
                    }
                    while (!WorkerList.Contains(w));
                    
                }
                return WorkerList;
            });
            

        }

        
    }
}
