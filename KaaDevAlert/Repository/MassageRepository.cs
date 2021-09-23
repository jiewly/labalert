using Dapper;
using KaaDevAlert.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Repository
{
    public interface IMassageRepository
    {
        IEnumerable<Massage> GetAll();
        Massage GetById(int id);
        int Add(Massage model);
        int Update(Massage model);
        int Delete(Massage model);

    }

    public class MassageRepository : GenericRepository<Massage>, IMassageRepository
    {
        public MassageRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override int Add(Massage tModel)
        {
            var commandStr = string.Format(@"INSERT INTO[dbo].[Massage]
                                                           ([Lable])
                                                             VALUES
                                                           (@ParaLable )");

                 return _db.Execute(commandStr, MappingParameter(tModel));
        }

        public override string CreateSelectString()
        {
            var commandStr = "SELECT * FROM Massage";
            return commandStr;
        }

        public override int Delete(Massage tModel)
        {
            var commandStr = string.Format(@" DELETE FROM [dbo].[Massage]
                                             WHERE [id]= @ParaId");

            return _db.Execute(commandStr, new { ParaId = tModel.Id });
        }

        public override int Update(Massage tModel)
        {
            var commandStr = string.Format(@" UPDATE [dbo].[Massage]
                                      SET [Lable] =  @ParaLable
                                       WHERE [id]= @ParaId");
            return _db.Execute(commandStr, MappingParameter(tModel));
        }
        private object MappingParameter(Massage tModel)
        {
            return new
            {
                ParaId = tModel.Id,
                ParaLable = tModel.Lable
                
            };
        }
    }
}

