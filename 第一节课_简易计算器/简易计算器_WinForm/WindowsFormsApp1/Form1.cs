using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double first = 0;
            double last = 0;
            double result = 0;
            try
            {
                first = double.Parse(textBox1.Text);
                last = double.Parse(textBox2.Text);
                switch (comboBox1.SelectedItem.ToString())
                {
                    case "+":
                        result = first + last;
                        label4.Text = Convert.ToString(result);
                        break;
                    case "-":
                        result = first - last;
                        label4.Text = Convert.ToString(result);
                        break;
                    case "*":
                        result = first * last;
                        label4.Text = Convert.ToString(result);
                        break;
                    case "/":
                        result = first / last;
                        label4.Text = Convert.ToString(result);
                        break;
                }
            }
  
            catch (OverflowException)
            {
                label4.Text = "数据太大溢出";
                textBox1.Focus();
            }
            catch (FormatException)
            {
                label4.Text = "输入格式有误";
                textBox1.Focus();
            }
            catch (InvalidCastException)
            {
                label4.Text = "输入格式有误";
                textBox1.Focus();
            }
        }
    }
}
