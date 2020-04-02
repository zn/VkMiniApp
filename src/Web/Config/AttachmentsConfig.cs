using System.Collections.Generic;

namespace Web.Config
{
    public class AttachmentsConfig
    {
        public string Directory { get; set; }
        public long FileMaxSize { get; set; }
        public int MaxNumberFiles { get; set; }
        public List<string> AllowedExtensions { get; set; }
    }
}