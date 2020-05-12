using OrderItems;
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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void buttonMod_Click(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            f1 = (Form1)this.Owner;
            f1.orderList.Modiforder(int.Parse(textBoxID.Text),
                comboBoxType.SelectedItem.ToString(),
                (OrderItem)comboBoxItem.SelectedItem,
                int.Parse(textBoxNum.Text));
            this.Close();
        }
    }
}
