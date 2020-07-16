using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_design_patterns_Question_4_Strategy;
using Test_design_patterns_Question_4_Strategy.strategy;

namespace Test_design_patterns_Question_4_Strategy_SR
{
    public partial class MainForm : Form
    {
        private IWorkersFactory _workersFactory = new WorkersFactory();

        public MainForm()
        {
            InitializeComponent();
            Initialize();
        }
        private void Initialize()
        {
            GlobalControlBundle.AddRange(this.Controls, this);

            this.Text = "Strategy";
            lblWorkerInfo.Text = string.Empty;
            lblWorkerInfo.Padding = new Padding(10);
            lblWorkerInfo.drawBorder(1, Color.Black);
            cmbResult.DropDownStyle = ComboBoxStyle.DropDown;

            btnAddNewWorker.Click += (object sender, EventArgs e) =>
            {
                lock (_workersFactory.Rnd)
                {
                    cmbWorkersList.Items.Add(new ComboItem<Worker>(_workersFactory.CreateRandomWorker()));
                }
            };

            btnAddNewWorkerList.Click += async (object sender, EventArgs e) =>
            {
                ((Button)sender).Enabled = false;                
                string btntext = ((Button)sender).Text;
                ((Button)sender).Text = "In progress...";
                cmbWorkersList.Enabled = false;
                cmbWorkersList.Text = "just wait...";

                var task = _workersFactory.GenerateRandomWorkerList();
                List<Worker> workers = await task;
                if (task.IsCompleted)
                {
                    ((Button)sender).Text = btntext;
                    ((Button)sender).Enabled = true;
                    cmbWorkersList.Enabled = true;
                    cmbWorkersList.Text = string.Empty;
                }
                foreach (var s in workers)
                {
                    lock (_workersFactory.Rnd)
                    {
                        cmbWorkersList.Items.Add(new ComboItem<Worker>(s));
                    }
                }
            };


            

            cmbWorkersList.SelectedIndexChanged += (object sender, EventArgs e) => 
            {
                Bitmap workerImage = ((sender as ComboBox).SelectedItem as ComboItem<Worker>).Item.Image.Base64StringToBitmap();
                pbxImage.Size = workerImage.Size;
                pbxImage.Image = workerImage;
                
                lblWorkerInfo.Text = ((sender as ComboBox).SelectedItem as ComboItem<Worker>).Item.ToString();
            };

            btnCloneAndSort.Click += async (object sender, EventArgs e) => 
            {
                (sender as Button).Enabled = false;
                string mySelftext = (sender as Button).Text;
                (sender as Button).Text = "נא להמתין";

                Task tsk = Task.Run(() => 
                {
                    new StrategySelector().SelectStrategy();
                });
                await tsk;
                if(tsk.IsCompleted)
                {
                    (sender as Button).Text = mySelftext;
                    (sender as Button).Enabled = true;
                }


            };

            cmbResult.SelectedIndexChanged += (object sender, EventArgs e) =>
            {
                Bitmap workerImage = ((sender as ComboBox).SelectedItem as Worker).Image.Base64StringToBitmap();
                pbxImage.Size = workerImage.Size;
                pbxImage.Image = workerImage;

                lblWorkerInfo.Text = ((sender as ComboBox).SelectedItem as Worker).ToString();
            };

            
        }

    }
}
