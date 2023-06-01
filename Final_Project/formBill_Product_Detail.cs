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
    public partial class formBill_Product_Detail : Form
    {
        public BILL a = new BILL();
        public Product p = new Product();
        // ======================== KHAI BÁO ĐỂ LẤY THONG TIN ======================== //
        public BLBill bill = new BLBill();
        public BLProduct pro = new BLProduct();
        public BLEmployee emp = new BLEmployee();
        public int current_price;
        // ============================================================================ //
        bool add = false;
        string err;
        BLBillDetail billd = new BLBillDetail();

        public formBill_Product_Detail()
        {
            InitializeComponent();
            emp.ResetKPI(); 
            
        }
        public formBill_Product_Detail(BILL selectedBill)
        {
            InitializeComponent();
            a = selectedBill;

        }

        void ResetText()
        {
            this.txtbpQuantityProduct.ResetText();
            this.txtbpPrice.ResetText();
        }

        private void formBill_Product_Detail_Load(object sender, EventArgs e)
        {
            txtBID.Text = a.bID;
            this.cbpID.DataSource = pro.GetAllProductIDs();
            LoadData();
        }

        void LoadData()
        {
            try
            {
                dgvBillProductDetail.DataSource = billd.getBillDetail(a.bID);
                // resize
                dgvBillProductDetail.AutoResizeColumns();
                // xoa trong cac doi tuong trong panel
                ResetText();
                // disable save cancel
                this.btnSave.Enabled = false;
                this.btnCancel.Enabled = false;
                this.panel1.Enabled = false;
                this.btnAdd.Enabled = true;
                this.btnDelete.Enabled = true;
                //dgvCUSTOMER_CellClick(null, null);
            }
            catch
            {
                MessageBox.Show("FAILED TO LOAD DATA", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvBillProductDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvBillProductDetail.CurrentCell.RowIndex;
            this.txtBID.Text = dgvBillProductDetail.Rows[r].Cells[0].Value.ToString();
            this.cbpID.Text = dgvBillProductDetail.Rows[r].Cells[1].Value.ToString();
            this.txtbpPrice.Text = dgvBillProductDetail.Rows[r].Cells[2].Value.ToString();
            this.txtbpQuantityProduct.Text = dgvBillProductDetail.Rows[r].Cells[3].Value.ToString();
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
            this.btnDelete.Enabled = false;

            //
            this.txtBID.Enabled = false;
            this.txtbpQuantityProduct.Enabled = true;
            this.txtbpPrice.Enabled = false;
        }

        private void cbpID_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pid = cbpID.SelectedValue.ToString();
            current_price = pro.GetPriceOfProduct(pid) * 2;
            txtbpPrice.Text = current_price.ToString();
        }


        // ============================================================= BUTTON SAVE ============================================================= //
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (add)
            {
                try
                {
                    p = pro.GetProduct(cbpID.SelectedItem.ToString());
                    if(p.pQuantity >= int.Parse(txtbpQuantityProduct.Text))
                    {
                        billd.addBillDetail(a.bID, cbpID.SelectedItem.ToString(), current_price, int.Parse(txtbpQuantityProduct.Text));
                        pro.updateQuantityProduct(p.pID, int.Parse(txtbpQuantityProduct.Text));
                        bill.updateBillTotalPrice(a.bID, int.Parse(txtbpQuantityProduct.Text)*current_price);
                        emp.UpdateAddKPI(a.eID, int.Parse(txtbpQuantityProduct.Text) * current_price);
                        emp.UpdateGrossSalary(a.eID, emp.GetBase(a.eID), emp.GetKPI(a.eID));
                        LoadData();
                        MessageBox.Show("ADD SUCCESSFUILLY", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else MessageBox.Show("QUANTITY IS NOT ENOUGH", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    MessageBox.Show("ADD FAILED", "FAIL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ============================================================= BUTTON DELETE ============================================================= //
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // get record row
                int r = dgvBillProductDetail.CurrentCell.RowIndex;
                // get cid
                string bid = dgvBillProductDetail.Rows[r].Cells[0].Value.ToString();
                string pid = dgvBillProductDetail.Rows[r].Cells[1].Value.ToString();
                int quantity = int.Parse(dgvBillProductDetail.Rows[r].Cells[2].Value.ToString());
                int price = int.Parse(dgvBillProductDetail.Rows[r].Cells[3].Value.ToString());
                DialogResult answer;
                answer = MessageBox.Show(string.Format("DELETE PRODUCT {0} FROM BILL?", pid), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    try
                    {
                        billd.deleteBillDetai(bid, pid, ref err);
                        pro.updateIncreaseQuantityProduct(p.pID, quantity);
                        bill.updateDecreaseBillTotalPrice(a.bID, quantity * price);
                        emp.UpdateRemoveKPI(a.eID, quantity * price);
                        emp.UpdateGrossSalary(a.eID, emp.GetBase(a.eID), emp.GetKPI(a.eID));
                        LoadData();
                        MessageBox.Show("DELETE SUCCESSFUILLY", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void button1_Click(object sender, EventArgs e)
        {
            BillDetailReport print = new BillDetailReport(a.bID);
            print.Show();

        }
    }
}
