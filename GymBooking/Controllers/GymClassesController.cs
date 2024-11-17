using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymBooking.Data;
using GymBooking.Models;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
//using GymBooking.Views;


namespace GymBooking.Controllers
{
    public class GymClassesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public GymClassesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: GymClasses
        public async Task<IActionResult> Index()
        {
            return View(await _context.GymClasses.ToListAsync());
        }

        // GET: GymClasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
           
            //var gymClassesName = _context.GymClasses.Select(e => e.Name);
            //var gymClassesDescription = _context.GymClasses.Select(e => e.Description);



            /*foreach(var gymClass in gymClassesName)
            {

            }*/

            //int a;

            /*var vehicles = context
            .Select(v => new OverviewViewModel
            {
                VehicleType = v.VehicleType,
                RegNr = v.RegNr,
                Arrival = v.Arrival
            });*/


            //var gymclasses = _context.GymClasses
            //.Select(g => g.Name);

            //IQueryable

            //int ay = 0;



            //var gymClass = _context.GymClasses.Select(m => m.Id == 2);
            //var gymClass2 = _context.GymClasses.Where(g => g.Name == "Aerobics" && g.Description == "Professional");





                var gymClass = await _context.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);

            if (gymClass == null)
            {
                return NotFound();
            }

            //2f8f5d57 - efc0 - 4f6c - 9049 - ec0e33713f18
            //d59bf970 - 5980 - 441e-981a - de0cce05519c

            //var userId = _userManager.GetUserId(this.User);

            //var usr = await _userManager.FindByNameAsync("");
            var usr1 = await _userManager.FindByIdAsync("d59bf970-5980-441e-981a-de0cce05519c");
            var usr2 = await _userManager.FindByIdAsync("2f8f5d57-efc0-4f6c-9049-ec0e33713f18");

            var ema1 = usr1.Email;
            var ema2 = usr2.Email;

            //var em = _userManager.GetUserEma

            var att = _context.ApplicationUserGymClass.Where(a => a.GymClassId == id).ToList();

            gymClass.AttendingMembers = new List<ApplicationUserGymClass>();


            foreach(var a in att)
            {
                var usr = await _userManager.FindByIdAsync(a.ApplicationUserId);
                a.ApplicationUserId = usr.Email;
                gymClass.AttendingMembers.Add(a);
            }

            //gymClass.AttendingMembers.Clear();

            int ab = 5;

            //gymClass.AttendingMembers.Add(attending.ElementAt(0));

            int b = 5;

            //var attending = await _context.ApplicationUserGymClass.FindAsync(id);

            //gymClass.AttendingMembers.Add(attending.app);





            //var a = attending


            /*foreach(var a in attending)
            {

            }*/


            /*var augc1 = new ApplicationUserGymClass();
            augc1.ApplicationUserId = "userid1";
            var augc2 = new ApplicationUserGymClass();
            augc2.ApplicationUserId = "userid2";

            gymClass.AttendingMembers.Add(augc1);
            gymClass.AttendingMembers.Add(augc2);*/





            //ApplicationUserGymCla


            //var augc = await _context.ApplicationUserGymClass
            //.FirstOrDefaultAsync(e => e.GymClassId == 5 && e.ApplicationUserId == "gfdf");



            //var gymClass3 = await _context.GymClasses.FindAsync(id);

            /*if (gymClass == null)
            {
                return NotFound();
            }*/

            //var test = new ViewModel();
            //test.Name = "Hej";



            return View(gymClass);
        }



        // GET: GymClasses/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: GymClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gymClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gymClass);
        }

        // GET: GymClasses/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass == null)
            {
                return NotFound();
            }
            return View(gymClass);
        }

        // POST: GymClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartTime,Duration,Description")] GymClass gymClass)
        {
            if (id != gymClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gymClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GymClassExists(gymClass.Id))
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
            return View(gymClass);
        }

        // GET: GymClasses/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gymClass = await _context.GymClasses
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gymClass == null)
            {
                return NotFound();
            }

            return View(gymClass);
        }

        // POST: GymClasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gymClass = await _context.GymClasses.FindAsync(id);
            if (gymClass != null)
            {
                _context.GymClasses.Remove(gymClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GymClassExists(int id)
        {
            return _context.GymClasses.Any(e => e.Id == id);
        }

        [Authorize]
        public async Task<IActionResult> BookingToggle(int? id)
        {
            if (id == null) return NotFound();

            var userId = _userManager.GetUserId(this.User);

            var attending = await _context.ApplicationUserGymClass.FindAsync(userId, id);

            if (attending == null)
            {
                var booking = new ApplicationUserGymClass
                {
                    ApplicationUserId = userId,
                    GymClassId = (int)id
                };
                _context.ApplicationUserGymClass.Add(booking);
            }
            else
            {
                _context.Remove(attending);
                //_context.ApplicationUserGymClass.Remove(attending);
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
