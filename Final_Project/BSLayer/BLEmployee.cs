using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Final_Project.BSLayer
{
    public class BLEmployee
    {
        public DataTable getEmployee()
        {
            QLBMTEntities ql = new QLBMTEntities();
            var employees = from emp in ql.Employees select emp;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("DOB");
            dt.Columns.Add("PHONE NUMBER");
            dt.Columns.Add("ID CARD NUMBER");
            dt.Columns.Add("START DAY");
            dt.Columns.Add("BASE SALARY");
            dt.Columns.Add("KPI");
            dt.Columns.Add("GROSS SALAY");
            dt.Columns.Add("POSITION");
            dt.Columns.Add("PASSWORD");
            foreach (var emp in employees)
            {
                dt.Rows.Add(emp.eID, emp.eName, emp.eDOB, emp.ePhoneNum, emp.eIDCardNum, emp.eStartDay, emp.eBaseSalary, emp.eKPI, emp.eGrossSalary, emp.ePosition, emp.ePassword);
            }
            return dt;
        }

        

        // GET ALL ID
        public List<string> GetAllEmployeeIDs()
        {
            using (QLBMTEntities ql = new QLBMTEntities())
            {
                return ql.Employees.Select(e => e.eID).ToList();
            }
        }

        // ============================================================= ADD EMPLOYEE ============================================================= //
        public bool addEmployee(string name, DateTime dob, string phonenum, string idcard, int ebase, int kpi, int gross, string position, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            Employee emp = new Employee();
            emp.eID = this.GenerateID();
            emp.eName = name;
            emp.eDOB = dob;
            emp.ePhoneNum = phonenum;
            emp.eIDCardNum = idcard;
            emp.eBaseSalary = ebase;
            emp.eKPI = kpi;
            emp.eStartDay = DateTime.Now;
            emp.eGrossSalary = gross;
            emp.ePosition = position;
            emp.ePassword = this.GenerateRandomPassword();
            ql.Employees.Add(emp);
            ql.SaveChanges();
            return true;
        }
        public string GenerateID() // auto create ID
        {
            int maxID = GetMaxID();
            int currentID = maxID + 1;
            string productID = "e" + currentID.ToString().PadLeft(5, '0');
            return productID;
        }
        public int GetMaxID()
        {
            using (var context = new QLBMTEntities())
            {
                var emp = (from p in context.Employees
                               orderby p.eID descending
                               select p).FirstOrDefault();


                if (emp != null)
                {
                    return int.Parse(emp.eID.Substring(1));
                }
                else
                {
                    return 0;
                }
            }
        }

        // ============================================================= DELETE EMPLOYEE ============================================================= //
        public bool deleteEmployee(string id, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            Employee e = new Employee();
            e.eID = id;
            ql.Employees.Attach(e);
            ql.Employees.Remove(e);
            ql.SaveChanges();
            return true;
        }

        // ============================================================= UPDATE EMPLOYEE ============================================================= //
        public bool updateEmployee(string id, string name, DateTime dob, string phonenum, string idcard, int ebase, int kpi,int gross, string position, string pass, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var emp = (from e in ql.Employees
                            where e.eID == id
                            select e).SingleOrDefault();
            if (emp != null)
            {
                emp.eID = id;
                emp.eName = name.Trim();
                emp.eDOB = dob;
                emp.ePhoneNum = phonenum.Trim();
                emp.eIDCardNum = idcard.Trim();
                emp.eBaseSalary = ebase;
                emp.eKPI = kpi;
                emp.eGrossSalary = gross;
                emp.ePosition = position;
                emp.ePassword = pass.Trim();
                ql.SaveChanges();
            }
            return true;
        }


        // ============================================================= SEARCH EMPLOYEE ============================================================= //
        public List<Employee> searchEmployee(string key)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var emp = from e in ql.Employees
                            where e.eID.Contains(key) || e.eName.Contains(key)
                            select e;
            return emp.ToList();
        }
        public string GenerateRandomPassword()
        {
            const string chars = "0123456789";
            Random random = new Random();
            string password = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return password;
        }

        // ============================================================= UPDATE KPI EMPLOYEE = KPI + 10%TotalPrice ============================================================= //
        public bool UpdateAddKPI(string id, int TotalPrice)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var emp = (from e in ql.Employees
                       where e.eID == id
                       select e).SingleOrDefault();
            if (emp != null)
            {
                double a = 0.01 * (double)TotalPrice;
                emp.eKPI += (int)a;
                ql.SaveChanges();
            }
            return true;
        }

        // ============================================================= UPDATE KPI EMPLOYEE = KPI - 10%TotalPrice ============================================================= //
        public bool UpdateRemoveKPI(string id, int TotalPrice)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var emp = (from e in ql.Employees
                       where e.eID == id
                       select e).SingleOrDefault();
            if (emp != null)
            {
                double a = 0.01 * (double)TotalPrice;
                emp.eKPI -= (int)a;
                ql.SaveChanges();
            }
            return true;
        }

        //// ============================================================= UPDATE GROSS SALARY ============================================================= //
        public bool UpdateGrossSalary(string id, int bases, int kpi)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var emp = (from e in ql.Employees
                       where e.eID == id
                       select e).SingleOrDefault();
            if (emp != null)
            {
                emp.eGrossSalary = bases + kpi;
                ql.SaveChanges();
            }
            return true;
        }



        // ============================================================= RESET KPI SAU MỖI THÁNG ============================================================= //
        public void ResetKPI()
        {
            // LAY LIST NHAN VIEN
            QLBMTEntities ql = new QLBMTEntities();
            var employees = from emp in ql.Employees select emp;
            List<Employee> employeeList = new List<Employee>();
            foreach (var emp in employees)
            {
                employeeList.Add(new Employee
                {
                    eID = emp.eID,
                    eName = emp.eName,
                    eDOB = emp.eDOB,
                    ePhoneNum = emp.ePhoneNum,
                    eIDCardNum = emp.eIDCardNum,
                    eStartDay = emp.eStartDay,
                    eBaseSalary = emp.eBaseSalary,
                    eKPI = emp.eKPI,
                    eGrossSalary = emp.eGrossSalary,
                    ePosition = emp.ePosition,
                    ePassword = emp.ePassword
                });
            }
            foreach(var emp in employeeList)
            {
                int daywork = (DateTime.Now - emp.eStartDay.Value).Days;
                if (daywork % 28 == 0)
                {
                    emp.eKPI = 0;
                    emp.eGrossSalary = emp.eBaseSalary;
                }
            }
        }

        // ============================================================= LAY BASE SALARY ============================================================= //
        public int GetBase(string eid)
        {
            using (var context = new QLBMTEntities())
            {
                var emp = context.Employees.Find(eid);
                if (emp != null)
                {
                    return (int)emp.eBaseSalary;
                }
                return 0;
            }
        }

        // ============================================================= LAY BASE KPI ============================================================= //
        public int GetKPI(string eid)
        {
            using (var context = new QLBMTEntities())
            {
                var emp = context.Employees.Find(eid);
                if (emp != null)
                {
                    return (int)emp.eKPI;
                }
                return 0;
            }
        }

        public int SumofeGrosssalary()
        {
            QLBMTEntities ql = new QLBMTEntities();
            var emp = from e in ql.Employees
                      select e;
            List<Employee> employees = new List<Employee>();
            employees = emp.ToList();
            int sum = 0;
            foreach(var e in employees)
            {
                sum += (int)e.eGrossSalary;
            }
            return sum;
        }
    }
}
