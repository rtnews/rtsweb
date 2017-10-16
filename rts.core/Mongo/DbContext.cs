using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Configuration;

namespace rts.core
{
    public class DbContext : Singleton<DbContext>
    {
        public IMongoCollection<T> GetCollection<T>(string nName)
        {
            return mongoDatabase.GetCollection<T>(nName);
        }

        public void DropCollection(string nName)
        {
            mongoDatabase.DropCollection(nName);
        }

        public DbContext()
        {
            var dbString = ConfigurationManager.AppSettings.Get("DbString");
            var dbName = ConfigurationManager.AppSettings.Get("DbName");

            dbString = dbString.Replace("{DB_NAME}", dbName);

            mongoClient = new MongoClient(dbString);
            mongoDatabase = mongoClient.GetDatabase(dbName);
        }

        IMongoDatabase mongoDatabase;
        IMongoClient mongoClient;
    }
}
