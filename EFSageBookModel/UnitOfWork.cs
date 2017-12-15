using System;
using EFSageBookModel.Entites;

namespace EFSageBookModel
{
    public class UnitOfWork : IDisposable
    {
        public UnitOfWork(SageBookContext context)
        {
            this.context = context;
        }
        bool disposed = false;
        SageBookContext context;
        private BookRepository BookRep;
        private SageRepository SageRep;
        public BookRepository Books
        {
            get
            {
                if (BookRep == null)
                    BookRep = new BookRepository(context);
                return BookRep;
            }
        }
        public SageRepository Sages
        {
            get
            {
                if (SageRep == null)
                    SageRep = new SageRepository(context);
                return SageRep;
            }
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                    this.disposed = true;
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
