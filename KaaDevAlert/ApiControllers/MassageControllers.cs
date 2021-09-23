using KaaDevAlert.Models;
using KaaDevAlert.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.ApiControllers
{
    [Route("api/Massage")]
    [ApiController]
    public class MassageControllers : ControllerBase
    {
        private readonly IMassageService _massageService;
        public MassageControllers(IMassageService massageService)
        {

            _massageService = massageService; 
        }
        [HttpGet]
        [Route("getall")]

        public IEnumerable<Massage> GetAll()
        {
            var massage = _massageService.GetAllWithDetail();
            return massage;
        }
        [HttpGet]
        [Route("getbyid/{id?}")]

        public Massage GetById(int id)
        {
            var item = _massageService.GetById(id);//การส่งข้อมูลผ่านคิวรี่เพื่อที่จะได้เรียกค่ากลับคืนมา 
            return item;
        }
        [HttpPost]
        public IActionResult Add(Massage item)
        {
            _massageService.Add(item);
            return Ok();
        }

        [HttpPut]
        public IActionResult Update(Massage item)
        {
            _massageService.Update(item);
            return Ok();
        }
        [HttpDelete]

        public IActionResult Delete(int id)
        {
            _massageService.Delete(id);
            return Ok();
        }
    }
}