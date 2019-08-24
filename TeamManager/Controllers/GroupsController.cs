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
    public class GroupsController : Controller
    {
        private readonly KarateKidDbContext _context;

        public GroupsController(KarateKidDbContext context)
        {
            _context = context;

            foreach (Groups item in _context.Groups)
            {
                item.AddNewGroupToList(item.groupName);
            }
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Groups.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .FirstOrDefaultAsync(m => m.ID == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        public async Task<IActionResult> Members(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .FirstOrDefaultAsync(m => m.ID == id);
            if (groups == null)
            {
                return NotFound();
            }
            var kids = _context.KarateKidsAll.Where(x => x.Group == groups.groupName);

            return View(kids);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,groupName,TrainingDay,TrainingTime")] Groups groups)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groups);
                await _context.SaveChangesAsync();
                groups.AddNewGroupToList(groups.groupName);
                return RedirectToAction(nameof(Index));
            }
            return View(groups);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups.FindAsync(id);
            if (groups == null)
            {
                return NotFound();
            }
            return View(groups);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,groupName,TrainingDay,TrainingTime")] Groups groups)
        {

            if (id != groups.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Groups item = await _context.Groups.FindAsync(groups.ID);
                    item.groupName = groups.groupName;
                    item.TrainingDay = groups.TrainingDay;
                    item.TrainingTime = groups.TrainingTime;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupsExists(groups.ID))
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
            return View(groups);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var GroupToRemove = _context.Groups.Where(x => x.ID == id).FirstOrDefault();
            KarateKid.Remove(GroupToRemove.groupName);
            if (id == null)
            {
                return NotFound();
            }

            var groups = await _context.Groups
                .FirstOrDefaultAsync(m => m.ID == id);
            if (groups == null)
            {
                return NotFound();
            }

            return View(groups);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groups = await _context.Groups.FindAsync(id);
            _context.Groups.Remove(groups);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroupsExists(int id)
        {
            return _context.Groups.Any(e => e.ID == id);
        }
    }
}
