using EFSageBookModel.Entites;
using Repository;

namespace EFSageBookModel
{

    public class SageRepository : BaseRepository<Sage>
        {
            public SageRepository(SageBookContext context) : base(context) { }
        }
    
}
