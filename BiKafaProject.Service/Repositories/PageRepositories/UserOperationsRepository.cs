using BiKafaProject.Core.Filters;
using BiKafaProject.Core.Interfaces;
using BiKafaProject.Core.Models;
using BiKafaProject.Core.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BiKafaProject.Service.Repositories
{
    public class UserOperationsRepository : IUserOperationsRepository
    {
        private static ObjectContext _ctx = null;

        //public UserOperationsRepository()
        //{

        //}

        //private static UserOperationsRepository userOperationsRepository = null;

        //public static UserOperationsRepository customerReportRepository(IOptions<Settings> settings)
        //{
        //    if (userOperationsRepository == null)
        //    {
        //        userOperationsRepository = new UserOperationsRepository();
        //        _ctx = new ObjectContext(settings);
        //    }
        //    return userOperationsRepository;
        //}


        public UserOperationsRepository(IOptions<Settings> settings)
        {
            _ctx = new ObjectContext(settings);
        }

        public async Task<IEnumerable<UserModel>> GetAsync(string id)
        {
            if (id == null)
                return await _ctx.UserModel.Find(x => true).ToListAsync();

            var UserModel = Builders<UserModel>.Filter.Eq("Id", id);
            return await _ctx.UserModel.Find(UserModel).ToListAsync();
        }

        public async Task<DeleteResult> DeleteAsync(string id)
        {
            if (id == null)
                return await _ctx.UserModel.DeleteManyAsync(new BsonDocument());

            var deleteInfo = Builders<UserModel>.Filter.Eq("Id", id);
            return await _ctx.UserModel.DeleteOneAsync(deleteInfo);
        }


        public async Task SaveAsync(UserModel entity)
        {
            await _ctx.UserModel.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(UserModel entity)
        {
            await _ctx.UserModel.ReplaceOneAsync(um => um.Id == entity.Id, entity);
        }


    }
}
