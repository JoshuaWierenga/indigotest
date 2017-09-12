using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using manytomanyloadingtest.Models;

namespace manytomanyloadingtest.Controllers
{
    public class UserConversations1Controller : Controller
    {
        private readonly MTMContext _context;

        public UserConversations1Controller(MTMContext context)
        {
            _context = context;
        }

        // GET: UserConversations1
        public async Task<IActionResult> Index()
        {
            var mTMContext = _context.UserConversations.Include(u => u.Conversation).Include(u => u.User);
            return View(await mTMContext.ToListAsync());
        }

        // GET: UserConversations1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userConversation = await _context.UserConversations
                .Include(u => u.Conversation)
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userConversation == null)
            {
                return NotFound();
            }

            return View(userConversation);
        }

        // GET: UserConversations1/Create
        public IActionResult Create()
        {
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: UserConversations1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,ConversationId,IsAdmin,GroupName")] UserConversation userConversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userConversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId", userConversation.ConversationId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userConversation.UserId);
            return View(userConversation);
        }

        // GET: UserConversations1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userConversation = await _context.UserConversations.SingleOrDefaultAsync(m => m.UserId == id);
            if (userConversation == null)
            {
                return NotFound();
            }
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId", userConversation.ConversationId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userConversation.UserId);
            return View(userConversation);
        }

        // POST: UserConversations1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,ConversationId,IsAdmin,GroupName")] UserConversation userConversation)
        {
            if (id != userConversation.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userConversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserConversationExists(userConversation.UserId))
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
            ViewData["ConversationId"] = new SelectList(_context.Conversations, "ConversationId", "ConversationId", userConversation.ConversationId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userConversation.UserId);
            return View(userConversation);
        }

        // GET: UserConversations1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userConversation = await _context.UserConversations
                .Include(u => u.Conversation)
                .Include(u => u.User)
                .SingleOrDefaultAsync(m => m.UserId == id);
            if (userConversation == null)
            {
                return NotFound();
            }

            return View(userConversation);
        }

        // POST: UserConversations1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userConversation = await _context.UserConversations.SingleOrDefaultAsync(m => m.UserId == id);
            _context.UserConversations.Remove(userConversation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserConversationExists(int id)
        {
            return _context.UserConversations.Any(e => e.UserId == id);
        }
    }
}
