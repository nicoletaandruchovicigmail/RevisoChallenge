using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevisoChallenge.DAL.Repositories.Model
{
    public interface IRepository<T>
    {
        bool Create(T item);
        bool Delete(T item);
        bool Update(T item);
        T Get(int id);
        IList<T> GetAll();
    }
}
