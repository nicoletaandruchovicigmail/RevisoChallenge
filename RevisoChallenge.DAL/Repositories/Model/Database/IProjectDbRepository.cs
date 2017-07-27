using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using RevisoChallenge.DAL.Entities;

namespace RevisoChallenge.DAL.Repositories.Model.Database
{
    public interface IProjectDbRepository: IRepository<Project>, IDbRepository
    {
    }
}
