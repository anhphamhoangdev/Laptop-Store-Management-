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
    public partial class formBILL : Form
    {
        // ======================== KHAI BÁO ĐỂ LẤY CID VÀ EID ======================== //
        public BLEmployee emp = new BLEmployee();
        public BLCustomer cus = new BLCustomer();
        public string currenteid;
        public string currentbid;
        // ============================================================================ //
        bool add = false;
        string err;
        public BLBill bill = new BLBill();
        public formBILL()
        {
            InitializeComponent();
        }
        void ResetText()
        {
            this.txtbID.ResetText();
            this.cbcID.ResetText();
            this.cbeID.ResetText();
            this.dtpBuyDate.ResetText();
            this.txtbTotalPrice.ResetText();
            this.txtSearch.ResetText();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            emp.ResetKPI();
            this.cbeID.DataSource = emp.GetAllEmployeeIDs();
            this.cbcID.DataSource = cus.GetAllCustomerIDs();
            LoadData();
        }

        void LoadData()
        {
            try
            {
                dgvBILL.DataSource = bill.getBill();
                // resize
                dgvBILL.AutoResizeColumns();
                // xoa trong cac doi tuong trong panel
                ResetText();
                // disable save cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel1.Enabled = false;
                this.btnAdd.Enabled = true;
                this.btnUpdate.Enabled = true;
                this.btnDelete.Enabled = true;
                this.btnBD.Enabled = false;
                //dgvCUSTOMER_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("FAILED TO LOAD DATA", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void dgvBILL_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvBILL.CurrentCell.RowIndex;
            this.txtbID.Text = dgvBILL.Rows[r].Cells[0].Value.ToString();
            this.cbcID.Text = dgvBILL.Rows[r].Cells[1].Value.ToString();
            this.cbeID.Text = dgvBILL.Rows[r].Cells[2].Value.ToString();
            this.dtpBuyDate.Text = dgvBILL.Rows[r].Cells[3].Value.ToString();
            this.txtbTotalPrice.Text = dgvBILL.Rows[r].Cells[4].Value.ToString();
            this.btnBD.Enabled = true;
            currenteid = dgvBILL.Rows[r].Cells[2].Value.ToString();
            currentbid = dgvBILL.Rows[r].Cells[0].Value.ToString();
     
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult answer = MessageBox.Show("EXIT ?", "ASK", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer == DialogResult.Yes)
                this.Close();
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
            this.txtbID.Enabled = false;
            this.txtbTotalPrice.Enabled = false;
            this.dtpBuyDate.Enabled = false;
        }

        // ============================================================= BUTTON UPDATE ============================================================= //
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            add = false;

            //
            panel1.Enabled = true;
            txtbID.Enabled = false;
            txtbTotalPrice.Enabled=false;
            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;
            this.btnAdd.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnUpdate.Enabled = false;
        }

        // ============================================================= BUTTON SAVE ============================================================= //
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (add)
            {
                try
                {

                    bill.addBill(cbcID.SelectedItem.ToString(), cbeID.SelectedItem.ToString(), dtpBuyDate.Value, 0, ref err);
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
                emp.UpdateRemoveKPI(currenteid, int.Parse(this.txtbTotalPrice.Text));
                bill.updateBill(txtbID.Text, cbcID.SelectedItem.ToString(), cbeID.SelectedItem.ToString(), dtpBuyDate.Value, int.Parse(txtbTotalPrice.Text), ref err);
                emp.UpdateAddKPI(this.cbcID.SelectedItem.ToString(), int.Parse(txtbTotalPrice.Text));
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
                int r = dgvBILL.CurrentCell.RowIndex;
                // get cid
                string id = dgvBILL.Rows[r].Cells[0].Value.ToString();
                DialogResult answer;
                answer = MessageBox.Show(string.Format("DELETE BILL {0}?", id), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    try
                    {
                        emp.UpdateRemoveKPI(currenteid, int.Parse(this.txtbTotalPrice.Text));
                        emp.UpdateGrossSalary(currenteid, emp.GetBase(currenteid), emp.GetKPI(currenteid));
                        bill.deleteBill(id, ref err);
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

        // ============================================================= BUTTON CANCEL ============================================================= //
        private void btnCancel_Click(object sender, EventArgs e)
        {

            ResetText();
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            this.panel1.Enabled = false;
            this.btnAdd.Enabled = true;
            this.btnUpdate.Enabled = true;
            this.btnDelete.Enabled = true;
            dgvBILL_CellClick(null, null);
        }

        // ============================================================= BUTTON SEARCH ============================================================= //
        private void btnSearch_Click(object sender, EventArgs e)
        {
                List<BILL> search = bill.SearchBillsByDate(txtSearch.Text);
                dgvBILL.DataSource = search;
            
        }

        private void btnBD_Click(object sender, EventArgs e)
        {
            BILL selectedBill = bill.GetBill(currentbid);
            formBill_Product_Detail a = new formBill_Product_Detail(selectedBill);
            a.Show();
        }

        

    }
}
