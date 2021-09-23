using KaaDevAlert.Models;
using KaaDevAlert.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Controllers
{
    public class ConfigurationController : Controller
    {

        private readonly IConfigurationService _configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {

            _configurationService = configurationService;
        }

        public ActionResult Index()
        {
            var configurations = _configurationService.GetAll();
            return View(configurations);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Configuration item)
        {
            if (ModelState.IsValid)
            {
                _configurationService.Add(item);

                return RedirectToAction("Index");
            }

            return View(item);

        }


        public ActionResult Edit(int id)
        {
            Configuration item = _configurationService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Configuration item)
        {
            if (ModelState.IsValid)
            {
                _configurationService.Update(item);

                return RedirectToAction("Index");
            }

            return View(item);
        }
        public IActionResult Delete(int id)
        {
            _configurationService.Delete(id);
            return RedirectToAction("Index");

        }

    }
}
