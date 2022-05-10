namespace siges.Models {
  public class FichaAudit {
    public FichaAudit.fechaDeProgramacion FechaDeProgramacion { get; set; }
    public FichaAudit.horaDeLlegada HoraDeLlegada { get; set; }
    public FichaAudit.horaDeSalida HoraDeSalida { get; set; }
    public FichaAudit.tiempoLaborado TiempoLaboradoProgramado { get; set; }
    public FichaAudit.tiempoLaborado TiempoLaboradoReal { get; set; }
    public FichaAudit.tiempoLaborado TiempoLaboradoDiff { get; set; }
    public FichaAudit.lugar Lugar { get; set; }
    public string Direccion {get; set;}
    public float UbicacionDistancia {get;set;}
    public float EstructuraExifBiDistancia {get;set;}

    public FichaAudit(){
      FechaDeProgramacion = new FichaAudit.fechaDeProgramacion();
      HoraDeLlegada = new FichaAudit.horaDeLlegada();
      HoraDeSalida = new FichaAudit.horaDeSalida();
      TiempoLaboradoProgramado = new FichaAudit.tiempoLaborado();
      TiempoLaboradoReal = new FichaAudit.tiempoLaborado();
      TiempoLaboradoDiff = new FichaAudit.tiempoLaborado();
      Lugar = new FichaAudit.lugar();
    }

    public class fechaDeProgramacion{
      public string InicioProgramado {get; set;}
      public string InicioReal {get; set;}
      public string FinalProgramado {get; set;}
      public string FinalReal {get; set;}
    }

    public class horaDeLlegada{
      public string Programado {get; set;}
      public string Real {get; set;}
      public string Minutos {get; set;}
    }
    
    public class horaDeSalida{
      public string Programado {get; set;}
      public string Real {get; set;}
      public string Minutos {get; set;}
    }

    public class tiempoLaborado{
      public string Minutos {get; set;}
    }

    public class lugar{
      public FichaAudit.CoordenadaSencilla Programado {get; set;}
      public FichaAudit.Coordenadas Real {get; set;}
    }

    public class CoordenadaSencilla{
      public string Latitude {get; set;}
      public string Longitude {get; set;}
    }

    public class Coordenadas {
      public Coordenadas(){
        Longitude = new ExifBi.gPSLongitude();
        Latitude = new ExifBi.gPSLatitude();
      }
      public ExifBi.gPSLongitude Longitude {get; set;}
      public ExifBi.gPSLatitude Latitude {get; set;}
    }

  }
}
