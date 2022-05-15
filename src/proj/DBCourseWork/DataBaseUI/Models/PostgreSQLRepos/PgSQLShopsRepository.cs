using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.Models
{
    internal class PgSQLShopsRepository : IShopsRepository, INotifyPropertyChanged
    {
        SpsrLtDbContext db;
        IEnumerable<Shop> shops = null!;

        public PgSQLShopsRepository()
        {
            db = new SpsrLtDbContext();
            db.Shops.Load();
            Shops = db.Shops.Local.ToBindingList();
        }

        public IEnumerable<Shop> Shops
        {
            get { return shops; }
            set 
            { 
                shops = value;
                OnPropertyChanged("Shops");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public void Create(Shop item)
        {
            try
            {
                item.Id = db.Shops.Count() + 1;
                db.Shops.Add(item);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Shop item)
        {
            try
            {
                db.Shops.Remove(item);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public Shop Get(int id)
        {
            try
            {
                var elem = db.Shops.Find(id);

                if (elem != null)
                    return elem;
                else
                    throw new Exception("Can\t find elem");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IEnumerable<Shop> GetAll()
        {
            return db.Shops;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Shop item)
        {
            try
            {
                db.Shops.Update(item);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
