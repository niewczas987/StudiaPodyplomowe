using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.Controllers
{
    public class ItemTypeController : Controller
    {
        // GET: ItemType
        public ActionResult Index()
        {
            return View();
        }

        // GET: ItemType/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ItemType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ItemType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemType/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ItemType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ItemType/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ItemType/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}