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
    public class CompaniesController : Controller
    {

        private readonly ICompanyRepository companyRepository;
        private readonly ICompanyTypeRepository companyTypeRepository;

        public CompaniesController(ICompanyRepository companyRepository, ICompanyTypeRepository companyTypeRepository)
        {
            this.companyRepository = companyRepository;
            this.companyTypeRepository = companyTypeRepository;
        }

        // GET: Companies
        public IActionResult Index()
        {
            return View(companyRepository.GetAll());
        }

        // GET: Companies/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = companyRepository.Get(id.Value);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // GET: Companies/Create
        public IActionResult Create()
        {
            List<Company> companies = new List<Company>()
            {
                new Company()
                {
                    CompanyType = new CompanyType()
                }
            };
            ViewData["CompanyTypeId"] = new SelectList(companyTypeRepository.GetAll(), "Id", "Title");
            return View(companies);
        }

        // POST: Companies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(List<Company> companies)
        {
            foreach(Company company in companies)
            {
                if (companyRepository.Exists(company))
                {
                    ModelState.AddModelError(string.Empty, "Item already exists");
                }
                if (ModelState.IsValid)
                {
                    companyRepository.Create(company);
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            ViewData["CompanyTypeId"] = new SelectList(companyTypeRepository.GetAll(), "Id", "Title");
            return View(companies);
        }

        // GET: Companies/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = companyRepository.Get(id.Value);
            if (company == null)
            {
                return NotFound();
            }
            ViewData["CompanyTypeId"] = new SelectList(companyTypeRepository.GetAll(), "Id", "Title", company.CompanyTypeId);
            return View(company);
        }

        // POST: Companies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Code,CompanyTypeId")] Company company)
        {
            if (id != company.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    companyRepository.Update(id, company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyExists(company.Id))
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
            ViewData["CompanyTypeId"] = new SelectList(companyTypeRepository.GetAll(), "Id", "Title", company.CompanyTypeId);
            return View(company);
        }

        // GET: Companies/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var company = companyRepository.Get(id.Value);
            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        // POST: Companies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var company = companyRepository.Get(id);
            companyRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private bool CompanyExists(int id)
        {
            return companyRepository.Get(id) != null;
        }
    }
}
