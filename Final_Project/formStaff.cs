using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Final_Project
{
    public partial class formStaff : Form
    {
        public formStaff()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var cus = new formCustomer();
            cus.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bill = new formBILL();
            bill.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var product= new formProduct();
            product.Show();
        }
    }
}
