﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBaseUI.DB;
using DataBaseUI.SysEntities;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace DataBaseUI.Models
{
    public class PgSQLShopsRepository : IShopsRepository
    {
        SpsrLtDbContext db;
        IEnumerable<Shop> shops = null!;

        public PgSQLShopsRepository()
        {
            db = new SpsrLtDbContext();
            shops = new ObservableCollection<Shop>();
            db.Shops.Load();
            foreach (var efshop in db.Shops)
                ((ObservableCollection<Shop>)shops).Add(new Shop(efshop.Id, efshop.Name, efshop.Description));
        }

        public PgSQLShopsRepository(SpsrLtDbContext spsr)
        {
            db = spsr;
            shops = new ObservableCollection<Shop>();
            db.Shops.Load();
            foreach (var efshop in db.Shops)
                ((ObservableCollection<Shop>)shops).Add(new Shop(efshop.Id, efshop.Name, efshop.Description));
        }

        public void Create(Shop item)
        {
            try
            {
                db.Shops.Add(new EFShop() { Id = db.Shops.Max(x => x.Id) + 1 , Name = item.Name, Description = item.Description});
                db.SaveChanges();
                item.Id = db.Shops.Max(x => x.Id);
                ((ObservableCollection<Shop>)shops).Add(item);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Delete(Shop item)
        {
            try
            {
                db.Shops.Remove(db.Shops.Find(item.Id));
                db.SaveChanges();
                ((ObservableCollection<Shop>)shops).Remove(item);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Dispose()
        {
            ((ObservableCollection<Shop>)shops).Clear();
            db.Dispose();
        }

        public Shop Get(int id)
        {
            try
            {
                var elem = db.Shops.Find(id);

                if (elem != null)
                    return new Shop(elem.Id, elem.Name, elem.Description);
                else
                    throw new Exception("Can\t find shop.\n");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<Shop> GetAll()
        {
            return shops;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Shop item)
        {
            try
            {
                EFShop shop = db.Shops.Find(item.Id);

                shop.Name = item.Name;
                shop.Description = item.Description;

                db.Shops.Update(shop);
                db.SaveChanges();

                for (int i = 0; i < shops.Count(); i++)
                    if (((ObservableCollection<Shop>)shops)[i].Id == item.Id)
                    {
                        ((ObservableCollection<Shop>)shops)[i].Name = item.Name;
                        ((ObservableCollection<Shop>)shops)[i].Description = item.Description;
                        break;
                    }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }
    }
}
