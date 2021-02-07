using BiKafaProject.API.Controllers;
using BiKafaProject.Core.Interfaces;
using BiKafaProject.Core.Models;
using BiKafaProject.Core.Models.DbModels;
using BiKafaProject.Service.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BiKafaProject.Test
{


    public class UnitTest1
    {
        UserOperationsRepository _repo;
        UserOperationsController _controller;
        IOptions<Settings> settings = Options.Create<Settings>(new Settings());
        ConfigurationRoot builder = (ConfigurationRoot)new ConfigurationBuilder().AddJsonFile("appSettings.json", true, true).AddEnvironmentVariables().Build();

        public IConfigurationRoot Configuration { get; }

        public UnitTest1()
        {
            settings.Value.ConnectionString = "mongodb://localhost:27017";
            settings.Value.Database = "BIKAFA";
            settings.Value.IConfigurationRoot = builder;
            _repo = new UserOperationsRepository(settings);
            _controller = new UserOperationsController(_repo);
        }


        [Fact]
        public async Task SaveDataTests()
        {
            UserModel userModelTest = new UserModel()
            {
                UserName = "ozgur41",
                UserSurname = "oguz6",
                UserCompany = "vzx6"
            };

            var query = _controller.SaveData(userModelTest);
            query.Wait();

            Assert.IsType<CreatedResult>(query.Result);
        }


        [Fact]
        public async Task GetOneTests()
        {
            var id = "601ea0e9533419c4370a68ba";

            var query = _controller.GetData(id);
            query.Wait();

            Assert.IsType<OkObjectResult>(query.Result);
        }

        [Fact]
        public async Task GetAllTests()
        {

            var query = _controller.GetData(null);
            query.Wait();

            Assert.IsType<OkObjectResult>(query.Result);
        }

        [Fact]
        public async Task UpdateTests()
        {

            UserModel userModelTest = new UserModel()
            {
                Id = "601fd15306163fe1dfd4163d",
                UserName = "test",
                UserSurname = "test",
                UserCompany = "test"
            };

            var query = _controller.UpdateData(userModelTest);
            query.Wait();

            Assert.IsType<AcceptedResult>(query.Result);
        }


        public async Task RemoveOneTests()
        {

            var Id = "601fd15306163fe1dfd4163d";
            var query = _controller.DeleteData(Id);
            query.Wait();

            Assert.IsType<OkObjectResult>(query.Result);
        }


        public async Task DeleteAllests()
        {
            var query = _controller.UpdateData(null);
            query.Wait();

            Assert.IsType<OkObjectResult>(query.Result);
        }

    }
}
