using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Final_Project.BSLayer
{
    public class BLCustomer
    {

        public DataTable getCustomer()
        {
            QLBMTEntities ql = new QLBMTEntities();
            var tps = from p in ql.Customers select p;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("Phone Number");
            dt.Columns.Add("EMAIL");
            dt.Columns.Add("ADDRESS");
            foreach (var p in tps)
            {
                dt.Rows.Add(p.cID, p.cName, p.cPhoneNum, p.cEmail, p.cAddress);
            }
            return dt;
        }

        public List<string> GetAllCustomerIDs()
        {
            using (QLBMTEntities ql = new QLBMTEntities())
            {
                return ql.Customers.Select(e => e.cID).ToList();
            }
        }

        // ============================================================= ADD CUSTOMER ============================================================= //
        public bool addCustomer(string name, string phonenum, string email, string address, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();

            Customer customer = new Customer();
            customer.cID = GenerateCustomerID();
            customer.cName = name;
            customer.cPhoneNum = phonenum;
            customer.cEmail = email;
            customer.cAddress = address;
            ql.Customers.Add(customer);
            ql.SaveChanges();
            return true;
        }
        public string GenerateCustomerID() // auto create cID
        {
            int maxID = GetMaxID();
            int currentID= maxID + 1;
            string customerID = "c" + currentID.ToString().PadLeft(5, '0');
            return customerID;
        }
        public int GetMaxID()
        {
            using (var context = new QLBMTEntities())
            {
                var customer = (from c in context.Customers
                                orderby c.cID descending
                                select c).FirstOrDefault();


                if (customer != null)
                {
                    return int.Parse(customer.cID.Substring(1));
                }
                else
                {
                    return 0;
                }
            }
        }

        // ============================================================= DELETE CUSTOMER ============================================================= //
        public bool deleteCustomer(string id, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            Customer customer = new Customer();
            customer.cID = id;
            ql.Customers.Attach(customer);
            ql.Customers.Remove(customer);
            ql.SaveChanges();
            return true;
        }

        // ============================================================= UPDATE CUSTOMER ============================================================= //
        public bool updateCustomer(string id, string name, string phonenum, string email, string address, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var customer = (from c in ql.Customers
                            where c.cID == id
                            select c).SingleOrDefault();
            if(customer != null)
            {
                customer.cID = id;
                customer.cEmail = email.Trim();
                customer.cAddress = address.Trim();
                customer.cPhoneNum = phonenum.Trim();
                customer.cName = name.Trim();
                ql.SaveChanges();
            }    
            return true;
        }


        // ============================================================= SEARCH CUSTOMER ============================================================= //
        public List<Customer> searchCustomers(string key)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var customers = from c in ql.Customers
                            where c.cID.Contains(key) || c.cName.Contains(key)
                            select c;
            return customers.ToList();
        }
    }
}
