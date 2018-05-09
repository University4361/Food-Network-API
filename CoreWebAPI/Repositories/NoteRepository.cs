using CoreWebAPI.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CoreWebAPI.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private static readonly ConcurrentDictionary<string, Note> Notes = new ConcurrentDictionary<string, Note>();

        public NoteRepository()
        {
            Add(new Note { Title = "My First Note", Content = "A worthy content" });
            Add(new Note { Title = "Second Note", Content = "A worthy content" });
            Add(new Note { Title = "Third Note", Content = "A worthy content" });
            Add(new Note { Title = "Fourth Note", Content = "A worthy content" });
            Add(new Note { Title = "Fifth Note", Content = "A worthy content" });
            Add(new Note { Title = "Sixth Note", Content = "A worthy content" });
            Add(new Note { Title = "Seventh Note", Content = "A worthy content" });
        }

        public IEnumerable<Note> GetAll()
        {
            return Notes.Values;
        }

        public Note GetNoteById(Guid id)
        {
            Note item;
            Notes.TryGetValue(id.ToString(), out item);
            return item;
        }

        public void Add(Note item)
        {
            item.Id = Guid.NewGuid();
            Notes[item.Id.ToString()] = item;
        }

        public void Update(Note item)
        {
            Notes[item.Id.ToString()] = item;
        }

        public Note Remove(Guid id)
        {
            Note item;
            Notes.TryGetValue(id.ToString(), out item);
            Notes.TryRemove(id.ToString(), out item);
            return item;
        }
    }
}
