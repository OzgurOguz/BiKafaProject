using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace BiKafaProject.Core.Models.DbModels
{


    public class ObjectContext
    {

        //private ObjectContext()
        //{
        //}
        //private static ObjectContext objectContextSngltn = null;
        //public static ObjectContext ObjectContextSngltn
        //{
        //    get
        //    {
        //        if (objectContextSngltn == null)
        //        {
        //            objectContextSngltn = new ObjectContext();
        //        }
        //        return objectContextSngltn;
        //    }
        //}

        public IConfigurationRoot Configuration { get; }
        private IMongoDatabase _db = null;

        public ObjectContext(IOptions<Settings> settings)
        {
            Configuration = settings.Value.IConfigurationRoot;
            settings.Value.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
            settings.Value.Database = Configuration.GetSection("MongoConnection:Database").Value;
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
            {
                _db = client.GetDatabase(settings.Value.Database);
            }
        }

        public IMongoCollection<UserModel> UserModel
        {
            get
            {
                return _db.GetCollection<UserModel>("UserModel");
            }
        }

    }
}
