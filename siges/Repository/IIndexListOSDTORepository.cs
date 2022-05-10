using siges.DTO;
using System.Linq;

namespace siges.Repository
{
    public interface IIndexListOSDTORepository : IRepository<IndexListOSDTO>
    {
        public IQueryable<IndexListOSDTO> GetAllOS();
    }
}
