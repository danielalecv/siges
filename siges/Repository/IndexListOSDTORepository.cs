using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.DTO;
using System.Linq;

namespace siges.Repository
{
    public class IndexListOSDTORepository : Repository<IndexListOSDTO>, IIndexListOSDTORepository
    {
        public IndexListOSDTORepository(ApplicationDbContext context) : base(context) { }

        public IQueryable<IndexListOSDTO> GetAllOS()
        {
            return entities.FromSqlRaw("" +
                "SELECT OS.Id AS Id, " +
                "OS.Folio AS Folio, " +
                "OS.FechaInicio AS FechaInicio, " +
                "CL.RazonSocial AS Cliente, " +
                "CO.Nombre AS Contrato, " +
                "CO.Tipo AS ContratoTipo, " +
                "U.Nombre AS Ubicacion, " +
                "S.Nombre AS Servicio, " +
                "l.Nombre AS LineaNegocio, " +
                "OS.EstatusServicio AS EstatusServicio, " +
                "CONCAT (P.Nombre, ' ', P.Paterno, '', P.Materno) PersonaComercial "  +
                "FROM OrdenServicio OS "  +
                "JOIN Cliente CL ON OS.ClienteId = CL.Id "  +
                "JOIN Contrato CO ON OS.ContratoId = CO.Id "  +
                "JOIN Ubicacion U ON OS.UbicacionId = U.Id "  +
                "JOIN Servicio S ON OS.ServicioId = S.Id "  +
                "JOIN LineaNegocio l ON OS.LineaNegocioId = l.Id "  +
                "JOIN Persona P ON OS.PersonaComercialId = P.Id "  +
                "WHERE OS.Estatus = 1 " );
        }
    }
}
