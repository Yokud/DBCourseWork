using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseUI.DB;
using DataBaseUI.SysEntities;
using Microsoft.EntityFrameworkCore;

namespace DataBaseUI.Models
{
    internal class PgSQLCostStoryRepository : ICostStoryRepository
    {
        SpsrLtDbContext db;
        IEnumerable<CostStory> stories;

        public PgSQLCostStoryRepository(SpsrLtDbContext spsr)
        {
            db = spsr;
            stories = new ObservableCollection<CostStory>();
            db.CostStories.Load();

            foreach (var cs in db.CostStories)
                ((ObservableCollection<CostStory>)stories).Add(new CostStory(cs.Id, cs.Year, cs.Month, cs.Cost, cs.Availabilityid));
        }

        public void Create(CostStory item)
        {
            try
            {
                db.CostStories.Add(new EFCostStory() { Id = item.Id, Year = item.Year, Month = item.Month, Availabilityid = item.AvailabilityId });
                db.SaveChanges();
                ((ObservableCollection<CostStory>)stories).Add(item);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Delete(CostStory item)
        {
            try
            {
                db.CostStories.Remove(new EFCostStory() { Id = item.Id, Year = item.Year, Month = item.Month, Availabilityid = item.AvailabilityId });
                db.SaveChanges();
                ((ObservableCollection<CostStory>)stories).Remove(item);
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

        public CostStory Get(int id)
        {
            try
            {
                var elem = db.CostStories.Find(id);

                if (elem != null)
                    return new CostStory(elem.Id, elem.Year, elem.Month, elem.Cost, elem.Availabilityid);
                else
                    throw new Exception("Can\'t find cost story elem.\n");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<CostStory> GetAll()
        {
            return stories;
        }

        public IEnumerable<CostStory> GetFullCostStory(Shop shop, Product product)
        {
            var avail = db.Availabilities.Where(x => x.Productid == product.Id && x.Shopid == shop.Id).First();
            return stories.Where(x => x.AvailabilityId == avail.Id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(CostStory item)
        {
            try
            {
                db.CostStories.Update(new EFCostStory() { Id = item.Id, Year = item.Year, Month = item.Month, Cost = item.Cost, Availabilityid = item.AvailabilityId });
                db.SaveChanges();

                for (int i = 0; i < stories.Count(); i++)
                    if (((ObservableCollection<CostStory>)stories)[i].Id == item.Id)
                    {
                        ((ObservableCollection<CostStory>)stories)[i] = item;
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
