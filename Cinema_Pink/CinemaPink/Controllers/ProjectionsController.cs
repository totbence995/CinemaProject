using System;
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
        public async Task<IActionResult> Index(
          string sortOrder,
        string currentFilter,
        string searchString,
        int? page = 1)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "Date";
            ViewData["CurrentFilter"] = searchString;

            _context.Projections.Include(p => p.Room).ThenInclude(r => r.Seats);

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var projections = from p in _context.Projections
                              .Include(p => p.Film).Include(p => p.Room)
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
            int pageSize = 10;
            return View(await PaginatedList<Projection>.CreateAsync(projections.AsNoTracking(), page ?? 1, pageSize));
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
        [HttpPost]
        public async Task<IActionResult> Reserve(int id)
        {

            List<int> SeatIdList = new List<int>() { 158, 159, 160 };

            List<Seat> seats = null;
                
            foreach (var item in SeatIdList)
            {
                var seat = await _context.Seats.Where(s => s.ID == item).SingleOrDefaultAsync();
                seats.Add(seat);
            }

            var projection = await _context.Projections.Where(p => p.RoomID == seats[0].RoomID).SingleOrDefaultAsync();

            foreach (var seat in seats)
            {
                await _context.Reservations.AddAsync(new Reservation()
                {
                    ProjectionID = projection.ID,

                    SeatID = seat.ID,

                    Seat = seat/* _context.Seats.Where(s => s.ID == 3).FirstOrDefault()*/



                });
            }
            
            await _context.SaveChangesAsync();


            Notification notification = new Notification() { EmailAddress = "c@c.com", NumberOfSeats = 3, Title = "Star Wars VIII" };
            return Ok(notification);
        }

        //public async Task<IActionResult> Notification()
        //{
        //    await _context.SaveChangesAsync();
        //    Notification notification = new Notification() { EmailAddress = "c@c.com", NumberOfSeats = 3, Title = "Star Wars VIII" };
        //     return View(notification);
        //}
        public async Task<IActionResult> Room(int id)
        {
            var projection = await _context.Projections
                .Include(p => p.Room)
                .Include(x => x.Room.Seats)
                .Where(p => p.ID == id)
                            .FirstOrDefaultAsync();

            var dTOList = projection.Room.Seats.Select(y => new SeatDTO(y.ID, false)).ToList();
            foreach (var DTO in dTOList)
            {
                if (_context.Reservations.Any(x => x.SeatID == DTO.ID))
                {
                    DTO.Reserved = true;
                }
            }

            return View(dTOList);
        }

    }
}
