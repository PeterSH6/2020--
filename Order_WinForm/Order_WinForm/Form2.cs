using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Order_WinForm
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1 = (Form1)this.Owner;//将本窗体的拥有者强制设为Form1类的实例f1
            f1.orderList.Addorder(textBoxCreate.Text);
            this.Close();
        }
    }
}
