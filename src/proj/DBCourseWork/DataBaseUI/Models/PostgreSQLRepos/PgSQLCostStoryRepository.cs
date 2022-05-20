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

        public PgSQLCostStoryRepository()
        {
            db = new SpsrLtDbContext();
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
            throw new NotImplementedException();
        }

        public IEnumerable<CostStory> GetAll()
        {
            return stories;
        }

        public IEnumerable<CostStory> GetFullCostStory(Shop shop, Product product)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(CostStory item)
        {
            throw new NotImplementedException();
        }
    }
}
