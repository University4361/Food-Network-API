using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreWebAPI.Models;
using CoreWebAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        public INoteRepository NoteRepository { get; set; }

        public NotesController(INoteRepository noteRepository)
        {
            NoteRepository = noteRepository;
        }

        [HttpGet]
        public IEnumerable<Note> GetAll()
        {
            return NoteRepository.GetAll();
        }

        [HttpGet("{id}", Name = "GetNote")]
        public IActionResult GetById(Guid id)
        {
            var item = NoteRepository.GetNoteById(id);
            if (item == null)
            {
                return new NotFoundResult();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]Note item)
        {
            if (item == null)
            {
                return new BadRequestResult();
            }

            NoteRepository.Add(item);
            return CreatedAtRoute("GetNote", new { controller = "Note", id = item.Id.ToString() }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody]Note item)
        {
            if (item == null || item.Id != id)
            {
                return new BadRequestResult();
            }

            var note = NoteRepository.GetNoteById(id);
            if (note == null)
            {
                return new NotFoundResult();
            }

            NoteRepository.Update(item);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            NoteRepository.Remove(id);
        }
    }
}
