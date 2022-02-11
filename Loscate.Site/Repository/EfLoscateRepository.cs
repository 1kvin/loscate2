using Loscate.Site.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Loscate.Site.Repository
{
    public class EfLoscateRepository : ILoscateDbRepository
    {
        private readonly LoscateDbContext loscateDbContext;
        
        public EfLoscateRepository(LoscateDbContext loscateDbContext)
        {
            this.loscateDbContext = loscateDbContext;
        }

        public DbSet<ChatMessage> ChatMessages => loscateDbContext.ChatMessages;
        public DbSet<Dialog> Dialogs => loscateDbContext.Dialogs;
        public DbSet<FirebaseUser> FirebaseUsers => loscateDbContext.FirebaseUsers;
        public DbSet<Pin> Pins => loscateDbContext.Pins;
        public void SaveChanges() => loscateDbContext.SaveChanges();
    }
}