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
    public class GameShopsController : Controller
    {
        private readonly GameContext _context;

        public GameShopsController(GameContext context)
        {
            _context = context;
        }

        // GET: GameShops
        public async Task<IActionResult> Index()
        {
            var gameContext = _context.GameShops.Include(g => g.GameLibrary).Include(g => g.GameSystem);
            return View(await gameContext.ToListAsync());
        }

        // GET: GameShops/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameShop = await _context.GameShops
                .Include(g => g.GameLibrary)
                .Include(g => g.GameSystem)
                .FirstOrDefaultAsync(m => m.GameShopID == id);
            if (gameShop == null)
            {
                return NotFound();
            }

            return View(gameShop);
        }

        // GET: GameShops/Create
        public IActionResult Create()
        {
            ViewData["GameLibraryID"] = new SelectList(_context.GameLibraries, "GameLibraryID", "Name");
            ViewData["GameSystemID"] = new SelectList(_context.GameSystems, "GameSystemID", "SystemName");
            return View();
        }

        // POST: GameShops/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameShopID,GameShopName,Description,GameLibraryID,GameSystemID,Address,CreationDate")] GameShop gameShop)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameShop);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameLibraryID"] = new SelectList(_context.GameLibraries, "GameLibraryID", "Name", gameShop.GameLibraryID);
            ViewData["GameSystemID"] = new SelectList(_context.GameSystems, "GameSystemID", "SystemName", gameShop.GameSystemID);
            return View(gameShop);
        }

        // GET: GameShops/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameShop = await _context.GameShops.FindAsync(id);
            if (gameShop == null)
            {
                return NotFound();
            }
            ViewData["GameLibraryID"] = new SelectList(_context.GameLibraries, "GameLibraryID", "Name", gameShop.GameLibraryID);
            ViewData["GameSystemID"] = new SelectList(_context.GameSystems, "GameSystemID", "SystemName", gameShop.GameSystemID);
            return View(gameShop);
        }

        // POST: GameShops/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameShopID,GameShopName,Description,GameLibraryID,GameSystemID,Address,CreationDate")] GameShop gameShop)
        {
            if (id != gameShop.GameShopID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameShop);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameShopExists(gameShop.GameShopID))
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
            ViewData["GameLibraryID"] = new SelectList(_context.GameLibraries, "GameLibraryID", "Name", gameShop.GameLibraryID);
            ViewData["GameSystemID"] = new SelectList(_context.GameSystems, "GameSystemID", "SystemName", gameShop.GameSystemID);
            return View(gameShop);
        }

        // GET: GameShops/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameShop = await _context.GameShops
                .Include(g => g.GameLibrary)
                .Include(g => g.GameSystem)
                .FirstOrDefaultAsync(m => m.GameShopID == id);
            if (gameShop == null)
            {
                return NotFound();
            }

            return View(gameShop);
        }

        // POST: GameShops/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameShop = await _context.GameShops.FindAsync(id);
            _context.GameShops.Remove(gameShop);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameShopExists(int id)
        {
            return _context.GameShops.Any(e => e.GameShopID == id);
        }
    }
}
