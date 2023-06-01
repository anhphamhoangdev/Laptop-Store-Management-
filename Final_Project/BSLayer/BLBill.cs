using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;

namespace Final_Project.BSLayer
{
    public class BLBill
    {
        public DataTable getBill()
        {
            QLBMTEntities ql = new QLBMTEntities();
            var tps = from p in ql.BILLs select p;
            DataTable dt = new DataTable();
            dt.Columns.Add("BILL ID");
            dt.Columns.Add("CUSTOMER ID");
            dt.Columns.Add("EMPLOYEE ID");
            dt.Columns.Add("NUY DATE");
            dt.Columns.Add("TOTAL PRICE");
            foreach (var p in tps)
            {
                dt.Rows.Add(p.bID, p.cID, p.eID, p.bBUY_Date, p.bTotalPrice);
            }
            return dt;
        }

        // ============================================================= ADD BILL ============================================================= //
        public bool addBill(string cid, string eid, DateTime buy_date, int toltalprice, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            BILL b = new BILL();
            b.bID = GenerateID();
            b.cID = cid;
            b.eID = eid;
            b.bBUY_Date = buy_date;
            b.bTotalPrice = toltalprice;
            ql.BILLs.Add(b);
            ql.SaveChanges();
            return true;
        }
        public string GenerateID() // auto create ID
        {
            int maxID = GetMaxID();
            int currentID = maxID + 1;
            string productID = "b" + currentID.ToString().PadLeft(5, '0');
            return productID;
        }
        public int GetMaxID()
        {
            using (var context = new QLBMTEntities())
            {
                var emp = (from p in context.BILLs
                           orderby p.bID descending
                           select p).FirstOrDefault();


                if (emp != null)
                {
                    return int.Parse(emp.bID.Substring(1));
                }
                else
                {
                    return 0;
                }
            }
        }
        // ============================================================= DELETE BILL ============================================================= //
        public bool deleteBill(string id, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            BILL b = new BILL();
            b.bID = id;
            ql.BILLs.Attach(b);
            ql.BILLs.Remove(b);
            ql.SaveChanges();
            return true;
        }

        // ============================================================= UPDATE BILL ============================================================= //
        public bool updateBill(string bid, string cid, string eid, DateTime buy_date, int toltalprice, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var bill = (from b in ql.BILLs
                            where b.bID == bid
                            select b).SingleOrDefault();
            if (bill != null)
            {
                bill.bID = bid;
                bill.cID = cid;
                bill.eID = eid;
                bill.bBUY_Date = buy_date;
                bill.bTotalPrice = toltalprice;
                ql.SaveChanges();
            }
            return true;
        }

        // ============================================================= SEARCH BILL ============================================================= //
        public List<BILL> SearchBillsByDate(string key)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var bills = from b in ql.BILLs
                        where b.bID.Contains(key)
                        select b;
            return bills.ToList();
        }

        // ============================================================= GET BILL TO BILL DETAIL ============================================================= //
        public BILL GetBill(string bid)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var emp = (from e in ql.BILLs
                       where e.bID == bid
                       select e).SingleOrDefault();
            if(emp != null)
            {
                return emp;
            }
            return null;
        }

        // ============================================================= GET BILL TO BILL DETAIL ============================================================= //
        public bool updateBillTotalPrice(string bid, int price )
        {
            QLBMTEntities ql = new QLBMTEntities();
            var bill = (from b in ql.BILLs
                        where b.bID == bid
                        select b).SingleOrDefault();
            if (bill != null)
            {
                bill.bTotalPrice = bill.bTotalPrice + price;
                ql.SaveChanges();
            }
            return true;
        }

        public bool updateDecreaseBillTotalPrice(string bid, int price)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var bill = (from b in ql.BILLs
                        where b.bID == bid
                        select b).SingleOrDefault();
            if (bill != null)
            {
                bill.bTotalPrice = bill.bTotalPrice - price;
                ql.SaveChanges();
            }
            return true;
        }

        // GET sum of btotalprice
        public int SumofbTotalPrice()
        {
            QLBMTEntities ql = new QLBMTEntities();
            var bills = from b in ql.BILLs
                        where b.bBUY_Date != null && b.bBUY_Date.Value.Month == DateTime.Now.Month
                        select b;
            List<BILL> list = new List<BILL>();
            list = bills.ToList();
            int sum = 0;
            foreach (var bill in list)
            {
                sum += (int)bill.bTotalPrice;
            }
            return sum;
        }

    }
}
