using System;

namespace ApplicationCore.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string AuthorVkId { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsAnonymously { get; set; }

        // attachments
        // comments
    }
}
