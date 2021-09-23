using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Repository
{
    public abstract class GenericRepository<TModel>
    {
        protected IDbConnection _db { get; set; }

        protected string CONNECTION_STRING;

        protected GenericRepository(IConfiguration configuration) 
        {
            CONNECTION_STRING = configuration.GetSection("ConnectionStrings:AppDB").Value;
            _db = new SqlConnection(CONNECTION_STRING);
        }

        public abstract string CreateSelectString();  
        public abstract int Add(TModel tModel);
        public abstract int Update(TModel tModel);
        public abstract int Delete(TModel tModel);

        public IEnumerable<TModel> GetAll()
        {
            var models = new List<TModel>();
            using (var db = new SqlConnection(CONNECTION_STRING))
            {
                models = db.Query<TModel>(CreateSelectString()).ToList();
            }
            return models;
        }

        public TModel GetById(int id)
        {
            var models = new List<TModel>();
            using (var db = new SqlConnection(CONNECTION_STRING))
            {
                models = db.Query<TModel>(CreateSelectString() + " WHERE Id = @Id", new
                {
                    id
                }).ToList();
            }
            return models.FirstOrDefault(); 
        }

    
    }
}
