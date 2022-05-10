using System;
using System.Collections;
using System.Collections.Generic;
using siges.Models;

namespace siges.Models{
  public class PaqueteInsumo{
    public int Id {get; set;}
    public bool Estatus {get; set;}
    public Insumo Insumo {get; set;}
    public Paquete Paquete {get; set;}
    public int Cantidad {get; set;}

    public PaqueteInsumo(){
      this.Insumo = new Insumo();
      this.Paquete = new Paquete();
    }
  }
}
