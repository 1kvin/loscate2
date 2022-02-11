using Loscate.Site.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Loscate.Site.Repository
{
    public interface ILoscateDbRepository
    {
        public DbSet<ChatMessage> ChatMessages { get; }
        public DbSet<Dialog> Dialogs { get; }
        public DbSet<FirebaseUser> FirebaseUsers { get; }
        public DbSet<Pin> Pins { get; }
        
        public void SaveChanges();
    }
}