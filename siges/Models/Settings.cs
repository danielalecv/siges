using System;
using System.Collections.Generic;

namespace siges.Models {
  public class Settings {

    public int Id {get; set;}
    public string Version {get; set;}
    public string FolioPrefix {get; set;}
    public int FolioDigitsLong {get; set;}
    public string EmailHost {get; set;}
    public string EmailPort {get; set;}
    public string EmailUser {get; set;}
    public string EmailPass {get; set;}
    public int RemainingDaysToUpload {get; set;}
    public bool EmailEnableSSL {get; set;}
    public int MaxCaractersFields {get; set;}
    public bool ValidateMinimumDate {get; set;}
    public string MinimumDateCriteria {get; set;}
    public byte[] AttachmentFile1 {get; set;}
    public string AttachmentFile1Name {get; set;}
    public bool SendAttachmentFile {get; set;}
    public bool UsoDePaquetes {get; set;}
    public bool UsoDeKits {get; set;}
    public bool FaceApiUso {get; set;}
    public bool FaceApiMantenerHistorico {get; set;}
    public bool FaceApiIterarRequestUntil90OrMore {get;set;}
    public int FaceApiMinCantEntrenamiento {get;set;}
    public bool CustomVisionUso {get;set;}
    public List<BulkUploadTemplate> Templates {get;set;}

    public Settings() { }
  }
}
