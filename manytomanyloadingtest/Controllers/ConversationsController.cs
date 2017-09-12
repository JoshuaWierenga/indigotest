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
    public class ConversationsController : Controller
    {
        private readonly MTMContext _context;

        public ConversationsController(MTMContext context)
        {
            _context = context;
        }

        // GET: Conversations
        public async Task<IActionResult> Index()
        {
            return View(await _context.Conversations.ToListAsync());
        }

        // GET: Conversations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .Include(i => i.UserConversations)
                    .ThenInclude(i => i.User)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // GET: Conversations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Conversations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConversationId,GroupName,IsGroupChat")] Conversation conversation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(conversation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(conversation);
        }

        // GET: Conversations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations.SingleOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }
            return View(conversation);
        }

        // POST: Conversations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConversationId,GroupName,IsGroupChat")] Conversation conversation)
        {
            if (id != conversation.ConversationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(conversation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConversationExists(conversation.ConversationId))
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
            return View(conversation);
        }

        // GET: Conversations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conversation = await _context.Conversations
                .SingleOrDefaultAsync(m => m.ConversationId == id);
            if (conversation == null)
            {
                return NotFound();
            }

            return View(conversation);
        }

        // POST: Conversations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var conversation = await _context.Conversations.SingleOrDefaultAsync(m => m.ConversationId == id);
            _context.Conversations.Remove(conversation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConversationExists(int id)
        {
            return _context.Conversations.Any(e => e.ConversationId == id);
        }
    }
}
