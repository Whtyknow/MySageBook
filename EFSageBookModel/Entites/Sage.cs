using System;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EFSageBookModel.Entites
{
    public class Sage:IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int? Age { get; set; }

        public byte[] Photo { get; set; }

        public string City { get; set; }

        public virtual List<Book> Books { get; set; }        
    }
}
