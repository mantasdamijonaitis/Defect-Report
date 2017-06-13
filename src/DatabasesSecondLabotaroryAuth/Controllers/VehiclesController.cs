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
using DatabasesSecondLabotaroryAuth.Models.ComplexCreateModels;

namespace DatabasesSecondLabotaroryAuth.Controllers
{
    public class VehiclesController : Controller
    {

        private readonly IVehicleRepository vehicleRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IVehicleTypeRepository vehicleTypeRepository;

        public VehiclesController(IVehicleRepository vehicleRepository,
            ICompanyRepository companyRepository, IVehicleTypeRepository vehicleTypeRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.companyRepository = companyRepository;
            this.vehicleTypeRepository = vehicleTypeRepository;
        }

        public IActionResult Index()
        {
            return View(vehicleRepository.GetAll());
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = vehicleRepository.Get(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(companyRepository.GetAll(), "Id", "Title");
            ViewData["VehicleTypeId"] = new SelectList(vehicleTypeRepository.GetAll(), "Id", "Title");
            return View(new VehicleCreateModel()
            {
                Vehicles = new List<Vehicle>()
                {
                    new Vehicle()
                },
                VehicleTypes = new List<VehicleType>()
                {
                    new VehicleType()
                }
            });
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(VehicleCreateModel model)
        {
            foreach (VehicleType vehicleType in model.VehicleTypes)
            {
                if (vehicleTypeRepository.Exists(vehicleType))
                {
                    ModelState.AddModelError(string.Empty, "VehicleType already exists");
                }
                if (ModelState.IsValid)
                {
                    vehicleType.Id = vehicleTypeRepository.Create(vehicleType);
                }
            }
            foreach (Vehicle vehicle in model.Vehicles)
            {
                if (vehicleRepository.Exists(vehicle))
                {
                    ModelState.AddModelError(string.Empty, "Vehicle already exists");
                }
                if (ModelState.IsValid)
                {
                    VehicleType previouslyStoredType = vehicleTypeRepository.GetByTitle(vehicle.VehicleType.Title);
                    if (previouslyStoredType != null)
                    {
                        vehicle.VehicleTypeId = previouslyStoredType.Id;
                        vehicleRepository.Create(vehicle);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            ViewData["CompanyId"] = new SelectList(companyRepository.GetAll(), "Id", "Title");
            ViewData["VehicleTypeId"] = new SelectList(companyRepository.GetAll(), "Id", "Title");
            return View(model);
        }

        // GET: Vehicles/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = vehicleRepository.Get(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(companyRepository.GetAll(), "Id", "Title", vehicle.CompanyId);
            ViewData["VehicleTypeId"] = new SelectList(vehicleTypeRepository.GetAll(), "Id", "Title", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,LicensePlates,CompanyId,VehicleTypeId")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    vehicleRepository.Update(id, vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            ViewData["CompanyId"] = new SelectList(companyRepository.GetAll(), "Id", "Title", vehicle.CompanyId);
            ViewData["VehicleTypeId"] = new SelectList(vehicleTypeRepository.GetAll(), "Id", "Title", vehicle.VehicleTypeId);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = vehicleRepository.Get(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vehicle = vehicleRepository.Get(id);
            vehicleRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private bool VehicleExists(int id)
        {
            return vehicleRepository.Get(id) != null;
        }
    }
}
