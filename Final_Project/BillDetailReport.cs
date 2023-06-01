using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Final_Project.Reporting;
namespace Final_Project
{
    public partial class BillDetailReport : Form
    {
        public string bid;
 
        public BillDetailReport()
        {
            InitializeComponent();
        }
        public BillDetailReport(string bid)
        {
            InitializeComponent();
            this.bid = bid;
        }

        private void BillDetailReport_Load(object sender, EventArgs e)
        {
            if(bid != null)
            BillDetailReport_Form();
        }

        private void BillDetailReport_Form()
        {
            using( var context = new QLBMTEntities())
            {

                string cmd = "select bdt.bID, b.bBUY_Date, b.bTotalPrice, c.cName, e.eName, bdt.pID , p.pName, bdt.bpQuantity_Product, bdt.bpPrice,  bdt.bpQuantity_Product*bdt.bpPrice as 'SUM' " +
                             "from Bill_Product_Detail bdt inner join Product p on bdt.pID = p.pID " +
                             "inner join Bill b on bdt.bID = b.bID " +
                             "inner join Customer c on b.cID = c.cID " +
                             "inner join Employee e on b.eID = e.eID " +
                             "where bdt.bID = @bID";

                List<ProductDetailReport> listproductbill = context.Database.SqlQuery<ProductDetailReport>(cmd, new SqlParameter("@bID", bid)).ToList();
                this.rpvBillDetail.LocalReport.ReportPath = "Report1.rdlc";
                var reportDataSource = new ReportDataSource("DataSet1", listproductbill);
                this.rpvBillDetail.LocalReport.DataSources.Clear();
                this.rpvBillDetail.LocalReport.DataSources.Add(reportDataSource);
                this.rpvBillDetail.RefreshReport();
            }
        }
    }
}
