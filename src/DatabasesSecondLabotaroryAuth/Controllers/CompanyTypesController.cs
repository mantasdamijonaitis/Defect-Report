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
    public class CompanyTypesController : Controller
    {

        private readonly ICompanyTypeRepository companyTypesRepository;

        public CompanyTypesController(ICompanyTypeRepository companyTypeRepository)
        {
            this.companyTypesRepository = companyTypeRepository;
        }

        // GET: CompanyTypes
        public IActionResult Index()
        {
            return View(companyTypesRepository.GetAll());
        }

        // GET: CompanyTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyType = companyTypesRepository.Get(id.Value);
            if (companyType == null)
            {
                return NotFound();
            }

            return View(companyType);
        }

        // GET: CompanyTypes/Create
        public IActionResult Create()
        {
            return View(new List<string>
            {
                string.Empty
            });
        }

        // POST: CompanyTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<string> companyTypesString)
        {
            List<CompanyType> companyTypes = companyTypesString.Select(x => new CompanyType { Title = x }).ToList();
            foreach(CompanyType companyType in companyTypes)
            {
                if (companyTypesRepository.Exists(companyType))
                {
                    ModelState.AddModelError(companyType.Title, "Item already exists");
                }
            }
            if (ModelState.IsValid)
            {
                foreach(CompanyType companyType in companyTypes)
                {
                    companyTypesRepository.Create(companyType);
                }
                return RedirectToAction("Index");
            }
            return View(companyTypesString);
        }

        // GET: CompanyTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyType = companyTypesRepository.Get(id.Value); ;
            if (companyType == null)
            {
                return NotFound();
            }
            return View(companyType);
        }

        // POST: CompanyTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title")] CompanyType companyType)
        {
            if (id != companyType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    companyTypesRepository.Update(id, companyType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyTypeExists(companyType.Id))
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
            return View(companyType);
        }

        // GET: CompanyTypes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyType = companyTypesRepository.Get(id.Value);
            if (companyType == null)
            {
                return NotFound();
            }

            return View(companyType);
        }

        // POST: CompanyTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var companyType = companyTypesRepository.Get(id);
            companyTypesRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private bool CompanyTypeExists(int id)
        {
            return companyTypesRepository.Get(id) != null;
        }
    }
}
