
using KaaDevAlert.Models;
using KaaDevAlert.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Service
{
    public interface IConfigurationService
    {
        
        IEnumerable<Configuration> GetAll();
        Configuration GetById(int id);
        Configuration GetKeyNumber(string KeyNumber);
        bool Add(Configuration model);
        bool Update(Configuration model);
        bool Delete(int id);
    }
    public class ConfigurationService : IConfigurationService 
    {
        private readonly IConfigurationRepository configurationRepository;
        public ConfigurationService(IConfigurationRepository configurationRepository)
        {
            this.configurationRepository = configurationRepository;
        }

        public bool Add(Configuration model)
        {
            return configurationRepository.Add(model) > 0;
        }

        public bool Delete(int id)
        {
            var configuration = new Configuration { Id = id };
            return configurationRepository.Delete(configuration) > 0;
        }

        public IEnumerable<Configuration> GetAll()
        {
            var listtext = configurationRepository.GetAll();
            return listtext;
        }

        public IEnumerable<Configuration> GetAllWithDetail()
        {
            throw new NotImplementedException();
        }

        public Configuration GetById(int id)
        {
            var idlistcon = configurationRepository.GetById(id);
            return idlistcon;
        }

        public Configuration GetKeyNumber(string KeyNumber)
        {
            var Keylist = configurationRepository.GetKeyNumber(KeyNumber);
            return Keylist;
        }

        public bool Update(Configuration model)
        {
            var configuration = configurationRepository.GetById(model.Id);
            configuration.KeyNumber = model.KeyNumber;
            configuration.Value = model.Value;
            return configurationRepository.Update(configuration) > 0;
        }
    }

}
