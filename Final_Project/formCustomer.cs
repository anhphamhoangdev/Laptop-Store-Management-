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
    public partial class formCustomer : Form
    {
        bool add = false;
        string err;
        BLCustomer customer = new BLCustomer();
        public formCustomer()
        {
            InitializeComponent();
        }
        void ResetText()
        {
            this.txtcID.ResetText();
            this.txtcName.ResetText();
            this.txtcPhoneNum.ResetText();
            this.txtcAddress.ResetText();
            this.txtcEmail.ResetText();
        }
        void LoadData()
        {
            try
            {
                dgvCUSTOMER.DataSource = customer.getCustomer();
                // resize
                dgvCUSTOMER.AutoResizeColumns();
                // xoa trong cac doi tuong trong panel
                ResetText();
                // disable save cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel1.Enabled = false;
                this.btnAdd.Enabled = true;
                this.btnUpdate.Enabled = true;
                this.btnDelete.Enabled = true;
                //dgvCUSTOMER_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("FAILED TO LOAD DATA","FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void formCustomer_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void dgvCUSTOMER_CellClick(object sender, DataGridViewCellEventArgs e)
        {
                int r = dgvCUSTOMER.CurrentCell.RowIndex;
                this.txtcID.Text = dgvCUSTOMER.Rows[r].Cells[0].Value.ToString();
                this.txtcName.Text = dgvCUSTOMER.Rows[r].Cells[1].Value.ToString();
                this.txtcPhoneNum.Text = dgvCUSTOMER.Rows[r].Cells[2].Value.ToString();
                this.txtcEmail.Text = dgvCUSTOMER.Rows[r].Cells[3].Value.ToString();
                this.txtcAddress.Text = dgvCUSTOMER.Rows[r].Cells[4].Value.ToString();
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
            this.btnCancel.Enabled=true;
            //
            this.btnAdd.Enabled = false;
            this.btnUpdate.Enabled = false;
            this.btnDelete.Enabled = false;

            //
            this.txtcID.Enabled = false;
            this.txtcName.Focus();
        }

        // ============================================================= BUTTON SAVE ============================================================= //
        private void btnSave_Click(object sender, EventArgs e)
        {
            if(add)
            {
                try
                {
                    
                    customer.addCustomer(this.txtcName.Text, this.txtcPhoneNum.Text, this.txtcEmail.Text, this.txtcAddress.Text, ref err);
                    LoadData();
                    MessageBox.Show("ADD SUCCESSFUILLY", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("ADD FAILED", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                customer.updateCustomer(txtcID.Text, txtcName.Text, txtcPhoneNum.Text, txtcEmail.Text, txtcAddress.Text, ref err);
                LoadData();
                MessageBox.Show("UPDATE SUCCESSFULLY");
            }
        }

        // ============================================================= BUTTON DELETE ============================================================= //
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // get record row
                int r = dgvCUSTOMER.CurrentCell.RowIndex;
                // get cid
                string id = dgvCUSTOMER.Rows[r].Cells[0].Value.ToString();
                DialogResult answer;
                answer = MessageBox.Show(string.Format("DELETE EMPLOYEE {0}?", id), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    try
                    {
                        customer.deleteCustomer(id, ref err);
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

        // ============================================================= BUTTON UPDATE ============================================================= //
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            add = false;

            //
            panel1.Enabled = true;
            txtcID.Enabled = false;

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnUpdate.Enabled = false;

            this.txtcName.Focus();
        }

        // ============================================================= BUTTON SEARCH ============================================================= //
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if(txtSearch.Text != "")
            {
                txtSearch.Text = txtSearch.Text.Trim();
                List<Customer> searchCustomer = customer.searchCustomers(txtSearch.Text);
                dgvCUSTOMER.DataSource = searchCustomer;
            }    
        }

        // ============================================================= BUTTON EXIT ============================================================= //
        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("EXIT ?", "ASK", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if(answer == DialogResult.Yes)
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
            dgvCUSTOMER_CellClick(null, null);
        }
    }
}
