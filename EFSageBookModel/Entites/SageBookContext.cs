using System;
using System.Data.Entity;


namespace EFSageBookModel.Entites
{
    public class SageBookContext : DbContext
    {        

        public SageBookContext(string name) : base(name)
        {
            Database.SetInitializer<SageBookContext>(new DbInitializer());
        }

        public DbSet<Sage> Sages { get; set; }
        public DbSet<Book> Books { get; set; }
    }




    public class DbInitializer : DropCreateDatabaseIfModelChanges<SageBookContext>
    {
        protected override void Seed(SageBookContext context)
        {
            Book[] books = new Book[]
            {
                new Book() {Name="Harry Potter", Description="Good Book" },
                new Book() {Name="Harry Potter2", Description="Good Book" },
                new Book() {Name="Dorian Grey", Description="Average Book" },
                new Book() {Name="Big Data", Description="Kappa" },
                new Book() {Name="Bukvar", Description="To difficult book" }
            };

            Sage s1 = new Sage() { Name = "Oscaw Wilde", Age = 111, City = "London", Photo = null };
            s1.Books = new System.Collections.Generic.List<Book>();
            s1.Books.Add(books[2]);

            Sage s2 = new Sage() { Name = "J Rowling", Age = 45, City = "London", Photo = null };
            s2.Books = new System.Collections.Generic.List<Book>();
            s2.Books.AddRange(new Book[] { books[0], books[1] });

            context.Books.AddRange(books);
            context.Sages.AddRange(new Sage[] { s1, s2 });


            base.Seed(context);
        }
    }
}
