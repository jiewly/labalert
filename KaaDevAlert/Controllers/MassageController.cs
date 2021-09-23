using KaaDevAlert.Models;
using KaaDevAlert.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KaaDevAlert.Controllers
{
    public class MassageController : Controller
    {
        private readonly IMassageService _massageService;
        public MassageController(IMassageService massageService)
        {
            _massageService = massageService;
        }
        public IActionResult Index()
        {
            var masgs = _massageService.GetAll();
            return View(masgs);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Massage item)
        {
            if (ModelState.IsValid)
            {
                _massageService.Add(item);

                return RedirectToAction("Index");
            }
            return View(item);
        }

        public IActionResult Edit(int id)
        {
            Massage item = _massageService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Massage item)
        {
            if (ModelState.IsValid)
            {
                _massageService.Update(item);

                return RedirectToAction("Index");
            }

            return View(item);
        }
        public IActionResult Delete(int id)
        {
            _massageService.Delete(id);
            return RedirectToAction("Index");

        }
    }
}
