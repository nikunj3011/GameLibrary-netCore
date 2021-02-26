using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameLibrary.Data;
using GameLibrary.Data.Entities;

namespace GameLibrary.Controllers
{
    public class GameSystemsController : Controller
    {
        private readonly GameContext _context;

        public GameSystemsController(GameContext context)
        {
            _context = context;
        }

        // GET: GameSystems
        public async Task<IActionResult> Index()
        {
            return View(await _context.GameSystems.ToListAsync());
        }

        // GET: GameSystems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameSystem = await _context.GameSystems
                .FirstOrDefaultAsync(m => m.GameSystemID == id);
            if (gameSystem == null)
            {
                return NotFound();
            }

            return View(gameSystem);
        }

        // GET: GameSystems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GameSystems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameSystemID,SystemName,CreationDate")] GameSystem gameSystem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameSystem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameSystem);
        }

        // GET: GameSystems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameSystem = await _context.GameSystems.FindAsync(id);
            if (gameSystem == null)
            {
                return NotFound();
            }
            return View(gameSystem);
        }

        // POST: GameSystems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameSystemID,SystemName,CreationDate")] GameSystem gameSystem)
        {
            if (id != gameSystem.GameSystemID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameSystem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameSystemExists(gameSystem.GameSystemID))
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
            return View(gameSystem);
        }

        // GET: GameSystems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameSystem = await _context.GameSystems
                .FirstOrDefaultAsync(m => m.GameSystemID == id);
            if (gameSystem == null)
            {
                return NotFound();
            }

            return View(gameSystem);
        }

        // POST: GameSystems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameSystem = await _context.GameSystems.FindAsync(id);
            _context.GameSystems.Remove(gameSystem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameSystemExists(int id)
        {
            return _context.GameSystems.Any(e => e.GameSystemID == id);
        }
    }
}
