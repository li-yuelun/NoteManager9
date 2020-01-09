using Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MongoDBHelper<T> where T:class,IEntity
    {
        private MongoClient client;

        private IMongoDatabase database;

        private IMongoCollection<T> collection;

        public MongoDBHelper(string server, string DatabaseName, string CollectionName)
        {
            client = new MongoClient(server);
            database = client.GetDatabase(DatabaseName);
            collection = database.GetCollection<T>(CollectionName);
        }

        //单个新增
        public async Task AddAsync(T t)
        {
            await collection.InsertOneAsync(t);
        }

        public void Add(T t)
        {
            collection.InsertOne(t);
        }

        //批量新增
        public async Task AddRangeAsync(List<T> list)
        {
            await collection.InsertManyAsync(list);
        }

        public void AddRange(List<T> list)
        {
            collection.InsertMany(list);
        }

        //根据条件动态删除
        public async Task DeleteAsync(Expression<Func<T, bool>> exp)
        {
            var filter = Builders<T>.Filter.Where(exp);
            await collection.DeleteManyAsync(filter);
        }

        public void Delete(Expression<Func<T, bool>> exp)
        {
            var filter = Builders<T>.Filter.Where(exp);
            collection.DeleteMany(filter);
        }

        //根据Id删除
        public async Task DeleteAsync(T t)
        {
            var filter = Builders<T>.Filter.Where(p => p.Id == t.Id);
            await collection.DeleteOneAsync(filter);
        }

        public void Delete(T t)
        {
            var filter = Builders<T>.Filter.Where(p => p.Id == t.Id);
            collection.DeleteOne(filter);
        }

        //根据Id修改
        public async Task UpdateAsync(T t)
        {
            //过滤要修改的
            var filter = Builders<T>.Filter.Where(p => p.Id == t.Id);
            var update = Builders<T>.Update.Set(p => p, t);
            await collection.UpdateOneAsync(filter, update);
        }

        public void Update(T t)
        {
            //过滤要修改的
            var filter = Builders<T>.Filter.Where(p => p.Id == t.Id);
            var update = Builders<T>.Update.Set(p => p, t);
            collection.UpdateOne(filter, update);
        }

        //根据Id查找单个
        public async Task<T> GetEntityAsync(ObjectId Id)
        {
            var filter = Builders<T>.Filter.Eq(p => p.Id, (ObjectId)Id);
            var entity = await collection.Find<T>(filter).FirstOrDefaultAsync();
            return entity;
        }

        public T GetEntity(ObjectId Id)
        {
            var filter = Builders<T>.Filter.Eq(p => p.Id, (ObjectId)Id);
            var entity = collection.Find<T>(filter).FirstOrDefault();
            return entity;
        }

        //根据动态条件查找单个
        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> exp)
        {
            var filter = Builders<T>.Filter.Where(exp); ;
            var entity = await collection.Find<T>(filter).SingleOrDefaultAsync();
            return entity;
        }

        public T GetEntity(Expression<Func<T, bool>> exp)
        {
            var filter = Builders<T>.Filter.Where(exp); ;
            var entity = collection.Find<T>(filter).SingleOrDefault();
            return entity;
        }

        //根据动态条件查找多个
        public async Task<List<T>> GetFilterAsync(Expression<Func<T, bool>> exp)
        {
            var filter = Builders<T>.Filter.Where(exp);
            var entitys = await collection.Find<T>(filter).ToListAsync();
            return entitys;
        }

        public List<T> GetFilter(Expression<Func<T, bool>> exp)
        {
            var filter = Builders<T>.Filter.Where(exp);
            var entitys = collection.Find<T>(filter).ToList();
            return entitys;
        }

        //查找所有
        public async Task<List<T>> GetFilterAsync()
        {
            var filter = Builders<T>.Filter.Empty;
            var entitys = await collection.FindAsync<T>(filter);
            return await entitys.ToListAsync();
        }

        public List<T> GetFilter()
        {
            var filter = Builders<T>.Filter.Empty;
            var entitys = collection.Find<T>(filter);
            return entitys.ToList();
        }
        //分页查询
        public async Task<List<T>> PagingAsync(int start, int pageCount, Expression<Func<T, bool>> exp)
        {
            var findOpt = new FindOptions<T, T>();
            findOpt.Limit = pageCount;
            findOpt.Skip = (start - 1) * pageCount;
            var filter = Builders<T>.Filter.Where(exp);
            var result = await collection.FindAsync(filter, findOpt);
            return await result.ToListAsync();
        }

        public List<T> Paging(int start, int pageCount, Expression<Func<T, bool>> exp)
        {
            var findOpt = new FindOptions<T, T>();
            findOpt.Limit = pageCount;
            findOpt.Skip = (start - 1) * pageCount;
            var filter = Builders<T>.Filter.Where(exp);
            var result = collection.Find(filter);
            return result.ToList();
        }
    }
}
