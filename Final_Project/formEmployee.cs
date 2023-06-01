using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Project.BSLayer;
namespace Final_Project
{
    public partial class formEmployee : Form
    {
        bool add = false;
        string err;
        BLEmployee emp = new BLEmployee();
        public formEmployee()
        {
            InitializeComponent();
        }
        void ResetText()
        {
            this.txteID.ResetText();
            this.txtName.ResetText();
            this.dtpDOB.ResetText();
            this.txtPhoneNumber.ResetText();
            this.txtIDcardnumber.ResetText();
            this.txtBaseSalary.ResetText();
            this.txtKPI.ResetText();
            this.dtpStartDay.ResetText();
            this.txtGrossSalary.ResetText();
            this.CBPosition.ResetText();
            this.txtPassword.ResetText();
            this.txtSearch.ResetText();
        }
        void LoadData()
        {
            try
            {
                dgvEmployee.DataSource = emp.getEmployee();
                // resize
                dgvEmployee.AutoResizeColumns();
                // xoa trong cac doi tuong trong panel
                ResetText();
                // disable save cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel1.Enabled = false;
                this.btnAdd.Enabled = true;
                this.btnUpdate.Enabled = true;
                this.btnDelete.Enabled = true;
            }
            catch
            {
                MessageBox.Show("FAILED TO LOAD DATA", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void formEmployee_Load(object sender, EventArgs e)
        {
            
            LoadData();
            emp.ResetKPI();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvEmployee_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvEmployee.CurrentCell.RowIndex;
            this.txteID.Text = dgvEmployee.Rows[r].Cells[0].Value.ToString();
            this.txtName.Text = dgvEmployee.Rows[r].Cells[1].Value.ToString();
            this.dtpDOB.Text = dgvEmployee.Rows[r].Cells[2].Value.ToString();
            this.txtPhoneNumber.Text = dgvEmployee.Rows[r].Cells[3].Value.ToString();
            this.txtIDcardnumber.Text = dgvEmployee.Rows[r].Cells[4].Value.ToString();
            this.dtpStartDay.Text = dgvEmployee.Rows[r].Cells[5].Value.ToString();
            this.txtBaseSalary.Text = dgvEmployee.Rows[r].Cells[6].Value.ToString();
            this.txtKPI.Text = dgvEmployee.Rows[r].Cells[7].Value.ToString();
            this.txtGrossSalary.Text = dgvEmployee.Rows[r].Cells[8].Value.ToString();
            this.CBPosition.Text = dgvEmployee.Rows[r].Cells[9].Value.ToString();
            this.txtPassword.Text = dgvEmployee.Rows[r].Cells[10].Value.ToString();
        }

        // ============================================================= BUTTON ADD ============================================================= //
        private void btnAdd_Click(object sender, EventArgs e)
        {
            add = true;
            // 
            ResetText();
            //
            this.panel1.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            //
            this.btnAdd.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = false;

            //
            this.txteID.Enabled = false;
            this.txtGrossSalary.Enabled = false;
            this.txtKPI.Enabled = false;
            this.dtpStartDay.Enabled = false;
            this.txtPassword.Enabled = false;
            this.txtName.Focus();
        }
        // ============================================================= BUTTON SAVE ============================================================= //
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(add)
            {
                try
                {
                    string baseStr = txtBaseSalary.Text;
                    int ebase;
                    if (!int.TryParse(baseStr, out ebase))
                    {
                        MessageBox.Show("BASE SALARY PRICE MUST BE INTERGER", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.txtBaseSalary.Focus();
                    }
                    else
                    {
                        emp.addEmployee(this.txtName.Text, this.dtpDOB.Value, this.txtPhoneNumber.Text, this.txtIDcardnumber.Text, ebase, 0, ebase, this.CBPosition.SelectedItem.ToString(), ref err);
                        LoadData();
                        MessageBox.Show("ADD SUCCESSFUILLY", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show("ADD FAILED", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                string baseStr = txtBaseSalary.Text;
                int ebase;
                if (!int.TryParse(baseStr, out ebase))
                {
                    MessageBox.Show("BASE SALARY PRICE MUST BE INTERGER", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.txtBaseSalary.Focus();
                }
                else
                {
                    string pass = this.txtPassword.Text.Trim();
                    emp.updateEmployee(this.txteID.Text, this.txtName.Text, this.dtpDOB.Value, this.txtPhoneNumber.Text, this.txtIDcardnumber.Text, ebase, int.Parse(this.txtKPI.Text), ebase + int.Parse(this.txtKPI.Text), this.CBPosition.Text, pass, ref err);
                    LoadData();
                    MessageBox.Show("UPDATE SUCCESSFUILLY", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }    
        }

        // ============================================================= BUTTON UPDATE ============================================================= //
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            add = false;

            //
            panel1.Enabled = true;
            txteID.Enabled = false;
            this.txtKPI.Enabled = false;
            this.txtGrossSalary.Enabled = false;

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnUpdate.Enabled = false;

            this.txtName.Focus();
            this.txtPassword.Enabled = true;
        }

        // ============================================================= BUTTON DELETE ============================================================= //
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // get record row
                int r = dgvEmployee.CurrentCell.RowIndex;
                // get cid
                string id = dgvEmployee.Rows[r].Cells[0].Value.ToString();
                DialogResult answer;
                answer = MessageBox.Show(string.Format("DELETE EMPLOYEE {0}?", id), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    try
                    {
                        emp.deleteEmployee(id, ref err);
                        LoadData();
                        MessageBox.Show("DELETE SUCCESSFULLY");
                    }
                    catch
                    {
                        MessageBox.Show("DELETE FAILED");
                    }
                }
            }
            catch
            {
                MessageBox.Show("DELETE FAILED");
            }
        }

        // ============================================================= BUTTON SEARCH ============================================================= //
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                txtSearch.Text = txtSearch.Text.Trim();
                List<Employee> search = emp.searchEmployee(txtSearch.Text);
                dgvEmployee.DataSource = search;
            }
        }

        // ============================================================= BUTTON EXIT ============================================================= //
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("EXIT ?", "ASK", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer == DialogResult.Yes)
                this.Close();
        }

        // ============================================================= BUTTON CANCEL ============================================================= //
        private void btnCancel_Click(object sender, EventArgs e)
        {

            ResetText();
            this.txtSearch.ResetText();

            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel1.Enabled = false;
            this.btnAdd.Enabled = true;
            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;
            dgvEmployee_CellClick(null, null);
        }
    }
}
