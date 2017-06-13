using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DatabasesSecondLabotaroryAuth.Data;
using DAL.DAO.Interfaces;

namespace DatabasesSecondLabotaroryAuth.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly IMaterialRepository materialsRepository;
        private readonly IMeasureUnitRepository measureUnitRepository;

        public MaterialsController(IMaterialRepository materialsRepository, IMeasureUnitRepository measureUnitRepository)
        {
            this.materialsRepository = materialsRepository;
            this.measureUnitRepository = measureUnitRepository;
        }

        // GET: Materials
        public IActionResult Index()
        {
            return View(materialsRepository.GetAll());
        }

        // GET: Materials/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = materialsRepository.Get(id.Value);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // GET: Materials/Create
        public IActionResult Create()
        {
            ViewData["MeasureUnitId"] = new SelectList(measureUnitRepository.GetAll(), "Id", "Title");
            return View();
        }

        // POST: Materials/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title,Amount,MeasureUnitId")] Material material)
        {
            if (materialsRepository.Exists(material))
            {
                ModelState.AddModelError(string.Empty, "Item already exists");
            }
            if (ModelState.IsValid)
            {
                materialsRepository.Create(material);
                return RedirectToAction("Index");
            }
            ViewData["MeasureUnitId"] = new SelectList(measureUnitRepository.GetAll(), "Id", "Title", material.MeasureUnitId);
            return View(material);
        }

        // GET: Materials/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = materialsRepository.Get(id.Value);
            if (material == null)
            {
                return NotFound();
            }
            ViewData["MeasureUnitId"] = new SelectList(measureUnitRepository.GetAll(), "Id", "Title", material.MeasureUnitId);
            return View(material);
        }

        // POST: Materials/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Amount,MeasureUnitId")] Material material)
        {
            if (id != material.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    materialsRepository.Update(id, material);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaterialExists(material.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["MeasureUnitId"] = new SelectList(measureUnitRepository.GetAll(), "Id", "Id", material.MeasureUnitId);
            return View(material);
        }

        // GET: Materials/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var material = materialsRepository.Get(id.Value);
            if (material == null)
            {
                return NotFound();
            }

            return View(material);
        }

        // POST: Materials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var material = materialsRepository.Get(id);
            materialsRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private bool MaterialExists(int id)
        {
            return materialsRepository.Get(id) != null;
        }
    }

}
