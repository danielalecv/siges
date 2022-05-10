using System;
using System.Collections.Generic;

namespace siges.Models{
  public class CustomVision{
    public DateTime created {get;set;}
    public string id {get;set;}
    public string iteration {get;set;}
    public List<CustomVision.prediction> predictions {get;set;}

    public CustomVision(){
      predictions = new List<CustomVision.prediction>();
    }

    public class prediction{
      public float probability {get;set;}
      public string tagId {get;set;}
      public string tagName{get;set;}
      public CustomVision.prediction.BoundingBox boundingBox {get;set;}
      public prediction(){
        boundingBox = new CustomVision.prediction.BoundingBox();
      }

      public class BoundingBox{
        public float height {get;set;}
        public float left {get;set;}
        public float top {get;set;}
        public float width {get;set;}
      }
    }
  }
}
/*{
   "created" : "2020-08-06T00:10:26.600Z",
   "id" : "6652d138-412e-4adc-a489-96142863ea87",
   "iteration" : "6ebc6b90-e787-4577-8616-afadde322b95",
   "predictions" : [
      {
         "boundingBox" : {
            "height" : 0.0633767247,
            "left" : 0.726865,
            "top" : 0.209464431,
            "width" : 0.04692167
         },
         "probability" : 0.0101400772,
         "tagId" : "b6b73a81-a1f3-4eff-ba7f-4098dd16277b",
         "tagName" : "casco de seguridad"
      },
      {
         "boundingBox" : {
            "height" : 0.215107262,
            "left" : 0.384967446,
            "top" : 0.00437586,
            "width" : 0.430045724
         },
         "probability" : 0.9979216,
         "tagId" : "b6b73a81-a1f3-4eff-ba7f-4098dd16277b",
         "tagName" : "casco de seguridad"
      },
      {
         "boundingBox" : {
            "height" : 0.226318181,
            "left" : 0.6655365,
            "top" : 0.125102639,
            "width" : 0.178336263
         },
         "probability" : 0.0147112124,
         "tagId" : "b6b73a81-a1f3-4eff-ba7f-4098dd16277b",
         "tagName" : "casco de seguridad"
      },
      {
         "boundingBox" : {
            "height" : 0.176830545,
            "left" : 0.5897397,
            "top" : 0.203356728,
            "width" : 0.197517037
         },
         "probability" : 0.0110203512,
         "tagId" : "b6b73a81-a1f3-4eff-ba7f-4098dd16277b",
         "tagName" : "casco de seguridad"
      },
      {
         "boundingBox" : {
            "height" : 0.145382464,
            "left" : 0.370109856,
            "top" : 0.384720445,
            "width" : 0.387858152
         },
         "probability" : 0.01255128,
         "tagId" : "b6b73a81-a1f3-4eff-ba7f-4098dd16277b",
         "tagName" : "casco de seguridad"
      },
      {
         "boundingBox" : {
            "height" : 0.220918134,
            "left" : 0.667895555,
            "top" : 0.181477,
            "width" : 0.172705889
         },
         "probability" : 0.011471,
         "tagId" : "0cff1b34-393f-4409-b0fc-730088013551",
         "tagName" : "Chaleco de seguridad"
      },
      {
         "boundingBox" : {
            "height" : 0.145382464,
            "left" : 0.370109856,
            "top" : 0.384720445,
            "width" : 0.387858152
         },
         "probability" : 0.279351383,
         "tagId" : "0cff1b34-393f-4409-b0fc-730088013551",
         "tagName" : "Chaleco de seguridad"
      },
      {
         "boundingBox" : {
            "height" : 0.2666216,
            "left" : 0.266245246,
            "top" : 0.419093,
            "width" : 0.140633225
         },
         "probability" : 0.07414317,
         "tagId" : "0cff1b34-393f-4409-b0fc-730088013551",
         "tagName" : "Chaleco de seguridad"
      },
      {
         "boundingBox" : {
            "height" : 0.187815845,
            "left" : 0.4294094,
            "top" : 0.672557056,
            "width" : 0.19949165
         },
         "probability" : 0.02092664,
         "tagId" : "0cff1b34-393f-4409-b0fc-730088013551",
         "tagName" : "Chaleco de seguridad"
      },
      {
         "boundingBox" : {
            "height" : 0.145382464,
            "left" : 0.370109856,
            "top" : 0.384720445,
            "width" : 0.387858152
         },
         "probability" : 0.151134133,
         "tagId" : "0390dd1d-9145-45d4-986b-b9c1c9a6f9ba",
         "tagName" : "Cubrebocas"
      },
      {
         "boundingBox" : {
            "height" : 0.187815845,
            "left" : 0.4294094,
            "top" : 0.672557056,
            "width" : 0.19949165
         },
         "probability" : 0.0105822254,
         "tagId" : "0390dd1d-9145-45d4-986b-b9c1c9a6f9ba",
         "tagName" : "Cubrebocas"
      }
   ],
   "project" : "47768834-6254-4135-89e4-0ddb5f30b25c"
}*/
