using DataBaseUI.DB;
using DataBaseUI.SysEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseUI.Models
{
    internal class PgSQLAvailabilityRepository : IAvailabilityRepository
    {
        SpsrLtDbContext db;
        IEnumerable<Availability> availabilities;

        public PgSQLAvailabilityRepository()
        {
            db = new SpsrLtDbContext();
            availabilities = new ObservableCollection<Availability>();
            db.Availabilities.Load();
            foreach (var avail in db.Availabilities)
                ((ObservableCollection<Availability>)availabilities).Add(new Availability(avail.Id, avail.Shopid, avail.Productid));
        }

        public void Create(Availability item)
        {
            try
            {
                db.Availabilities.Add(new EFAvailability() { Id = item.Id, Shopid = item.ShopId, Productid = item.ProductId });
                db.SaveChanges();
                ((ObservableCollection<Availability>)availabilities).Add(item);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Delete(Availability item)
        {
            try
            {
                db.Availabilities.Remove(new EFAvailability() { Id = item.Id, Shopid = item.ShopId, Productid = item.ProductId });
                db.SaveChanges();
                ((ObservableCollection<Availability>)availabilities).Remove(item);
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

        public Availability Get(int id)
        {
            try
            {
                var elem = db.Availabilities.Find(id);

                if (elem != null)
                    return new Availability(elem.Id, elem.Shopid, elem.Productid);
                else
                    throw new Exception("Can\'t find availability.\n");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<Availability> GetAll()
        {
            return availabilities;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Availability item)
        {
            try
            {
                var avail = new EFAvailability() { Id = item.Id, Shopid = item.ShopId, Productid = item.ProductId };
                db.Availabilities.Update(avail);
                db.SaveChanges();

                for (int i = 0; i < availabilities.Count(); i++)
                    if (((ObservableCollection<Availability>)availabilities)[i].Id == item.Id)
                    {
                        ((ObservableCollection<Availability>)availabilities)[i] = item;
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
