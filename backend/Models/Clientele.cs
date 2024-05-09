// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using backend.Models.Clientele;
//
//    var clientele = Clientele.FromJson(jsonString);

namespace backend.Models.Clientele
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using J = Newtonsoft.Json.JsonPropertyAttribute;
    using R = Newtonsoft.Json.Required;
    using N = Newtonsoft.Json.NullValueHandling;

    public partial class Clientele
    {
        [J("clientid")][JsonConverter(typeof(ParseStringConverter))]         public long Clientid { get; set; }       
        [J("name")]                                                          public string Name { get; set; }         
        [J("Retention Length")][JsonConverter(typeof(ParseStringConverter))] public long RetentionLength { get; set; }
        [J("sladuedate")][JsonConverter(typeof(ParseStringConverter))]       public long Sladuedate { get; set; }
        #nullable enable     
        [J("parentid")]                                                      public long? Parentid { get; set; }     
        [J("childids")]                                                      public List<long>? Childids { get; set; } 
    }

    public partial class Clientele
    {
        public static List<Clientele> FromJson(string json) => JsonConvert.DeserializeObject<List<Clientele>>(json, backend.Models.Clientele.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Clientele> self) => JsonConvert.SerializeObject(self, backend.Models.Clientele.Converter.Settings);
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
