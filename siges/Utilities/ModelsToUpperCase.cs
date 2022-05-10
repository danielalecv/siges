using System;
using siges.Models;

namespace siges.Utilities{
  public static class ModelsToUpperCase{
    public static Insumo ToUpper(Insumo i){
      i.Descripcion = String.IsNullOrEmpty(i.Descripcion) ? null : i.Descripcion.ToUpper();
      i.Clave = String.IsNullOrEmpty(i.Clave) ? null : i.Clave.ToUpper();
      i.Marca = String.IsNullOrEmpty(i.Marca) ? null : i.Marca.ToUpper();
      i.Tipo = String.IsNullOrEmpty(i.Tipo) ? null : i.Tipo.ToUpper();
      i.Opcional1 = String.IsNullOrEmpty(i.Opcional1) ? null : i.Opcional1.ToUpper();
      i.Opcional2 = String.IsNullOrEmpty(i.Opcional2) ? null : i.Opcional2.ToUpper();
      return i;
    }

    public static ActivoFijo ToUpper(ActivoFijo af){
      af.Descripcion = String.IsNullOrEmpty(af.Descripcion) ? null : af.Descripcion.ToUpper();
      af.Clave = String.IsNullOrEmpty(af.Clave) ? null : af.Clave.ToUpper();
      af.Marca = String.IsNullOrEmpty(af.Marca) ? null : af.Marca.ToUpper();
      af.Tipo = String.IsNullOrEmpty(af.Tipo) ? null : af.Tipo.ToUpper();
      af.NumeroSerie = String.IsNullOrEmpty(af.NumeroSerie) ? null : af.NumeroSerie.ToUpper();
      af.Opcional1 = String.IsNullOrEmpty(af.Opcional1) ? null : af.Opcional1.ToUpper();
      af.Opcional2 = String.IsNullOrEmpty(af.Opcional2) ? null : af.Opcional2.ToUpper();
      return af;
    }
  }
}
