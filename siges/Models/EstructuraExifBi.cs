using System;

namespace siges.Models{
  public class EstructuraExifBi{
    public int Id {get; set;}
    public int DateTimeOriginalId {get; set;}
    public DateTime DateTimeOriginal {get; set;}
    public int GPSLongitudeId {get; set;}
    public string GPSLongitudeValue {get; set;}
    public double GPSLongitudeDescription {get; set;}
    public int GPSLongitudeRefId {get; set;}
    public string GPSLongitudeRefValue {get; set;}
    public string GPSLongitudeRefDescription {get; set;}
    public int GPSLatitudeId {get; set;}
    public string GPSLatitudeValue {get; set;}
    public string GPSLatitudeDescription {get; set;}
    public int GPSLatitudeRefId {get; set;}
    public string GPSLatitudeRefValue {get; set;}
    public string GPSLatitudeRefDescription {get; set;}
    public int GPSAltitudeId {get; set;}
    public string GPSAltitudeValue {get; set;}
    public string GPSAltitudeDescription {get; set;}
    public int GPSAltitudeRefId {get; set;}
    public int GPSAltitudeRefValue {get; set;}
    public string GPSAltitudeRefDescription {get; set;}
    public float Distancia {get; set;}
  }
}
