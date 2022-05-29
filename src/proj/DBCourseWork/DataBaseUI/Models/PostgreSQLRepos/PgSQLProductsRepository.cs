using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBaseUI.DB;
using DataBaseUI.SysEntities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataBaseUI.Models
{
    public class PgSQLProductsRepository : IProductsRepository
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

        public PgSQLProductsRepository(SpsrLtDbContext spsr)
        {
            db = spsr;
            products = new ObservableCollection<Product>();
            db.Products.Load();
            foreach (var prod in db.Products)
                ((ObservableCollection<Product>)products).Add(new Product(prod.Id, prod.Name, prod.Producttype));
        }

        public void Create(Product item)
        {
            try
            {
                db.Products.Add(new EFProduct() { Id = db.Products.Max(x => x.Id) + 1, Name = item.Name, Producttype = item.ProductType });
                db.SaveChanges();
                item.Id = db.Products.Max(x => x.Id);
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
                db.Products.Remove(db.Products.Find(item.Id));
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
                EFProduct product = db.Products.Find(item.Id);

                product.Name = item.Name;
                product.Producttype = item.ProductType;

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
                var conn = (NpgsqlConnection?)db.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                if (conn.State == ConnectionState.Executing)
                    conn.Wait();
                string cmd = string.Format("select * from get_products_by_shopid({0})", shop.Id);
                NpgsqlCommand command = new NpgsqlCommand(cmd, conn);
                NpgsqlDataReader reader = command.ExecuteReader();
                ObservableCollection<Product> products = new ObservableCollection<Product>();

                while (reader.Read())
                    products.Add(new Product((int)reader.GetDouble(0), reader.GetString(1), reader.GetString(2), (int?)reader.GetDouble(3)));

                conn.Close();
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
