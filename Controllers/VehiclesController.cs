using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Garage_3_MVCEF.Data;
using Garage_3_MVCEF.Models;

namespace Garage_3_MVCEF.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly Garage_3_MVCEFContext _context;

        public VehiclesController(Garage_3_MVCEFContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            var garage_3_MVCEFContext = _context.Vehicle.Include(v => v.VehicleType).Include(v => v.Member);
            return View(await garage_3_MVCEFContext.ToListAsync());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .Include(v => v.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            ViewBag.VehicleTypes = new SelectList(_context.Set<VehicleType>(), "Id", "VehicleTypeName");

            //ViewBag.VehicleTypes = new SelectList(_context.Set<VehicleType>(), "Id", "VehicleTypeName", Vehicle.VehicleTypeID);
                        //ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id");
            ViewData["MemberID"] = new SelectList(_context.Member, "Id", "Id");
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RegNumber,Brand,Model,Color,NumWheels,ArrivalTime,VehicleTypeID,MemberID")] Vehicle vehicle)
        {
            //if (ModelState.IsValid)
            //{
                var existingMember = await _context.Member.FirstOrDefaultAsync(m => m.PersonalNumber == vehicle.PersonalNumber);

                //if (existingMember != null)
                //{
                //    // Associate the vehicle with the member
                //    vehicle.Member = existingMember;

                    _context.Add(vehicle);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                //}
                //else
                //{
                //    ModelState.AddModelError("PersonalNumber", "Member with this PersonalNumber does not exist. Please add the member first.");
                //}
            //}

            ViewBag.VehicleTypes = new SelectList(_context.Set<VehicleType>(), "Id", "VehicleTypeName", vehicle.VehicleTypeID);

            return View(vehicle);
        }
        // GET: Vehicles/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var vehicle = await _context.Vehicle.FindAsync(id);
        //    if (vehicle == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", vehicle.VehicleTypeID);
        //    ViewData["MemberID"] = new SelectList(_context.Member, "Id", "Id", vehicle.MemberID);
        //    return View(vehicle);
        //}

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RegNumber,Brand,Model,Color,NumWheels,ArrivalTime,MemberID,VehicleTypeID")] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["VehicleTypeID"] = new SelectList(_context.Set<VehicleType>(), "Id", "Id", vehicle.VehicleTypeID);
            ViewData["MemberID"] = new SelectList(_context.Member, "Id", "Id", vehicle.Member.Id);
            return View(vehicle);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Include(v => v.VehicleType)
                .Include(v => v.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            if (vehicle != null)
            {
                _context.Vehicle.Remove(vehicle);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult CheckPersonalNumber(string personalNumber)
        {
            var existingMember = _context.Member.FirstOrDefault(m => m.PersonalNumber == personalNumber);

            if (existingMember != null)
            {
                // Return JSON indicating that the personal number exists and provide the member ID
                return Json(new { exists = true, memberId = existingMember.Id });
            }
            else
            {
                // Return JSON indicating that the personal number doesn't exist
                return Json(new { exists = false });
            }
        }
        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
