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
using Microsoft.Extensions.Logging;

namespace DataBaseUI.Models
{
    public class PgSQLCostStoryRepository : ICostStoryRepository
    {
        SpsrLtDbContext db;
        IEnumerable<CostStory> stories;

        ILogger logger;

        public PgSQLCostStoryRepository(ILogger logger = null)
        {
            db = new SpsrLtDbContext();
            stories = new ObservableCollection<CostStory>();
            db.CostStories.Load();

            foreach (var cs in db.CostStories)
                ((ObservableCollection<CostStory>)stories).Add(new CostStory(cs.Id, cs.Year, cs.Month, cs.Cost, cs.Availabilityid));

            this.logger = logger;
        }

        public PgSQLCostStoryRepository(SpsrLtDbContext spsr, ILogger logger = null)
        {
            db = spsr;
            stories = new ObservableCollection<CostStory>();
            db.CostStories.Load();

            foreach (var cs in db.CostStories)
                ((ObservableCollection<CostStory>)stories).Add(new CostStory(cs.Id, cs.Year, cs.Month, cs.Cost, cs.Availabilityid));

            this.logger = logger;
        }

        public void Create(CostStory item)
        {
            try
            {
                db.CostStories.Add(new EFCostStory() { Id = db.CostStories.Count() != 0 ? db.CostStories.Max(x => x.Id) + 1 : 0, Year = item.Year, Month = item.Month, Availabilityid = item.AvailabilityId, Cost = item.Cost });
                db.SaveChanges();
                item.Id = db.CostStories.Max(x => x.Id);
                ((ObservableCollection<CostStory>)stories).Add(item);

                logger?.LogInformation(string.Format("Cost story with id = {0} was added.\n", item.Id));
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                logger?.LogError(e.Message);
            }
        }

        public void Delete(CostStory item)
        {
            try
            {
                db.CostStories.Remove(db.CostStories.Find(item.Id));
                db.SaveChangesAsync();
                ((ObservableCollection<CostStory>)stories).Remove(item);
                logger?.LogInformation(string.Format("Cost story with id = {0} was deleted.\n", item.Id));
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                logger?.LogError(e.Message);
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
                logger?.LogError(e.Message);
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
                EFCostStory c = db.CostStories.Find(item.Id);

                c.Year = item.Year;
                c.Month = item.Month;
                c.Cost = item.Cost;
                c.Availabilityid = item.AvailabilityId;

                db.CostStories.Update(c);
                db.SaveChanges();

                for (int i = 0; i < stories.Count(); i++)
                    if (((ObservableCollection<CostStory>)stories)[i].Id == item.Id)
                    {
                        ((ObservableCollection<CostStory>)stories)[i].Year = item.Year;
                        ((ObservableCollection<CostStory>)stories)[i].Month = item.Month;
                        ((ObservableCollection<CostStory>)stories)[i].Cost = item.Cost;
                        ((ObservableCollection<CostStory>)stories)[i].AvailabilityId = item.AvailabilityId;
                        break;
                    }

                logger?.LogInformation(string.Format("Cost story with id = {0} was updated.\n", item.Id));
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                logger?.LogError(e.Message);
            }
        }
    }
}
