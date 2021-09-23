

using KaaDevAlert.Models;
using KaaDevAlert.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Service
{
    public interface IMassageService
    {
        IEnumerable<Massage> GetAllWithDetail();
        IEnumerable<Massage> GetAll();
        Massage GetById(int id);
        bool Add(Massage model);
        bool Update(Massage model);
        bool Delete(int id);

    }
    public class MassageService : IMassageService

    {
        private readonly IMassageRepository massageRepository;
        private readonly IConfigurationRepository configurationRepository;

        public MassageService(IMassageRepository massageRepository, IConfigurationRepository configurationRepository)
        {
            this.massageRepository = massageRepository;
            this.configurationRepository = configurationRepository;
          
        }

        public bool Add(Massage model)
        {
            return massageRepository.Add(model) > 0;
        }



        public bool Delete(int id)
        {
            var massage = new Massage { Id = id };
            return massageRepository.Delete(massage) > 0;
        }

        public IEnumerable<Massage> GetAll()
        {
            var list = massageRepository.GetAll();
            return list;
        }

        public IEnumerable<Massage> GetAllWithDetail()
        {
            var list = massageRepository.GetAll();
            var massageasg = PopulateLismassage(list);
            return massageasg;

        }
        private IEnumerable<Massage> PopulateLismassage(IEnumerable<Massage> massages)
        {
            var configurationtype = configurationRepository.GetAll();
            foreach (var item in massages)
            {
                PopulateMassage(item, configurationtype);
            }
            return massages;
        }

        private void PopulateMassage(Massage item, IEnumerable<Configuration> configurationtype)
        {
            item.Configurationtype = configurationtype.FirstOrDefault(model => model.Id == item.Type);
            return ;
        }

        public Massage GetById(int id)
        {
            var idlist = massageRepository.GetById(id);
            return idlist;
        }

        public bool Update(Massage model)
        {
            var massage = massageRepository.GetById(model.Id);
            massage.Lable = model.Lable;
           
            return massageRepository.Update(massage) > 0;
        }
    }
}
