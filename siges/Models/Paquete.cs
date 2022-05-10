using System;
using System.Collections;
using System.Collections.Generic;
using siges.Models;

namespace siges.Models{
  public class Paquete{
    public int Id {get; set;}
    public string Clasificacion {get; set;}
    public string Descripcion {get; set;}
    public Persona Crea {get; set;}
    public bool Estatus {get; set;}
    public DateTime FechaAdmin {get; set;}
    public string Observaciones {get; set;}

    public Paquete(){
      this.Crea = new Persona();
    }
  }
}
