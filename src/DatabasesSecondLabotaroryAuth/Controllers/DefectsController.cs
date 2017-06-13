using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL.Models;
using DatabasesSecondLabotaroryAuth.Data;
using DAL.DAO;
using DAL.DAO.Interfaces;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DatabasesSecondLabotaroryAuth.Controllers
{
    public class DefectsController : Controller
    {
        private readonly IDefectRepository defectsRepository;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IPhotoRepository photosRepository;

        public DefectsController(IDefectRepository defectsRepository, 
            IHostingEnvironment hostingEnvironment,
            IPhotoRepository photosRepository)
        {
            this.defectsRepository = defectsRepository;
            this.hostingEnvironment = hostingEnvironment;
            this.photosRepository = photosRepository;
        }

        // GET: Defects
        public IActionResult Index()
        {
            return View(defectsRepository.GetAll());
        }

        // GET: Defects/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defect = defectsRepository.Get(id.Value);
            if (defect == null)
            {
                return NotFound();
            }

            return View(defect);
        }

        // GET: Defects/Create
        public IActionResult Create()
        {
            return View(new Defect());
        }

        // POST: Defects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Defect defect)
        {
            if (defectsRepository.Exists(defect))
            {
                ModelState.AddModelError(string.Empty, "Defect already exists");
            }
            if (ModelState.IsValid)
            {
                foreach(Photo photo in defect.Photos)
                {
                    photo.Path = SaveFile(photo.PhotoFile);
                }
                defectsRepository.Create(defect);
                return RedirectToAction("Index");
            }
            return View(defect);
        }

        // GET: Defects/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defect = defectsRepository.Get(id.Value);
            if (defect == null)
            {
                return NotFound();
            }
            return View(defect);
        }

        // POST: Defects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Defect defect)
        {
            if (id != defect.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach(Photo photo in defect.Photos)
                    {
                        if (photo.Id == 0)
                        {
                            photo.Path = SaveFile(photo.PhotoFile);
                            photo.DefectId = id;
                            photo.NewlyInserted = true;
                            photo.Id = photosRepository.Create(photo);
                        }
                    }
                    defectsRepository.Update(id, defect);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DefectExists(defect.Id))
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
            return View(defect);
        }

        // GET: Defects/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var defect = defectsRepository.Get(id.Value);
            if (defect == null)
            {
                return NotFound();
            }

            return View(defect);
        }

        // POST: Defects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var defect = defectsRepository.Get(id);
            defectsRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private string SaveFile(IFormFile formFile)
        {
            var uploads = "wwwroot/uploads/";
            if (formFile != null && formFile.Length > 0)
            {
                var filePath = Path.Combine(uploads, formFile.FileName);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                    return filePath;
                }
            }
            return string.Empty;
        }

        private bool DefectExists(int id)
        {
            return defectsRepository.Get(id) != null;
        }
    }
}
