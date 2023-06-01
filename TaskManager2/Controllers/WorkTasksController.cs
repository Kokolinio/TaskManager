using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManager2.Data;
using TaskManager2.Models;

namespace TaskManager2.Controllers
{
    public class WorkTasksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WorkTasksController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: WorkTasks
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.WorkTask.Include(w => w.TaskStatusLabel).Include(x => x.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: WorkTasks/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.WorkTask == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTask
                .Include(w => w.TaskStatusLabel)
                .Include(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTask == null)
            {
                return NotFound();
            }

            return View(workTask);
        }

        // GET: WorkTasks/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["TaskStatusLabelId"] = new SelectList(_context.TaskStatusLabel, "Id", "Name");
            return View();
        }

        // POST: WorkTasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Value,Description,TaskStatusLabelId,ExecutionDate,UserId")] WorkTask workTask)
        {
            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.GetUserAsync(User);

                workTask.User = currentUser;

                _context.Add(workTask);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TaskStatusLabelId"] = new SelectList(_context.TaskStatusLabel, "Id", "Name", workTask.TaskStatusLabelId);
            return View(workTask);
        }

        // GET: WorkTasks/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.WorkTask == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTask.FindAsync(id);
            if (workTask == null)
            {
                return NotFound();
            }
            ViewData["TaskStatusLabelId"] = new SelectList(_context.TaskStatusLabel, "Id", "Name", workTask.TaskStatusLabelId);
            return View(workTask);
        }

        // POST: WorkTasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Value,Description,TaskStatusLabelId,ExecutionDate,UserId")] WorkTask workTask)
        {
            if (id != workTask.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(workTask);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkTaskExists(workTask.Id))
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
            ViewData["TaskStatusLabelId"] = new SelectList(_context.TaskStatusLabel, "Id", "Name", workTask.TaskStatusLabelId);
            return View(workTask);
        }

        // GET: WorkTasks/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.WorkTask == null)
            {
                return NotFound();
            }

            var workTask = await _context.WorkTask
                .Include(w => w.TaskStatusLabel)
                .Include(x => x.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (workTask == null)
            {
                return NotFound();
            }

            return View(workTask);
        }

        // POST: WorkTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.WorkTask == null)
            {
                return Problem("Entity set 'ApplicationDbContext.WorkTask'  is null.");
            }
            var workTask = await _context.WorkTask.FindAsync(id);
            if (workTask != null)
            {
                _context.WorkTask.Remove(workTask);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WorkTaskExists(int id)
        {
          return (_context.WorkTask?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
