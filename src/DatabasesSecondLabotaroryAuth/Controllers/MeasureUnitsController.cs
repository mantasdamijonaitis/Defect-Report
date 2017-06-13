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
    public class MeasureUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IMeasureUnitRepository measureUnitsRepository;

        public MeasureUnitsController(ApplicationDbContext context, IMeasureUnitRepository measureUnitsRepository)
        {
            _context = context;
            this.measureUnitsRepository = measureUnitsRepository;
        }

        // GET: MeasureUnits
        public IActionResult Index()
        {
            IEnumerable<MeasureUnit> measureUnits = measureUnitsRepository.GetAll();
            return View(measureUnitsRepository.GetAll());
        }

        // GET: MeasureUnits/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measureUnit = measureUnitsRepository.Get(id.Value);
            if (measureUnit == null)
            {
                return NotFound();
            }

            return View(measureUnit);
        }

        // GET: MeasureUnits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MeasureUnits/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Title")] MeasureUnit measureUnit)
        {
            if (measureUnitsRepository.Exists(measureUnit))
            {
                ModelState.AddModelError(string.Empty, "Item already exists");
            }
            if (ModelState.IsValid)
            {
                measureUnitsRepository.Create(measureUnit);
                return RedirectToAction("Index");
            }
            return View(measureUnit);
        }

        // GET: MeasureUnits/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measureUnit = measureUnitsRepository.Get(id.Value);
            if (measureUnit == null)
            {
                return NotFound();
            }
            return View(measureUnit);
        }

        // POST: MeasureUnits/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title")] MeasureUnit measureUnit)
        {
            if (id != measureUnit.Id)
            {
                return NotFound();
            }
           
            if (ModelState.IsValid)
            {
                try
                {
                    measureUnitsRepository.Update(id, measureUnit);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MeasureUnitExists(measureUnit.Id))
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
            return View(measureUnit);
        }

        // GET: MeasureUnits/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var measureUnit = measureUnitsRepository.Get(id.Value);
            if (measureUnit == null)
            {
                return NotFound();
            }

            return View(measureUnit);
        }

        // POST: MeasureUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var measureUnit = measureUnitsRepository.Get(id);
            measureUnitsRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private bool MeasureUnitExists(int id)
        {
            return measureUnitsRepository.Get(id) != null;
        }
    }
}
