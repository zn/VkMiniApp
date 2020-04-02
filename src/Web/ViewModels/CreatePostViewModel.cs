using Microsoft.AspNetCore.Http;
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
        public int UserId { get; set; }

        [MaxLength(4000)]
        public string Content { get; set; }
        public DateTime PublishDate => DateTime.Now;
        public List<IFormFile> Attachments { get; set; }
        public bool IsAnonymous { get; set; }
    }
}
