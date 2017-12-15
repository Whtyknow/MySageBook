using System;
using Repository;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace EFSageBookModel.Entites
{
    public class Book : IEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual List<Sage> Sages { get; set; }
        
    }
}
