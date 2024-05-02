// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using backend.Models;
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
    {    [J("pieceid")][JsonConverter(typeof(ParseStringConverter))]     public long Pieceid { get; set; }                       
        [J("workorderid")][JsonConverter(typeof(ParseStringConverter))] public long Workorderid { get; set; }                   
        [J("clientid")][JsonConverter(typeof(ParseStringConverter))]    public long Clientid { get; set; }                      
        [J("Status")]                                                   public Status Status { get; set; }                      
        [J("Batch Name")]                                               public string BatchName { get; set; }                   
        [J("Pages")][JsonConverter(typeof(ParseStringConverter))]       public long Pages { get; set; }                         
        [J("Simplex")]                                                  public Simplex Simplex { get; set; }                    
        [J("sequence")][JsonConverter(typeof(ParseStringConverter))]    public long Sequence { get; set; }                      
        [J("Retention Start Day")]                                      public string RetentionStartDay { get; set; }
    }

    public enum RetentionStartDay { The43024 };

    public enum Simplex { Empty, True };

    public enum Status { Approved, Rejected };

    public partial class Piece
    {
        public static List<Piece> FromJson(string json) => JsonConvert.DeserializeObject<List<Piece>>(json, backend.Models.Piece.Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this List<Piece> self) => JsonConvert.SerializeObject(self, backend.Models.Piece.Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                RetentionStartDayConverter.Singleton,
                SimplexConverter.Singleton,
                StatusConverter.Singleton,
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

    internal class RetentionStartDayConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(RetentionStartDay) || t == typeof(RetentionStartDay?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "4/30/24")
            {
                return RetentionStartDay.The43024;
            }
            throw new Exception("Cannot unmarshal type RetentionStartDay");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (RetentionStartDay)untypedValue;
            if (value == RetentionStartDay.The43024)
            {
                serializer.Serialize(writer, "4/30/24");
                return;
            }
            throw new Exception("Cannot marshal type RetentionStartDay");
        }

        public static readonly RetentionStartDayConverter Singleton = new RetentionStartDayConverter();
    }

    internal class SimplexConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Simplex) || t == typeof(Simplex?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "":
                    return Simplex.Empty;
                case "TRUE":
                    return Simplex.True;
            }
            throw new Exception("Cannot unmarshal type Simplex");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Simplex)untypedValue;
            switch (value)
            {
                case Simplex.Empty:
                    serializer.Serialize(writer, "");
                    return;
                case Simplex.True:
                    serializer.Serialize(writer, "TRUE");
                    return;
            }
            throw new Exception("Cannot marshal type Simplex");
        }

        public static readonly SimplexConverter Singleton = new SimplexConverter();
    }

    internal class StatusConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Status) || t == typeof(Status?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Approved":
                    return Status.Approved;
                case "Rejected":
                    return Status.Rejected;
            }
            throw new Exception("Cannot unmarshal type Status");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Status)untypedValue;
            switch (value)
            {
                case Status.Approved:
                    serializer.Serialize(writer, "Approved");
                    return;
                case Status.Rejected:
                    serializer.Serialize(writer, "Rejected");
                    return;
            }
            throw new Exception("Cannot marshal type Status");
        }

        public static readonly StatusConverter Singleton = new StatusConverter();
    }
}
