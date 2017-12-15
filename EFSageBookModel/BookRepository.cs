using Repository;
using EFSageBookModel.Entites;

namespace EFSageBookModel
{
    public class BookRepository : BaseRepository<Book>
    {
        public BookRepository(SageBookContext context) : base(context) { }

       
    }


}
