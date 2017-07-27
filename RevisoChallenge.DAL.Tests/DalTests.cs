using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RevisoChallenge.Controllers;
using RevisoChallenge.DAL.Entities;
using RevisoChallenge.DAL.Services.Implementation;
using RevisoChallenge.DAL.Tests.Repositories.Implementation.InMemoryRepository;
using RevisoChallenge.Models;
using Task = System.Threading.Tasks.Task;

namespace RevisoChallenge.DAL.Tests
{
    [TestClass]
    public class DalTests
    {
        private Project CreateProjectNamed(int id, string name, string description, DateTime start, int clientId, decimal cost)
        {
            var newProject = new Project
            {
                Id = id,
                Name = name,
                Description = description,
                Start = start,
                ClientId = clientId,
                CostPerHour = cost
            };

            return newProject;
        }

        private Client CreateClientNamed(int id, string name, string email, string company)
        {
            var newClient = new Client
            {
                Id = id,
                Name = name,
                Email = email,
                Company = company
            };
            return newClient;
        }


        [TestMethod]
        public void TestGet_ProjectById()
        {
            // Arrange
            var project1 = CreateProjectNamed(1, "TestProject1", "Description of TestProject1", DateTime.Now, 1, 100);
            var project2 = CreateProjectNamed(2, "TestProject2", "Description of TestProject2", DateTime.Now, 1, 200);
            var client1 = CreateClientNamed(1, "TestClient1", "client1@email.com", "Company1");

            var mockProjectDbRepository = new MockProjectDbRepository();
            var mockClientDbRepository = new MockClientDbRepository();

            mockProjectDbRepository.Add(project1);
            mockProjectDbRepository.Add(project2);
            mockClientDbRepository.Add(client1);

            var dalServices = new DalServices(mockProjectDbRepository, mockClientDbRepository);
            var controller = new ProjectsController(dalServices);

            // Act
            var result = controller.Get(1);
            var requestContent = result.Content;
            var jsonContent = requestContent.ReadAsStringAsync().Result;
            var projectViewModel = JsonConvert.DeserializeObject<ProjectViewModel>(jsonContent);

            // Assert
            Assert.AreEqual(projectViewModel.Id, 1);
        }


        [TestMethod]
        public void TestGet_AllProjectsFromRepository()
        {
            var project1 = CreateProjectNamed(1, "TestProject1", "Description of TestProject1", DateTime.Now, 1, 100);
            var project2 = CreateProjectNamed(2, "TestProject2", "Description of TestProject2", DateTime.Now, 1, 200);
            var client1 = CreateClientNamed(1, "TestClient1", "client1@email.com", "Company1");

            var mockProjectDbRepository = new MockProjectDbRepository();
            var mockClientDbRepository = new MockClientDbRepository();

            mockProjectDbRepository.Add(project1);
            mockProjectDbRepository.Add(project2);
            mockClientDbRepository.Add(client1);

            var dalServices = new DalServices(mockProjectDbRepository, mockClientDbRepository);
            var controller = new ProjectsController(dalServices);

            var result = controller.Get();
            var requestContent = result.Content;
            var jsonContent = requestContent.ReadAsStringAsync().Result;
            var projectViewModels = JsonConvert.DeserializeObject<List<ProjectViewModel>>(jsonContent);

            // new projectViewModel to compare with
            var clientName = controller.GetClientName(project1.ClientId);
            decimal cost = (decimal)controller.GetProjectCost(project1.Id, project1.CostPerHour);
            var isProjectCompleted = controller.IsProjectCompleted(project1.Id);
            var testProject = new ProjectViewModel(project1, clientName, cost, isProjectCompleted);

            CollectionAssert.Contains(projectViewModels, testProject);

            clientName = controller.GetClientName(project2.ClientId);
            cost = (decimal)controller.GetProjectCost(project2.Id, project2.CostPerHour);
            isProjectCompleted = controller.IsProjectCompleted(project2.Id);
            var testProject2 = new ProjectViewModel(project2, clientName, cost, isProjectCompleted);

            CollectionAssert.Contains(projectViewModels, testProject2);
        }


        [TestMethod]
        public void TestUpdate_ClientById()
        {
            var client1 = CreateClientNamed(1, "TestClient1", "client1@email.com", "Company1");
            var mockClientDbRepository = new MockClientDbRepository();
            mockClientDbRepository.Add(client1);

            var mockProjectDbRepository = new MockProjectDbRepository();
            var dalServices = new DalServices(mockProjectDbRepository, mockClientDbRepository);
            var controller = new ClientsController(dalServices);

            var client2 = CreateClientNamed(1, "UpdatedTestClient1", "client1@email.com", "Company1");     
            controller.Edit(new ClientViewModel(client2));

            var updatedClient = mockClientDbRepository.Get(1);
            Assert.AreEqual(updatedClient.Name, client2.Name);
        }

        [TestMethod]
        [Ignore]
        public async Task ProjectCountTest()
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
        [Ignore]
        public void AddProjectTest()
        {
            var context = new RevisoChallengeEntities();
            var newProject = new Project
            {
                Name = "Test1",
                CostPerHour = 100,
                Start = DateTime.Now,
                ClientId = 1
            };
            context.Projects.Add(newProject);
            Assert.IsTrue(context.Entry(newProject).State == EntityState.Added);
        }

        [TestMethod]
        [Ignore]
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
            Assert.AreEqual(count + 1, count2);
        }
    }
}