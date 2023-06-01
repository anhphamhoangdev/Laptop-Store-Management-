using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Final_Project.BSLayer
{
    public class BLBillDetail
    {
        public DataTable getBillDetail(string bid)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var tps = from p in ql.Bill_Product_Detail where p.bID == bid select p;
            DataTable dt = new DataTable();
            dt.Columns.Add("BILL ID");
            dt.Columns.Add("PRODUCT ID");
            dt.Columns.Add("PRODUCT PRICE");
            dt.Columns.Add("QUANTITY");
            foreach (var p in tps)
            {
                dt.Rows.Add(p.bID, p.pID, p.bpPrice, p.bpQuantity_Product);
            }
            return dt;
        }

        // ============================================================= ADD BILL DETAIL ============================================================= //
        public bool addBillDetail(string bid, string pid, int price, int quantity)
        {
            QLBMTEntities ql = new QLBMTEntities();
            Bill_Product_Detail b = new Bill_Product_Detail();
            b.bID = bid;
            b.pID = pid;
            b.bpPrice = price;
            b.bpQuantity_Product = quantity;
            ql.Bill_Product_Detail.Add(b);
            ql.SaveChanges();
            return true;
        }

        // ============================================================= DELETE BILL DETAIL ============================================================= //
        public bool deleteBillDetai(string bID, string pID, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var billDetail = ql.Bill_Product_Detail.Find(bID, pID);
            ql.Bill_Product_Detail.Attach(billDetail);
            ql.Bill_Product_Detail.Remove(billDetail);
            ql.SaveChanges();
            return true;
        }

        

    }
}
