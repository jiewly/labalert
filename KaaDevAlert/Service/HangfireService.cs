using Hangfire;
using KaaDevAlert.Models;
using KaaDevAlert.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Service
{
    public interface IHangfireService
    {
        void Execute();
    }
    public class HangfireService : IHangfireService
    {
        private readonly IMassageService massageService;
        private readonly ILineNotiService lineNotiService;
        private readonly IConfigurationRepository configurationRepository;
        private readonly IMassageRepository massageRepository;
        public HangfireService(IMassageService massageService, ILineNotiService lineNotiService, IConfigurationRepository configurationRepository, IMassageRepository massageRepository)
        {

            this.massageService = massageService;
            this.lineNotiService = lineNotiService;
            this.massageRepository = massageRepository;
            this.configurationRepository = configurationRepository;
        }

        public void Execute()
        {
            var massage = massageService.GetAll();
            var random = new Random();
            var SelectedPost = massage.OrderBy(x => random.Next()).Take(1).FirstOrDefault().Lable;
            lineNotiService.SendLineNoti(SelectedPost);

        }

        public IEnumerable<Massage> SendMassages()
        {
            var massage = massageService.GetAll();
            var random = new Random();
            var SelectedPost = massage.OrderBy(x => random.Next()).Take(1).FirstOrDefault().Lable;
            lineNotiService.SendLineNoti(SelectedPost);
            return massage;
        }

    }
}
