using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace Final_Project.BSLayer
{
    public class BLProduct
    {
        public DataTable getProduct()
        {
            QLBMTEntities ql = new QLBMTEntities();
            var product = from p in ql.Products select p;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("NAME");
            dt.Columns.Add("BRAND");
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("WARRANTY TIME");
            dt.Columns.Add("QUANTITY");
            dt.Columns.Add("PRICE");
            foreach (var p in product)
            {
                dt.Rows.Add(p.pID, p.pName, p.pBrand, p.pDescription, p.pWarranty_time, p.pQuantity, p.pPrice);
            }
            return dt;
        }

        public List<string> GetAllProductIDs()
        {
            using (QLBMTEntities ql = new QLBMTEntities())
            {
                return ql.Products.Select(e => e.pID).ToList();
            }
        }

        public int GetPriceOfProduct(string pid)
        {
            using (var context = new QLBMTEntities())
            {
                var product = context.Products.Find(pid);
                if (product != null)
                {
                    return (int)product.pPrice;
                }
                return 0;
            }
        }

        public Product GetProduct(string pid)
        {
            using (var context = new QLBMTEntities())
            {
                var product = context.Products.Find(pid);
                if (product != null)
                {
                    return product;
                }
                return null;
            }
        }

        public int GetQuantityOfProduct(string pid)
        {
            using (var context = new QLBMTEntities())
            {
                var product = context.Products.Find(pid);
                if (product != null)
                {
                    return (int)product.pQuantity;
                }
                return 0;
            }
        }

        // ============================================================= ADD PRODUCT ============================================================= //
        public bool addProduct(string name, string brand, string des, int warranty_time, int quantity, int price, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();

            Product p = new Product();
            p.pID = GenerateID();
            p.pName = name;
            p.pBrand = brand;
            p.pDescription = des;
            p.pWarranty_time = warranty_time;
            p.pQuantity = quantity;
            p.pPrice = price;
            ql.Products.Add(p);
            ql.SaveChanges();
            return true;
        }
        public string GenerateID() // auto create cID
        {
            int maxID = GetMaxID();
            int currentID = maxID + 1;
            string productID = "p" + currentID.ToString().PadLeft(5, '0');
            return productID;
        }
        public int GetMaxID()
        {
            using (var context = new QLBMTEntities())
            {
                var product = (from p in context.Products
                                orderby p.pID descending
                                select p).FirstOrDefault();


                if (product != null)
                {
                    return int.Parse(product.pID.Substring(1));
                }
                else
                {
                    return 0;
                }
            }
        }

        // ============================================================= DELETE PRODUCT ============================================================= //
        public bool deleteProduct(string id, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            Product product = new Product();
            product.pID = id;
            ql.Products.Attach(product);
            ql.Products.Remove(product);
            ql.SaveChanges();
            return true;
        }

        // ============================================================= UPDATE PRODUCT ============================================================= //
        public bool updateProduct(string id, string name, string brand, string des, int warranty_time, int quantity, int price, ref string err)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var product = (from p in ql.Products
                            where p.pID == id
                            select p).SingleOrDefault();
            if (product != null)
            {
                product.pID = id;
                product.pName = name.Trim();
                product.pBrand = brand.Trim();
                product.pDescription = des.Trim();
                product.pWarranty_time = warranty_time;
                product.pQuantity = quantity;
                product.pPrice = price;
                ql.SaveChanges();
            }
            return true;
        }


        // ============================================================= SEARCH PRODUCT ============================================================= //
        public List<Product> searchProducts(string key, int min, int max)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var product = from p in ql.Products
                          where (p.pID.Contains(key) || p.pName.Contains(key) || p.pBrand.Contains(key) || p.pDescription.Contains(key)) && (p.pPrice >= min && p.pPrice <= max) 
                          orderby p.pPrice
                          select p;
            return product.ToList();
        }
        public List<Product> searchProducts(int min, int max)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var product = from p in ql.Products
                          where (p.pPrice >= min && p.pPrice <= max)
                          orderby p.pPrice
                          select p;
            return product.ToList();
        }

        public bool updateQuantityProduct(string id, int Quantity)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var product = (from p in ql.Products
                           where p.pID == id
                           select p).SingleOrDefault();
            if (product != null)
            {
                product.pQuantity = product.pQuantity - Quantity;
                ql.SaveChanges();
            }
            return true;
        }

        public bool updateIncreaseQuantityProduct(string id, int Quantity)
        {
            QLBMTEntities ql = new QLBMTEntities();
            var product = (from p in ql.Products
                           where p.pID == id
                           select p).SingleOrDefault();
            if (product != null)
            {
                product.pQuantity = product.pQuantity + Quantity;
                ql.SaveChanges();
            }
            return true;
        }
    }
}
