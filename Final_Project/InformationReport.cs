using Final_Project.BSLayer;
using Final_Project.Reporting;
using Final_Project.InformationReporting;
using Microsoft.Reporting.WinForms;
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

    public partial class InformationReport : Form
    {
        BLBill bill = new BLBill();
        BLEmployee emp = new BLEmployee();
        public InformationReport()
        {
            InitializeComponent();
        }

        private void InformationReport_Load(object sender, EventArgs e)
        {
            InformationReportForm();
        }

        private void InformationReportForm()
        {
            using (var context = new QLBMTEntities())
            {
                double costprice = (double)bill.SumofbTotalPrice() / 2;
                int btotalprice = bill.SumofbTotalPrice();
                int egrosssalary = emp.SumofeGrosssalary();
                int profit = btotalprice - egrosssalary;
                // ========================================================== //
                Information a = new Information();
                a.cosprice = costprice;
                a.revenue = btotalprice;
                a.profit = profit;
                a.egrossalary = egrosssalary;
                a.bBuy_Date1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                a.bBuy_Date2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));
                List<Information> listinformation = new List<Information>();
                listinformation.Add(a);
                this.rpvInfor.LocalReport.ReportPath = "Report2.rdlc";
                var reportDataSource = new ReportDataSource("DataSet1", listinformation);
                this.rpvInfor.LocalReport.DataSources.Clear();
                this.rpvInfor.LocalReport.DataSources.Add(reportDataSource);
                this.rpvInfor.RefreshReport();
            }
        }
    }
}
