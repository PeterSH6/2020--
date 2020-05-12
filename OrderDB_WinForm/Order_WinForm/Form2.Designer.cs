namespace Order_WinForm
{
    partial class Form2
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.labelCustomID = new System.Windows.Forms.Label();
            this.textBoxCreate = new System.Windows.Forms.TextBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.labelCustomID);
            this.flowLayoutPanel1.Controls.Add(this.textBoxCreate);
            this.flowLayoutPanel1.Controls.Add(this.buttonCreate);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(8);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(926, 130);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // labelCustomID
            // 
            this.labelCustomID.AutoSize = true;
            this.labelCustomID.Location = new System.Drawing.Point(38, 38);
            this.labelCustomID.Margin = new System.Windows.Forms.Padding(30);
            this.labelCustomID.Name = "labelCustomID";
            this.labelCustomID.Size = new System.Drawing.Size(106, 24);
            this.labelCustomID.TabIndex = 2;
            this.labelCustomID.Text = "用户名：";
            // 
            // textBoxCreate
            // 
            this.textBoxCreate.Location = new System.Drawing.Point(204, 38);
            this.textBoxCreate.Margin = new System.Windows.Forms.Padding(30);
            this.textBoxCreate.Name = "textBoxCreate";
            this.textBoxCreate.Size = new System.Drawing.Size(442, 35);
            this.textBoxCreate.TabIndex = 0;
            // 
            // buttonCreate
            // 
            this.buttonCreate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonCreate.Location = new System.Drawing.Point(706, 38);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(30);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(115, 35);
            this.buttonCreate.TabIndex = 1;
            this.buttonCreate.Text = "创建";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 130);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "Form2";
            this.Text = "Form2";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox textBoxCreate;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Label labelCustomID;
    }
}