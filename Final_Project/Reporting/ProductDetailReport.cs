using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_Project.Reporting
{
    public class ProductDetailReport
    {
        public string bID { get; set; }
        public string cID { get; set; }
        public string eID { get; set; }
        public DateTime bBUY_Date { get; set; }
        public string cName { get; set; }
        public string eName { get; set; }
        public string pID { get; set; }
        public string pName { get; set; }
        public int bpQuantity_Product { get; set; }
        public int bpPrice { get; set; }
        public int SUM { get; set; }
        public int bTotalPrice { get; set; }
    }
}
