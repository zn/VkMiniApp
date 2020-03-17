using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class CreatePostViewModel
    {
        public int AuthorVkId { get; set; }

        [MaxLength(4000)]
        public string Content { get; set; }
        public bool IsAnonymous { get; set; }
        public DateTime PublishDate => DateTime.Now;
        // TODO: attachments
    }
}
