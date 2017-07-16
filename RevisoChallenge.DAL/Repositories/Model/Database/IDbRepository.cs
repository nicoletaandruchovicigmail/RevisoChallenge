using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisoChallenge.DAL.Repositories.Model.Database
{
    public interface IDbRepository
    {
        DbContext GetContext();
        void SetContext(DbContext context);
    }
}
