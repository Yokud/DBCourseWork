using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseUI.DB;
using DataBaseUI.SysEntities;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataBaseUI.Models
{
    public class PgSQLSaleReceiptPositionsRepository : ISaleReceiptPositionsRepository
    {
        SpsrLtDbContext db;
        IEnumerable<SaleReceiptPosition> saleReceiptPositions;

        public PgSQLSaleReceiptPositionsRepository()
        {
            db = new SpsrLtDbContext();
            saleReceiptPositions = new ObservableCollection<SaleReceiptPosition>();
            db.SaleReceiptPositions.Load();
            foreach (var srp in db.SaleReceiptPositions)
                ((ObservableCollection<SaleReceiptPosition>)saleReceiptPositions).Add(new SaleReceiptPosition(srp.Id, srp.Availabilityid, srp.Salereceiptid));
        }

        public PgSQLSaleReceiptPositionsRepository(SpsrLtDbContext spsr)
        {
            db = spsr;
            saleReceiptPositions = new ObservableCollection<SaleReceiptPosition>();
            db.SaleReceiptPositions.Load();
            foreach (var srp in db.SaleReceiptPositions)
                ((ObservableCollection<SaleReceiptPosition>)saleReceiptPositions).Add(new SaleReceiptPosition(srp.Id, srp.Availabilityid, srp.Salereceiptid));
        }

        public void Create(SaleReceiptPosition item)
        {
            try
            {
                db.SaleReceiptPositions.Add(new EFSaleReceiptPosition() { Id = db.SaleReceiptPositions.Max(x => x.Id) + 1, Availabilityid = item.AvailabilityId, Salereceiptid = item.SaleReceiptId });
                db.SaveChanges();
                item.Id = db.SaleReceiptPositions.Max(x => x.Id);
                ((ObservableCollection<SaleReceiptPosition>)saleReceiptPositions).Add(item);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        public void Delete(SaleReceiptPosition item)
        {
            try
            {
                db.SaleReceiptPositions.Remove(db.SaleReceiptPositions.Find(item.Id));
                db.SaveChanges();
                ((ObservableCollection<SaleReceiptPosition>)saleReceiptPositions).Remove(item);
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

        public SaleReceiptPosition Get(int id)
        {
            try
            {
                var elem = db.SaleReceiptPositions.Find(id);

                if (elem != null)
                    return new SaleReceiptPosition(elem.Id, elem.Availabilityid, elem.Salereceiptid);
                else
                    throw new Exception("Can\'t find sale receipt position.\n");
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                return null;
            }
        }

        public IEnumerable<SaleReceiptPosition> GetAll()
        {
            return saleReceiptPositions;
        }

        public IEnumerable<Product> GetAllFromSaleReceipt(SaleReceipt saleReceipt)
        {
            try
            {
                var conn = (NpgsqlConnection?)db.Database.GetDbConnection();
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                if (conn.State == ConnectionState.Executing)
                    conn.Wait();
                string cmd = string.Format("select * from get_content_from_salereceipt({0})", saleReceipt.Id);
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

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(SaleReceiptPosition item)
        {
            try
            {
                EFSaleReceiptPosition srp = db.SaleReceiptPositions.Find(item.Id);

                srp.Salereceiptid = item.SaleReceiptId;
                srp.Availabilityid = item.AvailabilityId;

                db.SaleReceiptPositions.Update(srp);
                db.SaveChanges();

                for (int i = 0; i < saleReceiptPositions.Count(); i++)
                    if (((ObservableCollection<SaleReceiptPosition>)saleReceiptPositions)[i].Id == item.Id)
                    {
                        ((ObservableCollection<SaleReceiptPosition>)saleReceiptPositions)[i].AvailabilityId = item.AvailabilityId;
                        ((ObservableCollection<SaleReceiptPosition>)saleReceiptPositions)[i].SaleReceiptId = item.SaleReceiptId;
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
