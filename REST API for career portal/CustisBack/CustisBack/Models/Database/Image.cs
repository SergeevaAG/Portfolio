using System;
using System.Collections.Generic;

namespace CustisBack.Models.Database
{
    public partial class Image
    {
        public Image()
        {
            Clients = new HashSet<Client>();
            Possibilities = new HashSet<Possibility>();
            Socials = new HashSet<Social>();
            WorkersStories = new HashSet<WorkersStory>();
        }

        public Guid Id { get; set; }
        public byte[]? Image1 { get; set; }
        public bool? ShowGallery { get; set; }
        public string? Desc { get; set; }
        public string? ContentType { get; set; }

        public virtual ICollection<Client> Clients { get; set; }
        public virtual ICollection<Possibility> Possibilities { get; set; }
        public virtual ICollection<Social> Socials { get; set; }
        public virtual ICollection<WorkersStory> WorkersStories { get; set; }
    }
}
