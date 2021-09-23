using KaaDevAlert.Models;
using KaaDevAlert.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace KaaDevAlert.ApiControllers
{
    [Route("api/Configuration")]
    [ApiController]

    public class ConfigurationControllers : ControllerBase
    {
        private readonly IConfigurationService _configurationService;
        public ConfigurationControllers(IConfigurationService configurationService)
        {

            _configurationService = configurationService;
        }
        [HttpGet]
        [Route("getall")]

        public IEnumerable<Configuration> GetAll()
        {
            var masge = _configurationService.GetAll();
            return masge;
        }
        [HttpGet]
        [Route("getbyid/{id?}")]

        public Configuration GetById(int id)
        {
            var itemid = _configurationService.GetById(id);
            return itemid;
        }

        [HttpPost]
        public IActionResult Add(Configuration item)
        {
            _configurationService.Add(item);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Configuration item)
        {
            _configurationService.Update(item);
            return Ok();
        }

        [HttpDelete]

        public IActionResult Delete(int id)
        {
            _configurationService.Delete(id);
            return Ok();
        }
    }
}