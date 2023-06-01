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
    public partial class formProduct : Form
    {
        bool add = false;
        string err;
        BLProduct product = new BLProduct();
        public formProduct()
        {
            InitializeComponent();
        }
        void ResetText()
        {
            this.txtpID.ResetText();
            this.txtpName.ResetText();
            this.txtpBrand.ResetText();
            this.txtpDescription.ResetText();
            this.txtpWarranty_time.ResetText();
            this.txtpQuantity.ResetText();
            this.txtpPrice.ResetText();
            this.txtSearch.ResetText();
            this.txtMinPrice.ResetText();
            this.txtMaxPrice.ResetText();
        }
        void LoadData()
        {
            try
            {
                dgvPRODUCT.DataSource = product.getProduct();
                // resize
                dgvPRODUCT.AutoResizeColumns();
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
        private void formProduct_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }


        private void dgvPRODUCT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int r = dgvPRODUCT.CurrentCell.RowIndex;
            this.txtpID.Text = dgvPRODUCT.Rows[r].Cells[0].Value.ToString();
            this.txtpName.Text = dgvPRODUCT.Rows[r].Cells[1].Value.ToString();
            this.txtpBrand.Text = dgvPRODUCT.Rows[r].Cells[2].Value.ToString();
            this.txtpDescription.Text = dgvPRODUCT.Rows[r].Cells[3].Value.ToString();
            this.txtpWarranty_time.Text = dgvPRODUCT.Rows[r].Cells[4].Value.ToString();
            this.txtpQuantity.Text = dgvPRODUCT.Rows[r].Cells[5].Value.ToString();
            this.txtpPrice.Text = dgvPRODUCT.Rows[r].Cells[6].Value.ToString();
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
            this.txtpID.Enabled = false;
            this.txtpName.Focus();
        }

        // ============================================================= BUTTON SAVE ============================================================= //
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (add)
            {
                try
                {
                    string quantityStr = txtpQuantity.Text;
                    string warrantyStr = txtpWarranty_time.Text;
                    string priceStr = txtpPrice.Text;
                    int quantity;
                    int warranty;
                    int price;
                    if (!int.TryParse(quantityStr, out quantity) || !int.TryParse(warrantyStr, out warranty) || !int.TryParse(priceStr, out price))
                    {
                        MessageBox.Show("WARRANTY TIME , QUANTITY, PRICE MUST BE INTERGER", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        product.addProduct(this.txtpName.Text, this.txtpBrand.Text, this.txtpDescription.Text, warranty, quantity, price, ref err);
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
                string quantityStr = txtpQuantity.Text;
                string warrantyStr = txtpWarranty_time.Text;
                string priceStr = txtpPrice.Text;
                int quantity;
                int warranty;
                int price;
                if (!int.TryParse(quantityStr, out quantity) || !int.TryParse(warrantyStr, out warranty) || !int.TryParse(priceStr, out price))
                {
                    MessageBox.Show("WARRANTY TIME , QUANTITY, PRICE MUST BE INTERGER", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    product.updateProduct(this.txtpID.Text, this.txtpName.Text, this.txtpBrand.Text, this.txtpDescription.Text, warranty, quantity, price, ref err);
                    LoadData();
                    MessageBox.Show("UPDATE SUCCESSFUILLY", "DONE", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // ============================================================= BUTTON DELETE ============================================================= //
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // get record row
                int r = dgvPRODUCT.CurrentCell.RowIndex;
                // get cid
                string id = dgvPRODUCT.Rows[r].Cells[0].Value.ToString();
                DialogResult answer;
                answer = MessageBox.Show(string.Format("DELETE PRODUCT {0}?", id), "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    try
                    {
                        product.deleteProduct(id, ref err);
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
            txtpID.Enabled = false;

            this.btnSave.Enabled = true;
            this.btnCancel.Enabled = true;

            this.btnAdd.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnUpdate.Enabled = false;

            this.txtpName.Focus();
        }

        // ============================================================= BUTTON SEARCH ============================================================= //
        private void btnSearch_Click(object sender, EventArgs e)
        {
            int min;
            int max;
            if (txtMinPrice.Text == "")
            {
                min = 0;
            } 
            else
            {
                string minStr = txtMinPrice.Text;
                if (!int.TryParse(minStr, out min))
                {
                    MessageBox.Show("MIN PRICE MUST BE INTERGER", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    
                }
            }
            if (txtMaxPrice.Text == "")
            {
                max = int.MaxValue;
            }
            else
            {
                string maxStr = txtMaxPrice.Text;
                if (!int.TryParse(maxStr, out max))
                {
                    MessageBox.Show("MAX PRICE MUST BE INTERGER", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (txtSearch.Text != "")
            {
                txtSearch.Text = txtSearch.Text.Trim();
                List<Product> searchProdcut = product.searchProducts(txtSearch.Text, min, max);
                dgvPRODUCT.DataSource = searchProdcut;
            }
            else
            {
                List<Product> searchProdcut = product.searchProducts(min, max);
                dgvPRODUCT.DataSource = searchProdcut;
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
            dgvPRODUCT_CellClick(null, null);
        }

       
    }
}
