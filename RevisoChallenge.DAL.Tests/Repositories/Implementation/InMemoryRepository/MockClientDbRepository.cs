using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Tests.Repositories.Implementation.InMemoryRepository
{
    public class MockClientDbRepository : IClientDbRepository
    {
        readonly List<Client> _clientList = new List<Client>();

        public void Add(Client clientToAdd)
        {
            _clientList.Add(clientToAdd);
        }

        public Client Get(int id)
        {
            return _clientList.FirstOrDefault(d => d.Id == id);
        }

        public IList<Client> GetAll()
        {
            return _clientList;
        }

        public bool Create(Client clientToCreate)
        {
            try
            {
                _clientList.Add(clientToCreate);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Delete(Client clientToDelete)
        {
            try
            {
                _clientList.Remove(clientToDelete);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool Update(Client item)
        {
            try
            {
                var clientToUpdate = _clientList.FirstOrDefault(x => x.Id == item.Id);
                if (clientToUpdate != null)
                {
                    _clientList.Remove(clientToUpdate);
                    _clientList.Add(item);
                    return true;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public DbContext GetContext()
        {
            throw new NotImplementedException();
        }

        public void SetContext(DbContext context)
        {
            throw new NotImplementedException();
        }
    }
}