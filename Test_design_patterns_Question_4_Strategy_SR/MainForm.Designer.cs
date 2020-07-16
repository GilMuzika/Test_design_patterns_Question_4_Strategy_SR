namespace Test_design_patterns_Question_4_Strategy_SR
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cmbWorkersList = new System.Windows.Forms.ComboBox();
            this.lblWorkerInfo = new System.Windows.Forms.Label();
            this.btnAddNewWorker = new System.Windows.Forms.Button();
            this.btnAddNewWorkerList = new System.Windows.Forms.Button();
            this.pbxImage = new System.Windows.Forms.PictureBox();
            this.btnCloneAndSort = new System.Windows.Forms.Button();
            this.cmbResult = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbWorkersList
            // 
            this.cmbWorkersList.FormattingEnabled = true;
            this.cmbWorkersList.Location = new System.Drawing.Point(13, 13);
            this.cmbWorkersList.Name = "cmbWorkersList";
            this.cmbWorkersList.Size = new System.Drawing.Size(212, 21);
            this.cmbWorkersList.TabIndex = 0;
            // 
            // lblWorkerInfo
            // 
            this.lblWorkerInfo.Location = new System.Drawing.Point(13, 143);
            this.lblWorkerInfo.Name = "lblWorkerInfo";
            this.lblWorkerInfo.Size = new System.Drawing.Size(157, 116);
            this.lblWorkerInfo.TabIndex = 1;
            this.lblWorkerInfo.Text = "label1";
            // 
            // btnAddNewWorker
            // 
            this.btnAddNewWorker.Location = new System.Drawing.Point(13, 307);
            this.btnAddNewWorker.Name = "btnAddNewWorker";
            this.btnAddNewWorker.Size = new System.Drawing.Size(138, 23);
            this.btnAddNewWorker.TabIndex = 2;
            this.btnAddNewWorker.Text = "Add New Worker";
            this.btnAddNewWorker.UseVisualStyleBackColor = true;
            // 
            // btnAddNewWorkerList
            // 
            this.btnAddNewWorkerList.Location = new System.Drawing.Point(13, 346);
            this.btnAddNewWorkerList.Name = "btnAddNewWorkerList";
            this.btnAddNewWorkerList.Size = new System.Drawing.Size(138, 23);
            this.btnAddNewWorkerList.TabIndex = 3;
            this.btnAddNewWorkerList.Text = "Add New Worker List";
            this.btnAddNewWorkerList.UseVisualStyleBackColor = true;
            // 
            // pbxImage
            // 
            this.pbxImage.Location = new System.Drawing.Point(16, 38);
            this.pbxImage.Name = "pbxImage";
            this.pbxImage.Size = new System.Drawing.Size(111, 48);
            this.pbxImage.TabIndex = 4;
            this.pbxImage.TabStop = false;
            // 
            // btnCloneAndSort
            // 
            this.btnCloneAndSort.Location = new System.Drawing.Point(279, 13);
            this.btnCloneAndSort.Name = "btnCloneAndSort";
            this.btnCloneAndSort.Size = new System.Drawing.Size(162, 23);
            this.btnCloneAndSort.TabIndex = 5;
            this.btnCloneAndSort.Text = "שכפל ומיין את הרשימה";
            this.btnCloneAndSort.UseVisualStyleBackColor = true;
            // 
            // cmbResult
            // 
            this.cmbResult.FormattingEnabled = true;
            this.cmbResult.Location = new System.Drawing.Point(279, 51);
            this.cmbResult.Name = "cmbResult";
            this.cmbResult.Size = new System.Drawing.Size(509, 21);
            this.cmbResult.TabIndex = 6;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbResult);
            this.Controls.Add(this.btnCloneAndSort);
            this.Controls.Add(this.pbxImage);
            this.Controls.Add(this.btnAddNewWorkerList);
            this.Controls.Add(this.btnAddNewWorker);
            this.Controls.Add(this.lblWorkerInfo);
            this.Controls.Add(this.cmbWorkersList);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pbxImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbWorkersList;
        private System.Windows.Forms.Label lblWorkerInfo;
        private System.Windows.Forms.Button btnAddNewWorker;
        private System.Windows.Forms.Button btnAddNewWorkerList;
        private System.Windows.Forms.PictureBox pbxImage;
        private System.Windows.Forms.Button btnCloneAndSort;
        private System.Windows.Forms.ComboBox cmbResult;
    }
}

