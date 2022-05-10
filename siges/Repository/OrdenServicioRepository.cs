using Microsoft.EntityFrameworkCore;
using siges.Data;
using siges.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace siges.Repository {
  public class OrdenServicioRepository : Repository<OrdenServicio>, IOrdenServicioRepository {
    private ApplicationDbContext _ctx;
    public OrdenServicioRepository(ApplicationDbContext context) : base(context) {
      _ctx = context;
    }

    public IQueryable<OrdenServicio> GetAllDeleted(){
      var result = entities.Where(r => r.Estatus == false).Include(r => r.Cliente).Include(r => r.Contrato).Include(r => r.Ubicacion).Include(r => r.Servicio);
      Console.WriteLine("\n\nEstoy en repo de os para eliminadas {0}\n", result.Any());
      return result;
    }

    public IQueryable<OrdenServicio> GetAll(bool estatus) {
      Console.WriteLine("\n\nEstoy en Repositorio de os con variable {0}\n", estatus);
      var result = entities.Where(r => r.Estatus == estatus).Include(r => r.Ubicacion).Include(r => r.Contrato).Include(r => r.Cliente).Include(r => r.LineaNegocio).Include(r => r.Servicio).Include(r => r.Insumos).ThenInclude(r => r.Insumo).Include(r => r.Personal).ThenInclude(r => r.Persona).Include(r => r.Activos).ThenInclude(r => r.ActivoFijo).Where(a => a.Estatus == true).Include(r => r.PersonaComercial).Include(r => r.PersonaValida).OrderByDescending(r => r.Id).AsQueryable();
      Console.WriteLine("\n\nResult {0}\n", result.Any());
      return result;
    }

    public OrdenServicio GetByIdOS(int id) {
      return entities.Where(r => r.Id == id).Include(r => r.Ubicacion).Include(r => r.Contrato).Include(r => r.Cliente).Include(r => r.LineaNegocio).Include(r => r.Servicio).Include(r => r.Insumos).ThenInclude(r => r.Insumo).Include(r => r.Personal).ThenInclude(r => r.Persona).Include(r => r.Activos).ThenInclude(r => r.ActivoFijo).Where(a => a.Estatus == true).Include(r => r.PersonaComercial).Include(r => r.PersonaValida).Single();
    }

    /*public IQueryable<OrdenServicio> GetByPersonaComercialId(int id){
      Console.WriteLine("\n\n\tEstoy dentro de GetByPersonaComercialId: "+id+"\n");
      var ent = entities.Where(r => r.PersonaComercial.Id == id).DefaultIfEmpty().Include(r => r.Ubicacion).Include(r => r.Contrato).Include(r => r.Cliente).Include(r => r.LineaNegocio).Include(r => r.Servicio).Include(r => r.PersonaComercial).Include(r => r.PersonaValida).Include(r => r.Insumos).ThenInclude(r => r.Insumo).Include(r => r.Personal).ThenInclude(r => r.Persona).Include(r => r.Activos).ThenInclude(r => r.ActivoFijo).OrderByDescending(r => r.Id).AsQueryable();
      Console.WriteLine("\n\n\tTerminó de hacer el query: "+ent+"\n");
      return ent;
    }*/

    public IQueryable<OrdenServicio> GetByPersonaComercialId(int id){
      Console.WriteLine("\n\n\tEstoy dentro de GetByPersonaComercialId: "+id+"\n");
      var ent = entities.Where(r => r.PersonaComercial.Id == id).AsQueryable();
      Console.WriteLine("\n\n\tTerminó de hacer el query: "+ent+"\n");
      return ent;
    }

    public OrdenServicio GetOSbyFolio(string folio) {
      return entities.Where(r => r.Estatus == true && r.Folio == folio).DefaultIfEmpty().Include(r => r.Ubicacion).Include(r => r.Contrato).Include(r => r.Cliente).Include(r => r.LineaNegocio).Include(r => r.Servicio).Include(r => r.Insumos).ThenInclude(r => r.Insumo).Include(r => r.Personal).ThenInclude(r => r.Persona).Include(r => r.Activos).ThenInclude(r => r.ActivoFijo).Include(r => r.PersonaComercial).Include(r => r.PersonaValida).Single();
    }

    public IQueryable<OrdenServicio> GetOrdenServicio(){
      return entities.Where(r => r.Estatus == true).Include(r => r.Servicio).Include(r => r.Ubicacion);
    }

    /*
     * Consulta Operador.Estado.NuevoEstado para determinar si hay actividad reportada, información requerida para permitir cancelar o no la orden de servicio
     *
     * */
    public bool IsCancelable(int osId){
      var conn = RelationalDatabaseFacadeExtensions.GetDbConnection(_ctx.Database);
      if(((System.Data.SqlClient.SqlConnection)conn).State == System.Data.ConnectionState.Open)
        ((System.Data.SqlClient.SqlConnection)conn).Close();
      ((System.Data.SqlClient.SqlConnection)conn).Open();
      System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("select es.nuevoestado, os.id as ordenservicio from estado es left join operador op on es.operadorid = op.id left join ordenservicio os on op.ordenservicioid = os.id where os.id = @OsId and (es.nuevoestado != 'asistencia' and es.nuevoestado != 'sitio')", (System.Data.SqlClient.SqlConnection)conn);
      cmd.Parameters.Add("@OsId", System.Data.SqlDbType.Int);
      cmd.Parameters["@OsId"].Value = osId;
      var r = cmd.ExecuteReader();
      if(r.HasRows){
        r.Close();
        ((System.Data.SqlClient.SqlConnection)conn).Close();
        return false;
      }
      r.Close();
      ((System.Data.SqlClient.SqlConnection)conn).Close();
      return true;
    }

    //public IQueryable<OrdenServicio> GetOrdenServicioDetail(int id){
      //return entities.Where(r => r.Id == id).Include(r => r.Servicio).Include(r => r.Ubicacion).Include(r => r.Contrato).ThenInclude(op => op)
    //}
  }
}
