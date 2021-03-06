﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameLibrary.Data;
using GameLibrary.Data.Entities;
using GameLibrary.Services;

namespace GameLibrary.Controllers
{
    public class LibrariesController : Controller
    {
        private readonly IGameRepository gameRepository;
        private readonly GameContext context;
        private readonly IMailService mailService;

        public LibrariesController(IGameRepository gameRepository, GameContext context,IMailService mailService)
        {
            this.gameRepository = gameRepository;
            this.context = context;
            this.mailService = mailService;
        }

        // GET: Libraries
        public async Task<IActionResult> Index()
        {
            var games = gameRepository.GetGameLibraries();
            //fluent syntax
            //var gameContext2 = context.GameLibraries.OrderBy(p => p.Name).ToList();
            //linq
            //var gameContext3 = from p in context.GameLibraries orderby p.Name select p;
            //return View(gameContext3.ToList());
            return View(games);
        }

        // GET: Libraries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await context.GameLibraries
                .Include(l => l.GameSystems)
                .FirstOrDefaultAsync(m => m.GameLibraryID == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // GET: Libraries/Create
        public IActionResult Create()
        {
            ViewData["GameSystemID"] = new SelectList(context.GameSystems, "GameSystemID", "SystemName");
            return View();
        }

        // POST: Libraries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameLibraryID,Name,Description,GameSystemID,Rating,DiscType,CreationDate")] Games library)
        {
            if (ModelState.IsValid)
            {
                context.Add(library);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameSystemID"] = new SelectList(context.GameSystems, "GameSystemID", "SystemName", library.GameSystemID);
            return View(library);
        }

        // GET: Libraries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await context.GameLibraries.FindAsync(id);
            if (library == null)
            {
                return NotFound();
            }
            ViewData["GameSystemID"] = new SelectList(context.GameSystems, "GameSystemID", "SystemName", library.GameSystemID);
            return View(library);
        }

        // POST: Libraries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameLibraryID,Name,Description,GameSystemID,Rating,DiscType,CreationDate")] Games library)
        {
            if (id != library.GameLibraryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(library);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryExists(library.GameLibraryID))
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
            ViewData["GameSystemID"] = new SelectList(context.GameSystems, "GameSystemID", "SystemName", library.GameSystemID);
            return View(library);
        }

        // GET: Libraries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var library = await context.GameLibraries
                .Include(l => l.GameSystems)
                .FirstOrDefaultAsync(m => m.GameLibraryID == id);
            if (library == null)
            {
                return NotFound();
            }

            return View(library);
        }

        // POST: Libraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var library = await context.GameLibraries.FindAsync(id);
            context.GameLibraries.Remove(library);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryExists(int id)
        {
            return context.GameLibraries.Any(e => e.GameLibraryID == id);
        }
    }
}
