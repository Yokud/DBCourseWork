using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBaseUI.DB;
using DataBaseUI.SysEntities;
using Microsoft.EntityFrameworkCore;

namespace DataBaseUI.Models
{
    internal class PgSQLProductsRepository : IProductsRepository, INotifyPropertyChanged
    {
        SpsrLtDbContext db;
        IEnumerable<Product> products = null!;

        public PgSQLProductsRepository()
        {
            db = new SpsrLtDbContext();
            products = new ObservableCollection<Product>();
            db.Products.Load();
            foreach (var prod in db.Products)
                ((ObservableCollection<Product>)products).Add(new Product(prod.Id, prod.Name, prod.Producttype));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Create(Product item)
        {
            try
            {
                db.Products.Add(new EFProduct() { Id = item.Id, Name = item.Name, Producttype = item.ProductType });
                db.SaveChanges();
                ((ObservableCollection<Product>)products).Add(item);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Delete(Product item)
        {
            try
            {
                db.Products.Remove(new EFProduct() { Id = item.Id, Name = item.Name, Producttype = item.ProductType });
                db.SaveChanges();
                ((ObservableCollection<Product>)products).Remove(item);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Product Get(int id)
        {
            try
            {
                var elem = db.Products.Find(id);

                if (elem != null)
                    return new Product(elem.Id, elem.Name, elem.Producttype);
                else
                    throw new Exception("Can\'t find product.\n");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Product item)
        {
            try
            {
                EFProduct product = new EFProduct() { Id = item.Id, Name = item.Name, Producttype = item.ProductType };
                db.Products.Update(product);
                db.SaveChanges();

                for (int i = 0; i < products.Count(); i++)
                    if (((ObservableCollection<Product>)products)[i].Id == item.Id)
                    {
                        ((ObservableCollection<Product>)products)[i] = item;
                        break;
                    }

            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public IEnumerable<Product> GetAllFromShop(Shop shop)
        {
            try
            {
                var prodIds = db.Availabilities.Where(x => x.Shopid == shop.Id).Select(x => x.Productid);
                var products = new ObservableCollection<Product>();

                foreach (var id in prodIds)
                {
                    var efprod = db.Products.Find(id);
                    products.Add(new Product(efprod.Id, efprod.Name, efprod.Producttype));
                }

                return products;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return null;
            }
        }
    }
}
