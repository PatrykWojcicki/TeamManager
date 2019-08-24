using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TeamManager.Models;

namespace TeamManager.Controllers
{
    public class DateModelsController : Controller
    {
        private readonly KarateKidDbContext _context;

        public DateModelsController(KarateKidDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.DateBaseAll.ToListAsync());
        }
        public async Task<IActionResult> PresentGroupHistory()
        {
            var Date = _context.DateBaseAll.Where(x => x.ItIsPayment == false & x.GroupName=="Pszczółki").Select(x=>x);
            return View(Date);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dateModel = await _context.DateBaseAll
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dateModel == null)
            {
                return NotFound();
            }

            return View(dateModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ActualDate,ChildName,GroupName,TrainingDay")] DateModel dateModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dateModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dateModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dateModel = await _context.DateBaseAll.FindAsync(id);
            if (dateModel == null)
            {
                return NotFound();
            }
            return View(dateModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ActualDate,ChildName,GroupName,TrainingDay")] DateModel dateModel)
        {
            if (id != dateModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dateModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DateModelExists(dateModel.ID))
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
            return View(dateModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dateModel = await _context.DateBaseAll
                .FirstOrDefaultAsync(m => m.ID == id);
            if (dateModel == null)
            {
                return NotFound();
            }

            return View(dateModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dateModel = await _context.DateBaseAll.FindAsync(id);
            _context.DateBaseAll.Remove(dateModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DateModelExists(int id)
        {
            return _context.DateBaseAll.Any(e => e.ID == id);
        }
    }
}
