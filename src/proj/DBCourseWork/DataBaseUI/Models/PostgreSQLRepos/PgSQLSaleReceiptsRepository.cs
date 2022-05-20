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
    internal class PgSQLSaleReceiptsRepository : ISaleReceiptsRepository
    {
        SpsrLtDbContext db;
        IEnumerable<SaleReceipt> saleReceipts;

        public PgSQLSaleReceiptsRepository()
        {
            db = new SpsrLtDbContext();
            saleReceipts = new ObservableCollection<SaleReceipt>();
            db.SaleReceipts.Load();
            foreach (var sr in db.SaleReceipts)
                ((ObservableCollection<SaleReceipt>)saleReceipts).Add(new SaleReceipt(sr.Id, sr.Fio, sr.Dateofpurchase, sr.Shopid));
        }

        public void Create(SaleReceipt item)
        {
            try
            {
                db.SaleReceipts.Add(new EFSaleReceipt() { Id = item.Id, Fio = item.Fio, Dateofpurchase = item.DateOfPurchase, Shopid = item.ShopId });
                db.SaveChanges();
                ((ObservableCollection<SaleReceipt>)saleReceipts).Add(item);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Delete(SaleReceipt item)
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

        public SaleReceipt Get(int id)
        {
            try
            {
                var elem = db.SaleReceipts.Find(id);

                if (elem != null)
                    return new SaleReceipt(elem.Id, elem.Fio, elem.Dateofpurchase, elem.Shopid);
                else
                    throw new Exception("Can\'t find sale receipt.\n");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<SaleReceipt> GetAll()
        {
            return saleReceipts;
        }

        public IEnumerable<SaleReceipt> GetAllFromShop(Shop shop)
        {
            return saleReceipts.Where(x => x.ShopId == shop.Id);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(SaleReceipt item)
        {
            try
            {
                EFSaleReceipt sr = new EFSaleReceipt() { Id = item.Id, Dateofpurchase = item.DateOfPurchase, Fio = item.Fio, Shopid = item.ShopId };
                db.SaleReceipts.Add(sr);
                db.SaveChanges();

                for (int i = 0; i < saleReceipts.Count(); i++)
                    if (((ObservableCollection<SaleReceipt>)saleReceipts)[i].Id == item.Id)
                    {
                        ((ObservableCollection<SaleReceipt>)saleReceipts)[i] = item;
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
