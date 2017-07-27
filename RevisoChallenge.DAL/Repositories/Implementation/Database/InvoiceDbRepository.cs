using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Repositories.Implementation.Database
{
    class InvoiceDbRepository: IInvoiceDbRepository 
    {
        private RevisoChallengeEntities _context;

        public InvoiceDbRepository(DbContext context)
        {
            _context = (RevisoChallengeEntities)context;
        }

        DbContext IDbRepository.GetContext()
        {
            return _context;
        }

        public RevisoChallengeEntities GetContext()
        {
            return _context;
        }

        public void SetContext(DbContext context)
        {
            _context = (RevisoChallengeEntities)context;
        }

        public IList<Invoice> GetAll()
        {
            return GetContext().Invoices.ToList();
        }

        public bool Create(Invoice item)
        {
            try
            {
                GetContext().Invoices.Add(item);
                GetContext().SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Update(Invoice item)
        {
            try
            {
                var originalItem = GetContext().Invoices.FirstOrDefault(x => x.Id == item.Id);
                var entry = GetContext().Entry(originalItem);

                entry.CurrentValues.SetValues(item);
                entry.State = EntityState.Modified;
                GetContext().SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Delete(Invoice item)
        {
            try
            {
                GetContext().Invoices.Remove(item);
                GetContext().SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Invoice Get(int id)
        {
            return GetContext().Invoices.Find(id);
        }
    }
}
