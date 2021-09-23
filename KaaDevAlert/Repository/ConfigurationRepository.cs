using Dapper;
using KaaDevAlert.Models;
using KaaDevAlert.Service;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Repository
{
    public interface IConfigurationRepository
    {
        IEnumerable<Configuration> GetAll();
        Configuration GetById(int id);
        Configuration GetKeyNumber(string KeyNumber);
        int Add(Configuration model);
        int Update(Configuration model);
        int Delete(Configuration model);
    }
    public class ConfigurationRepository : GenericRepository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override int Add(Configuration tModel)
        {
            var commandStr = string.Format(@"INSERT INTO [dbo].[Configuration]
                                            ([KeyNumber]
                                             ,[Value])
                                              VALUES
                                            (@ParaKeyNumber
                                             ,@ParaValue)");
            return _db.Execute(commandStr, MappingParameter(tModel));
        }

        public override string CreateSelectString()
        {
            var commandStr = "SElECT*FROM Configuration";
            return commandStr;
        }

        public override int Delete(Configuration tModel)
        {
            var commandStr = string.Format(@"DELETE FROM [dbo].[Configuration]

                                                  WHERE [id]= @ParaId");

            return _db.Execute(commandStr, new { ParaId = tModel.Id });
        }


        public override int Update(Configuration tModel)
        {
            var commandStr = string.Format(@"UPDATE [dbo].[Configuration]
                                           SET [KeyNumber] =@ParaKeyNumber
                                          ,[Value] = @ParaValue
                                           WHERE [id]= @ParaId");
            return _db.Execute(commandStr, MappingParameter(tModel));
        }
        private object MappingParameter(Configuration tModel)
        {
            return new
            {
                ParaId = tModel.Id,
                ParaKeyNumber = tModel.KeyNumber,
                ParaValue = tModel.Value,
              
            };
        }
        public Configuration GetKeyNumber(string KeyNumber)
        {
            var models = new List<Configuration>();
            using (var db = new SqlConnection(CONNECTION_STRING))
            {
                models = db.Query<Configuration>(CreateSelectString() + " WHERE KeyNumber =@KeyNumber", new
                {
                    KeyNumber
                }).ToList();
            }
                return models.FirstOrDefault();
        }
    }
}

