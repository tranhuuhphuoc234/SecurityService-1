﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIUser.Models;
using WebAPIUser.Models.Context;

namespace WebAPIUser.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class commentsController : ControllerBase
    {
        private readonly Security_ServiceContext _context;

        public commentsController(Security_ServiceContext context)
        {
            _context = context;
        }

        // GET: api/comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<comment>>> Getcomments()
        {
            return await _context.comments.ToListAsync();
        }

        // GET: api/comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<comment>> Getcomment(int id)
        {
            var comment = await _context.comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putcomment(int id, comment comment)
        {
            if (id != comment.id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!commentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<comment>> Postcomment(comment comment)
        {
            _context.comments.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcomment", new { id = comment.id }, comment);
        }

        // DELETE: api/comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletecomment(int id)
        {
            var comment = await _context.comments.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool commentExists(int id)
        {
            return _context.comments.Any(e => e.id == id);
        }
        //search by id employee
        [HttpGet("SearchByEmployee/{employee}")]
        public async Task<ActionResult<IEnumerable<comment>>> searchByEmployee(int employee)
        {
            return await _context.comments.Where(a => a.employee == employee).ToListAsync();
        }
        //search by id client
        [HttpGet("SearchByClient/{client}")]
        public async Task<ActionResult<IEnumerable<comment>>> searchByClient(int client)
        {
            return await _context.comments.Where(a => a.client == client).ToListAsync();
        }
    }
}
