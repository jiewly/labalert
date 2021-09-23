using KaaDevAlert.Repository;
using KaaDevAlert.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Job
{
    public class Daily
    {
        private readonly IHangfireService hangfireService;
        private readonly IConfigurationRepository configurationRepository;

        public Daily(HangfireService hangfireService, IConfigurationRepository configurationRepository)
        {

            this.hangfireService = hangfireService;
            this.configurationRepository = configurationRepository;
        }

        public void Run()
        {
            hangfireService.Execute();
        }


        public Dictionary<string, string> GetTimeAlert()
        {
            var timeAlert = new Dictionary<string, string>();
            char[] delimiterChars = { '.' };
            var result = configurationRepository.GetKeyNumber("TimeAlert")?.Value;
                string[] words = result.Split(delimiterChars);
                timeAlert.Add("Hour", words[0]);
                timeAlert.Add("Minute", words[1]);

                return timeAlert;
            }
        }
    }

