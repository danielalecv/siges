namespace siges.Models {
  public class ExifBi {
    public ExifBi.gPSLatitudeRef GPSLatitudeRef { get; set; }
    public ExifBi.gPSLatitude GPSLatitude { get; set; }
    public ExifBi.gPSLongitudeRef GPSLongitudeRef { get; set; }
    public ExifBi.gPSLongitude GPSLongitude { get; set; }
    public ExifBi.gPSAltitudeRef GPSAltitudeRef { get; set; }
    public ExifBi.gPSAltitude GPSAltitude { get; set; }
    public ExifBi.dateTimeOriginal DateTimeOriginal { get; set; }

    public static string ValueToString(int[] v){
      string s = "[";
      for(int i = 0; i < v.Length; i++){
        s = s + v[i];
        if(i+1 == v.Length)
          break;
        else
          s = s + ",";
      }
      return s = s + "]";
    }

    public static string ValueToString(int[][] v){
      string s = "[";
      for(int i = 0; i < v.Length; i++){
        s = s + "[";
        for(int j = 0; j < v[i].Length; j++){
          s = s + v[i][j];
          if(j+1 == v[i].Length)
            break;
          else
            s = s + ",";
        }
        s = s + "]";
        if(i+1 == v.Length)
          break;
        else
          s = s + ",";
      }
      return s = s + "]";
    }

    public static string ValueToString(string[] v){
      string s = "[";
      for(int i = 0; i < v.Length; i++){
        s = s + v[i];
        if(i+1 == v.Length)
          break;
        else
          s = s + ",";
      }
      return s = s + "]";
    }

    public static string ValueToString(string[][] v){
      string s = "[";
      for(int i = 0; i < v.Length; i++){
        s = s + "[";
        for(int j = 0; j < v[i].Length; j++){
          s = s + v[i][j];
          if(j+1 == v[i].Length)
            break;
          else
            s = s + ",";
        }
        s = s + "]";
        if(i+1 == v.Length)
          break;
        else
          s = s + ",";
      }
      return s = s + "]";
    }

    public class dateTimeOriginal{
      public int id {get; set;}
      public string[] value {get; set;}
      public string description {get; set;}
    }
    
    public class gPSLongitude{
      public int id {get; set;}
      public int[][] value {get; set;}
      public double description {get; set;}
    }

    public class gPSLongitudeRef{
      public int id {get; set;}
      public string[] value {get; set;}
      public string description {get; set;}
    }

    public class gPSLatitude{
      public int id {get; set;}
      public int[][] value {get; set;}
      public string description {get; set;}
    }

    public class gPSLatitudeRef{
      public int id {get; set;}
      public string[] value {get; set;}
      public string description {get; set;}
    }

    public class gPSAltitude{
      public int id {get; set;}
      public int[] value {get; set;}
      public string description {get; set;}
    }

    public class gPSAltitudeRef{
      public int id {get; set;}
      public int value {get; set;}
      public string description {get; set;}
    }
//"GPSLatitudeRef": {
  //"id": 1,
    //"value": [
      //"N"
    //],
    //"description": "North latitude"
//},
  //"GPSLatitude": {
    //"id": 2,
    //"value": [
      //[
        //19,
    //1
      //],
      //[
        //:
          //"description": "Version 2.2"
  //},

  //"GPSLongitudeRef": {
    //"id": 3,
    //"value": [
      //"W"
    //],
    //"description": "West longitude"
  //},
  //"GPSLongitude": {
    //"id": 4,
    //"value": [
      //[
        //99,
    //1
      //],
      //[
        //12,
      //1
      //],
      //[
        //543276,
      //10000
      //]
    //],
    //"description": 99.215091
  //}
//"GPSAltitudeRef": {
  //"id": 5,
  //"value": 0,
  //"description": "Sea level"
//},
  //"GPSAltitude": {
    //"id": 6,
    //"value": [
      //2254907,
    //1000
    //],
    //"description": "2254.907 m"
  //}
  }
}
  //"GPSLatitudeRef": {
    //"id": 1,
    //"value": [
      //"N"
    //],
    //"description": "North latitude"
  //},
  //"GPSLatitude": {
    //"id": 2,
    //"value": [
      //[
        //19,
    //1
      //],
      //[
        //41,
      //1
      //],
      //[
        //160705,
      //10000
      //]
    //],
    //"description": 19.687797361111112
  //},

//"DateTimeOriginal": {
  //"id": 36867,
  //"value": [
    //"2019:08:31 18:55:39"
    //],
  //"description": "2019:08:31 18:55:39"
//}
