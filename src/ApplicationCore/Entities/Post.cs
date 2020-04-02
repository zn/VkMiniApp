using System;
using System.Collections.Generic;

namespace ApplicationCore.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsAnonymously { get; set; }
        public bool IsEdited { get; set; }
        public bool IsDeleted { get; set; }


        public int AuthorVkId { get; set; }
        public User Author { get; set; }
        public ICollection<Attachment> Attachments { get; set; }

        // comments
    }
}
