// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using backend.Models.Piece;
//
//    var piece = Piece.FromJson(jsonString);

namespace backend.Models.Piece
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class Piece
    {
        [J("pieceid")][JsonConverter(typeof(ParseStringConverter))]     public long Pieceid { get; set; }            
        [J("jobid")][JsonConverter(typeof(ParseStringConverter))] public long Jobid { get; set; }        
        [J("clienteleid")][JsonConverter(typeof(ParseStringConverter))]    public long Clienteleid { get; set; }           
        [J("Status")]                                                   public string Status { get; set; }           
        [J("Batch Name")]                                               public string BatchName { get; set; }        
        [J("Pages")][JsonConverter(typeof(ParseStringConverter))]       public long Pages { get; set; }              
        [J("Simplex")]                                                  public string Simplex { get; set; }          
        [J("sequence")][JsonConverter(typeof(ParseStringConverter))]    public long Sequence { get; set; }           
        [J("Retention Start Day")]                                      public string RetentionStartDay { get; set; }
    }

    public partial class Piece
    {
        public static Piece FromJson(string json) => JsonConvert.DeserializeObject<Piece>(json, backend.Models.Piece.Converter.Settings);
    
        public static List<Piece> FromJsonList(string json) => JsonConvert.DeserializeObject<List<Piece>>(json, backend.Models.Piece.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Piece self) => JsonConvert.SerializeObject(self, backend.Models.Piece.Converter.Settings);
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

    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
