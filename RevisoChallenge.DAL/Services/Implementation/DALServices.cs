﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Repositories.Implementation.Database;
using RevisoChallenge.DAL.Repositories.Implementation.Mock;
using RevisoChallenge.DAL.Repositories.Model.Database;

namespace RevisoChallenge.DAL.Services.Implementation
{
    public class DalServices
    {
        public IList<Project> GetProjects()
        {
            var context = new RevisoChallengeEntities();
            IProjectDbRepository projectRepository = new ProjectDbRepository(context);

            return projectRepository.GetAll();
        }

        public Project GetProject(int id)
        {
            var context = new RevisoChallengeEntities();
            IProjectDbRepository projectRepository = new ProjectDbRepository(context);

            return projectRepository.Get(id);
        }

        public void AddProject(Project project)
        {
            var context = new RevisoChallengeEntities();
            IProjectDbRepository projectRepository = new ProjectDbRepository(context);
            projectRepository.Create(project);
            context.SaveChanges();
        }

        void AddTask(Task task)
        {
            DbContext context = new RevisoChallengeEntities();
            ITaskDbRepository projectRepository = new TaskDbRepository(context);
            projectRepository.Create(task);
        }

        void AddClient(Client client)
        {
            DbContext context = new RevisoChallengeEntities();
            IClientDbRepository projectRepository = new ClientDbRepository(context);
            projectRepository.Create(client);
        }

        void AddInvoice(Invoice invoice)
        {
            DbContext context = new RevisoChallengeEntities();
            IInvoiceDbRepository projectRepository = new InvoiceDbRepository(context);
            projectRepository.Create(invoice);
        }

        Invoice CreateInvoice(Project project, Client client)
        {
            var invoice = new Invoice();

            return invoice;
        }


    }
}