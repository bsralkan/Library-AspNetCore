using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Library.Models;

namespace Library.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext (DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Library.Models.Books> Books { get; set; }

        public DbSet<Library.Models.BookDetails> BookDetails { get; set; }
        
    }
}
