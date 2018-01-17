﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CinemaPink.Data;
using CinemaPink.Models;

namespace CinemaPink.Controllers
{
    public class ProjectionsController : Controller
    {
        private readonly Cinema_context _context;

        public ProjectionsController(Cinema_context context)
        {
            _context = context;
        }

        // GET: Projections
        //public async Task<IActionResult> Index()
        //{
        //    var cinema_context = _context.Projections.Include(p => p.Film).Include(p => p.Room);
        //    return View(await cinema_context.ToListAsync());
        //}
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            var projections = from p in _context.Projections
                              .Include(p=>p.Film).Include(p=>p.Room)
                           select p;
            if (!String.IsNullOrEmpty(searchString))
            {
                projections = projections.Where(p => p.Film.Title.Contains(searchString));

            }
            switch (sortOrder)
            {
                case "name_desc":
                    projections = projections.OrderByDescending(p => p.Film.Title);
                    break;
                case "Date":
                    projections = projections.OrderBy(p => p.Date);
                    break;
                case "date_desc":
                    projections = projections.OrderByDescending(p => p.Date);
                    break;
                default:
                    projections = projections.OrderBy(p => p.Film.Title);
                    break;
            }
            return View(await projections.AsNoTracking().ToListAsync());
        }

        // GET: Projections/Create
        public IActionResult Create()
        {
            ViewData["FilmID"] = new SelectList(_context.Films, "ID", "ID");
            ViewData["RoomID"] = new SelectList(_context.Rooms, "ID", "ID");
            return View();
        }

        // POST: Projections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Date,RoomID,FilmID")] Projection projection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmID"] = new SelectList(_context.Films, "ID", "ID", projection.FilmID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "ID", "ID", projection.RoomID);
            return View(projection);
        }

        // GET: Projections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projection = await _context.Projections.SingleOrDefaultAsync(m => m.ID == id);
            if (projection == null)
            {
                return NotFound();
            }
            ViewData["FilmID"] = new SelectList(_context.Films, "ID", "ID", projection.FilmID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "ID", "ID", projection.RoomID);
            return View(projection);
        }

        // POST: Projections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Date,RoomID,FilmID")] Projection projection)
        {
            if (id != projection.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectionExists(projection.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FilmID"] = new SelectList(_context.Films, "ID", "ID", projection.FilmID);
            ViewData["RoomID"] = new SelectList(_context.Rooms, "ID", "ID", projection.RoomID);
            return View(projection);
        }

        // GET: Projections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projection = await _context.Projections
                .Include(p => p.Film)
                .Include(p => p.Room)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (projection == null)
            {
                return NotFound();
            }

            return View(projection);
        }

        // POST: Projections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projection = await _context.Projections.SingleOrDefaultAsync(m => m.ID == id);
            _context.Projections.Remove(projection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectionExists(int id)
        {
            return _context.Projections.Any(e => e.ID == id);
        }
    }
}