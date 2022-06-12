using siges.Data;
using siges.Models;

namespace siges.Repository
{
    public class OsRecurrenteRepository : Repository<OsRecurrente>, IOsRecurrente
    {
        public OsRecurrenteRepository(ApplicationDbContext context) : base(context) { }

    }
}