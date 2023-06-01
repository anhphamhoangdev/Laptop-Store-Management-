using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
namespace Final_Project
{
    public partial class formLOGIN : Form
    {
        public formLOGIN()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (var context = new QLBMTEntities())
            {
                var employee = context.Employees.SingleOrDefault(emp => emp.eID == txtID.Text && emp.ePassword == txtPassword.Text);

                if (employee != null)
                {
                    string position = employee.ePosition.Trim();
                    if (position == "Manager")
                    {
                        var managementForm = new formManagement();
                        managementForm.Show();
                    }
                    else
                    {
                        var staffForm = new formStaff();
                        staffForm.Show();
                    }
                    this.Hide();
                }
                else
                    MessageBox.Show("ID OR PASS IS NOT CORRECT");
                
            }
        }
    }
}
