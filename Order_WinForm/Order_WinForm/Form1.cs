using OrderItems;
using Orders;
using OrderServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Items;

namespace Order_WinForm
{
    public partial class Form1 : Form
    {
        private OrderService Service;
        public OrderService orderList { get => Service; set => Service = value; }

        
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            orderList = new OrderService();
            orderList.Addorder("Peter");
            orderList.Addorder("Sam");
            orderList.Addorder("Bob");
            orderList.Modiforder("1", "add", Item.Apple, 3);
            orderList.Modiforder("1", "add", Item.Banana, 1);
            orderList.Modiforder("1", "add", Item.Pen, 1);
            orderList.Modiforder("1", "add", Item.Bottle, 1);
            orderList.Modiforder("1", "add", Item.Egg, 1);
            orderBindingSource.DataSource = orderList.orders;
            orderBindingSource.ResetBindings(true);

        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            Order temp = (Order)orderBindingSource.Current;
            orderList.Delorder(temp.orderID);
            orderBindingSource.ResetBindings(true);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog(this);//这个this必不可少（将窗体显示为具有指定所有者：窗体f2的所有者是Form1类当前的对象）
            orderBindingSource.ResetBindings(true);
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.ShowDialog(this);
            orderItemBindingSource.ResetBindings(true);

        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            if(comboBoxID.SelectedItem.ToString() == "OrderID")
            {
                orderBindingSource.DataSource = orderList.SearchOrderID(textBoxSearch.Text);
            }
            else if(comboBoxID.SelectedItem.ToString() == "CustomID")
            {
                orderBindingSource.DataSource = orderList.SearchCustomerID(textBoxSearch.Text);
            }
            else if ( comboBoxID.SelectedItem.ToString() == "ItemID")
            {
                orderBindingSource.DataSource = orderList.SearchItemID(textBoxSearch.Text);
            }

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {

            //设置文件类型
            saveFileDialog1.Filter = "XML files(.xml)|*.xml|all Files(*.*)|*.*"; ;
            //保存对话框是否记忆上次打开的目录
            saveFileDialog1.RestoreDirectory = true;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //获得文件路径
                string localFilePath = saveFileDialog1.FileName.ToString();
                orderList.Export(localFilePath);
            }
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "XML files(.xml)|*.xml|all Files(*.*)|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = openFileDialog1.FileName.ToString();
                orderList.Import(localFilePath);
                orderBindingSource.ResetBindings(true);

            }

        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            orderBindingSource.DataSource = orderList.orders;
            orderBindingSource.ResetBindings(true);
        }
    }
}
