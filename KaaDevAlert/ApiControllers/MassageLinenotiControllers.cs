
using KaaDevAlert.Models;
using KaaDevAlert.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.ApiControllers
{
    public class MassageLinenotiControllers : ControllerBase
    {
        private readonly IMassageService _massageService;
        private readonly ILineNotiService lineNotiService;
        public MassageLinenotiControllers(IMassageService massageService, ILineNotiService lineNotiService)
        {

            _massageService = massageService;
            this.lineNotiService = lineNotiService;
        }
        [HttpGet]
        [Route(" SendMassages")]

        public IEnumerable<Massage> SendMassages()
        {
            var massage = _massageService.GetAll();
            var random = new Random();
            var SelectedPost = massage.OrderBy(x => random.Next()).Take(1).FirstOrDefault().Lable;
           lineNotiService.SendLineNoti(SelectedPost);
            return massage;
        }
      
    }
}