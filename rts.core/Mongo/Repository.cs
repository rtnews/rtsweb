using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core;
using System.Linq.Expressions;

namespace rts.core
{
    public abstract class Repository<T0, T1> : Singleton<T0>
        where T1: Entity where T0 : new()
    {
        protected List<T1> QueryPage(Expression<Func<T1, bool>> filter, int skip, int take)
        {
            return collection.Find(filter).Skip(skip).Limit(take).ToList();
        }

        protected List<T1> QueryTop(int skip, int take)
        {
            return collection.Find(new BsonDocument()).Skip(skip).Limit(take).ToList();
        }

        protected T1 QueryOne()
        {
            return collection.Find(new BsonDocument()).FirstOrDefault();
        }

        protected bool InsertOne(T1 entity)
        {
            try
            {
                entity.Id = Guid.NewGuid();
                collection.InsertOne(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected bool UpdateOne(T1 entity)
        {
            if (entity.Id == null)
            {
                return this.InsertOne(entity);
            }

            var update = collection.ReplaceOne(
                x => x.Id == entity.Id, entity);

            return update.ModifiedCount == 1;
        }

        protected long DeleteOne(string nId)
        {
            return collection.DeleteOne(
                x => x.Id == Guid.Parse(nId)).DeletedCount;
        }

        protected long DeleteOne(Guid nId)
        {
            return collection.DeleteOne(
                x => x.Id == nId).DeletedCount;
        }

        protected long DeleteOne(T1 entity)
        {
            return collection.DeleteOne(
                x => x.Id == entity.Id).DeletedCount;
        }

        protected void GetCollection()
        {
            var repositoryName = GetRepositoryName();

            var dbContext = DbContext.Instance();
            collection = dbContext.GetCollection<T1>(repositoryName);
        }

        protected abstract string GetRepositoryName();

        private IMongoCollection<T1> collection;
    }
}
