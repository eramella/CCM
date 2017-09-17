using CCM.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCM.Business.Repositories
{
    public class TagRepository
    {
        private CCMContext _context;

        public TagRepository(CCMContext context)
        {
            _context = context;
        }

        public async Task<List<Tag>> GetTags()
        {
            return await _context.Tags.ToListAsync();
        }

        public async Task<Tag> GetTagById(string id)
        {
            return await _context.Tags.FindAsync(id);
        }

        public async Task<Tag> GetTagByName(string name)
        {
            return await _context.Tags.FirstOrDefaultAsync(tag => String.Compare(tag.Name, name, true) == 0);
        }

        public async Task<Tag> FindOrAdd(string name)
        {
            var existingTag = await _context.Tags.FirstOrDefaultAsync(tag => String.Compare(tag.Name, name, true) == 0);
            if (existingTag == null)
            {
                Tag newTag = new Tag()
                {
                    Name = name.Trim()
                };
                this.AddTag(newTag);
                return newTag;
            }

            return existingTag;
        }

        public void UpdateTag(Tag tag)
        {
            if (_context.Entry<Tag>(tag).State != EntityState.Modified)
            {
                _context.Entry<Tag>(tag).State = EntityState.Modified;
            }
        }

        public void AddTag(Tag tag)
        {
            _context.Tags.Add(tag);
        }

        public void DeleteTag(Tag tag)
        {
            _context.Remove(tag);
        }

        public async Task<List<TagSession>> GenerateTagSessions(IEnumerable<string> tags, string sessionId)
        {
            List<TagSession> tagSessions = new List<TagSession>();
            foreach (var tag in tags)
            {
                var newTag = await FindOrAdd(tag);
                TagSession newTagSession = new TagSession()
                {
                    SessionId = sessionId,
                    TagId = newTag.Id
                };
                tagSessions.Add(newTagSession);
            }
            return tagSessions;
        }
    }
}
