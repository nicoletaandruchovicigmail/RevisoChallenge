using System;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;

namespace RevisoChallenge.DAL.Tests
{
    [TestClass]
    public class DalTests
    {
        [TestMethod]
        public async System.Threading.Tasks.Task ProjectCountTest()
        {
            try
            {
                var context = new RevisoChallengeEntities();
                
                var projectCount = await context.Projects.SqlQuery("Select * From Project").CountAsync();

                context.SaveChanges();
                context.Projects.Add(new Project
                {
                    Name = "Test",
                    CostPerHour = 100,
                    End = DateTime.Now,
                    Start = DateTime.Now,
                    ClientId = 1

                });
                context.SaveChanges();
                var projectCount2 = await context.Projects.SqlQuery("Select * From Project").CountAsync();
                context.SaveChanges();
                Assert.AreEqual(projectCount + 1, projectCount2);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [TestMethod]
        public void AddProjectTest()
        {
            var context = new RevisoChallengeEntities();
            var newProject = new Project
            {
                Name="Test1",
                CostPerHour=100,
                Start=DateTime.Now,
                ClientId = 1
            };
            context.Projects.Add(newProject);
            Assert.IsTrue(context.Entry(newProject).State==EntityState.Added);
        }

        [TestMethod]
        public void AddProjectTest2()
        {
            var service = new DalServices();
            var newProject = new Project
            {
                Name = "Test1",
                CostPerHour = 100,
                Start = DateTime.Now,
                ClientId = 1
            };

            var count = service.GetProjects().Count;
            service.AddProject(newProject);
            var count2 = service.GetProjects().Count;
            Assert.AreEqual(count+1, count2);
        }

    }
}