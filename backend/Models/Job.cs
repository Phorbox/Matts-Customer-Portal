// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using backend.Models.Job;
//
//    var job = Job.FromJson(jsonString);

namespace backend.Models.Job
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class Job
    {
        #nullable enable     
        [J("Jobid")]            public long? Jobid { get; set; }                
        [J("Projectid")]        public long? Projectid { get; set; }            
        [J("Clienteleid")]      public long? Clienteleid { get; set; }          
        [J("Inputid")]          public long? Inputid { get; set; }              
        [J("Status")]           public string? Status { get; set; }             
        [J("DateApproved")]     public DateTime? DateApproved { get; set; }  
        [J("DueDate")]          public DateTime? DueDate { get; set; }   
        [J("DateCreated")]      public DateTime? DateCreated { get; set; }
        [J("ProjectName")]      public string? ProjectName { get; set; }        
        [J("SlaOveride")]       public long? SlaOveride { get; set; }         
        [J("Approval")]         public string? Approval { get; set; }           
        [J("ClienteleName")]    public string? ClienteleName { get; set; }      
        [J("RetentionLength")]  public long? RetentionLength { get; set; }      
        [J("SlaDueDate")]       public long? SlaDueDate { get; set; }           
        [J("ParentId")]         public long? ParentId { get; set; }    
        [J("Filename")]         public string? Filename { get; set; }           
        [J("StoragePriority")] public string? StoragePriority { get; set; }    
        [J("InputPDF")]         public string? InputPdf { get; set; }           
        #nullable disable       
    }

    public partial class Job
    {
        public static Job FromJson(string json) => JsonConvert.DeserializeObject<Job>(json, backend.Models.Job.Converter.Settings);
    
        public static List<Job> FromJsonList(string json) => JsonConvert.DeserializeObject<List<Job>>(json, backend.Models.Job.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Job self) => JsonConvert.SerializeObject(self, backend.Models.Job.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
