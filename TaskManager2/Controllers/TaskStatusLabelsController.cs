using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager2.Data;
using TaskManager2.Models;

namespace TaskManager2.Controllers
{
    public class TaskStatusLabelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaskStatusLabelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaskStatusLabels
        public async Task<IActionResult> Index()
        {
              return _context.TaskStatusLabel != null ? 
                          View(await _context.TaskStatusLabel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.TaskStatusLabel'  is null.");
        }

        // GET: TaskStatusLabels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TaskStatusLabel == null)
            {
                return NotFound();
            }

            var taskStatusLabel = await _context.TaskStatusLabel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskStatusLabel == null)
            {
                return NotFound();
            }

            return View(taskStatusLabel);
        }

        // GET: TaskStatusLabels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaskStatusLabels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TaskStatusLabel taskStatusLabel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskStatusLabel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskStatusLabel);
        }

        // GET: TaskStatusLabels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TaskStatusLabel == null)
            {
                return NotFound();
            }

            var taskStatusLabel = await _context.TaskStatusLabel.FindAsync(id);
            if (taskStatusLabel == null)
            {
                return NotFound();
            }
            return View(taskStatusLabel);
        }

        // POST: TaskStatusLabels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TaskStatusLabel taskStatusLabel)
        {
            if (id != taskStatusLabel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskStatusLabel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskStatusLabelExists(taskStatusLabel.Id))
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
            return View(taskStatusLabel);
        }

        // GET: TaskStatusLabels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TaskStatusLabel == null)
            {
                return NotFound();
            }

            var taskStatusLabel = await _context.TaskStatusLabel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taskStatusLabel == null)
            {
                return NotFound();
            }

            return View(taskStatusLabel);
        }

        // POST: TaskStatusLabels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TaskStatusLabel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TaskStatusLabel'  is null.");
            }
            var taskStatusLabel = await _context.TaskStatusLabel.FindAsync(id);
            if (taskStatusLabel != null)
            {
                _context.TaskStatusLabel.Remove(taskStatusLabel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskStatusLabelExists(int id)
        {
          return (_context.TaskStatusLabel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
