﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.Controllers
{
    public class EmployeePositionController : Controller
    {
        // GET: EmployeePosition
        public ActionResult Index()
        {
            return View();
        }

        // GET: EmployeePosition/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EmployeePosition/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeePosition/Create
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

        // GET: EmployeePosition/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EmployeePosition/Edit/5
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

        // GET: EmployeePosition/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EmployeePosition/Delete/5
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