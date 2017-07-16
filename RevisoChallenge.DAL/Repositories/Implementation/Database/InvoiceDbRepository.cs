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

        public bool Create(Invoice item)
        {
            try
            {
                GetContext().Invoices.Add(item);
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
                GetContext().Invoices.Attach(item);
                var entry = GetContext().Entry(item);
                //entry.State = EntityState.Modified;
                entry.Property("Name").IsModified = true;
                //other properties

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
                GetContext().Invoices.Attach(item);
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

        public IList<Invoice> GetAll()
        {
            return GetContext().Invoices.ToList();
        }
    }
}
