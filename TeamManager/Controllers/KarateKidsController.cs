using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamManager.Models;
using TeamManager.ViewModels;

namespace TeamManager.Controllers
{
    public class KarateKidsController : Controller
    {
        private readonly KarateKidDbContext _context;

        public KarateKidsController(KarateKidDbContext context)
        {
            _context = context;
            GroupsController GroupsController = new GroupsController(_context);
            DateModelsController dateModelsController = new DateModelsController(_context);

        }

        public async Task<IActionResult> Index(string id)
        {
            var Kids = from x in _context.KarateKidsAll
                       select x;
            if (!String.IsNullOrEmpty(id))
            {
                Kids = Kids.Where(x => x.name.Contains(id)||x.Group.Contains(id)||x.phone.ToString().Contains(id));
            }
            var Date = _context.DateBaseAll.Where(x => x.ActualDate.Month == DateTime.Today.Month & x.ItIsPayment==true);
            foreach (KarateKid child in _context.KarateKidsAll)
            {
                foreach (DateModel dateModel in Date)
                {
                    child.PaymentList.Add(dateModel);
                }
            }
            
            return View(await Kids.ToListAsync());
        }

        public async Task<IActionResult> Index2(int groupId)
        {
            var group = _context.Groups.Where(x => x.ID == groupId).FirstOrDefault();
            var karateKids = _context.KarateKidsAll.Where(x => x.Group == group.groupName);
            var Date = _context.DateBaseAll.Where(x => x.ActualDate == DateTime.Today);
            foreach (KarateKid child in karateKids)
            {   foreach (DateModel dateModel in Date)
                {
                    child.PresentList.Add(dateModel);
                }
            }
            return View(karateKids);
        }

        public async Task<IActionResult> AddPresent(int ID)
        {
            var karateKid = _context.KarateKidsAll.FirstOrDefault(x => x.ID == ID);

            var Today = new DateModel(karateKid.name, karateKid.Group, false);
            if (!_context.DateBaseAll.Where(x => x.ActualDate == DateTime.Today & x.ChildName == karateKid.name & x.ItIsPayment==false & x.GroupName==karateKid.Group).Select(x => x.ActualDate).Contains(Today.ActualDate))
            {
                _context.Update(Today);
            }
            await _context.SaveChangesAsync();

            var group = _context.Groups.Where(x => x.groupName == karateKid.Group).FirstOrDefault();
            return RedirectToAction("Index2", "KarateKids", new { groupId = group.ID });
        }

        public async Task<IActionResult> DeletePresent(int ID)
        {
            var karateKid = _context.KarateKidsAll.FirstOrDefault(x => x.ID == ID);
            var Today = new DateModel(karateKid.name, karateKid.Group, false);
            var RemovePresent = _context.DateBaseAll.Where(x => x.ActualDate == DateTime.Today & x.ChildName == karateKid.name & x.ItIsPayment == false & x.GroupName == karateKid.Group).FirstOrDefault();
            _context.DateBaseAll.Remove(RemovePresent);
            await _context.SaveChangesAsync();

            var group = _context.Groups.Where(x => x.groupName == karateKid.Group).FirstOrDefault();
            return RedirectToAction("Index2", "KarateKids", new { groupId = group.ID });
        }

        public async Task<IActionResult> AddPayment(int ID)
        {
            var karateKid = _context.KarateKidsAll.FirstOrDefault(x => x.ID == ID);

            var Today = new DateModel(karateKid.name,true);
            if (!_context.DateBaseAll.Where(x => x.ActualDate.Month == DateTime.Today.Month & x.ChildName == karateKid.name &x.ItIsPayment==true).Select(x => x.ActualDate).Contains(Today.ActualDate))
            {
                _context.Update(Today);
            }
            await _context.SaveChangesAsync();

            var group = _context.Groups.Where(x => x.groupName == karateKid.Group).FirstOrDefault();
            return RedirectToAction("Index", "KarateKids");
        }

        public async Task<IActionResult> DeletePayment(int ID)
        {
            var karateKid = _context.KarateKidsAll.FirstOrDefault(x => x.ID == ID);
            var Today = new DateModel(karateKid.name,true);
            var RemovePresent = _context.DateBaseAll.Where(x => x.ActualDate.Month == DateTime.Today.Month & x.ChildName == karateKid.name & x.ItIsPayment==true).FirstOrDefault();
            _context.DateBaseAll.Remove(RemovePresent);
            await _context.SaveChangesAsync();

            var group = _context.Groups.Where(x => x.groupName == karateKid.Group).FirstOrDefault();
            return RedirectToAction("Index", "KarateKids");
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karateKid = await _context.KarateKidsAll
                .FirstOrDefaultAsync(m => m.ID == id);
            if (karateKid == null)
            {
                return NotFound();
            }

            return View(karateKid);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,name,phone,Group")] KarateKid karateKid)
        {
            if (ModelState.IsValid)
            {
                _context.Add(karateKid);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(karateKid);
        }

        public async Task<IActionResult> Edit(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }
            
            var karateKid = await _context.KarateKidsAll.FindAsync(id);
            if (karateKid == null)
            {
                return NotFound();
            }

            return View(karateKid);
        }

        public async Task<IActionResult> PaymentHistory(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karateKid = await _context.KarateKidsAll.FindAsync(id);
            if (karateKid == null)
            {
                return NotFound();
            }

            var date = _context.DateBaseAll.Where(x => x.ItIsPayment == true & x.ChildName == karateKid.name);
            return View(date);
        }
        public async Task<IActionResult> PresentHistory(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karateKid = await _context.KarateKidsAll.FindAsync(id);
            if (karateKid == null)
            {
                return NotFound();
            }

            var date = _context.DateBaseAll.Where(x => x.ItIsPayment == false & x.ChildName == karateKid.name);
            return View(date);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,name,phone,Group")] KarateKid karateKid, Groups groups)
        {
            var lol = ViewBag.ViewGroups;
            if (id != karateKid.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(karateKid);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KarateKidExists(karateKid.ID))
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
            return View(karateKid);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var karateKid = await _context.KarateKidsAll
                .FirstOrDefaultAsync(m => m.ID == id);
            if (karateKid == null)
            {
                return NotFound();
            }

            return View(karateKid);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var karateKid = await _context.KarateKidsAll.FindAsync(id);
            _context.KarateKidsAll.Remove(karateKid);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KarateKidExists(int id)
        {
            return _context.KarateKidsAll.Any(e => e.ID == id);
        }
    }
}
