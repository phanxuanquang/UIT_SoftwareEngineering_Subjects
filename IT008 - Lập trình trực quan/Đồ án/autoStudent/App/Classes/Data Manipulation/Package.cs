
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App
{

    public partial class Root
    {
        [JsonProperty("updatedate")]
        public DateTime UpdateDate { get; set; }
        [JsonProperty("packages")]
        public List<Package> Packages { get; set; }
    }

    public partial class Package
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("installer")]
        public Installer Installer { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("displayname")]
        public string Displayname { get; set; }

        [JsonProperty("role")]
        public Role? Role { get; set; }

        [JsonProperty("uninstall argument", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> UninstallArgument { get; set; }

        public string UninstallString { get; set; }

        public Package() { }

        public Package(Package package)
        {
            this.Name = package.Name;
            this.Installer = package.Installer;
            this.Version = package.Version;
            this.Displayname = package.Displayname;
            this.Role = package.Role;
            this.UninstallArgument = package.UninstallArgument;
            this.UninstallString = package.UninstallString;
        }
    }

    public partial class Installer
    {
        [JsonProperty("kind")]
        public Kind Kind { get; set; }

        [JsonProperty("options", NullValueHandling = NullValueHandling.Ignore)]
        public Options Options { get; set; }

        [JsonProperty("x86", NullValueHandling = NullValueHandling.Ignore)]
        public string X86 { get; set; }

        [JsonProperty("x86_64", NullValueHandling = NullValueHandling.Ignore)]
        public string X8664 { get; set; }
    }

    public partial class Options
    {
        [JsonProperty("extension", NullValueHandling = NullValueHandling.Ignore)]
        public Extension? Extension { get; set; }

        [JsonProperty("destination", NullValueHandling = NullValueHandling.Ignore)]
        public string Destination { get; set; }

        [JsonProperty("shims", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Shims { get; set; }

        [JsonProperty("x86", NullValueHandling = NullValueHandling.Ignore)]
        public X86 X86 { get; set; }

        [JsonProperty("x86_64", NullValueHandling = NullValueHandling.Ignore)]
        public X86 X8664 { get; set; }

        [JsonProperty("arguments", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Arguments { get; set; }

        [JsonProperty("container", NullValueHandling = NullValueHandling.Ignore)]
        public Container Container { get; set; }
    }

    public partial class Container
    {
        [JsonProperty("installer")]
        public string Installer { get; set; }

        [JsonProperty("kind")]
        public Kind Kind { get; set; }
    }

    public partial class X86
    {
        [JsonProperty("container", NullValueHandling = NullValueHandling.Ignore)]
        public Container Container { get; set; }

        [JsonProperty("arguments", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Arguments { get; set; }

        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore)]
        public string Filename { get; set; }
    }

    public enum Kind { 
        Advancedinstaller, 
        Appx, 
        AsIs, 
        Copy, 
        Custom, 
        Innosetup, 
        Msi, 
        Nsis, 
        Squirrel, 
        Zip 
    };

    public enum Extension { 
        Exe, 
        Msi 
    };

    public partial class Root
    {
        public static Root FromJson(string json) => JsonConvert.DeserializeObject<Root>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Root self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                KindConverter.Singleton,
                ExtensionConverter.Singleton,
                RoleConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    internal class KindConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Kind) || t == typeof(Kind?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "advancedinstaller":
                    return Kind.Advancedinstaller;
                case "appx":
                    return Kind.Appx;
                case "as-is":
                    return Kind.AsIs;
                case "copy":
                    return Kind.Copy;
                case "custom":
                    return Kind.Custom;
                case "innosetup":
                    return Kind.Innosetup;
                case "msi":
                    return Kind.Msi;
                case "nsis":
                    return Kind.Nsis;
                case "squirrel":
                    return Kind.Squirrel;
                case "zip":
                    return Kind.Zip;
            }
            throw new Exception("Cannot unmarshal type Kind");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Kind)untypedValue;
            switch (value)
            {
                case Kind.Advancedinstaller:
                    serializer.Serialize(writer, "advancedinstaller");
                    return;
                case Kind.Appx:
                    serializer.Serialize(writer, "appx");
                    return;
                case Kind.AsIs:
                    serializer.Serialize(writer, "as-is");
                    return;
                case Kind.Copy:
                    serializer.Serialize(writer, "copy");
                    return;
                case Kind.Custom:
                    serializer.Serialize(writer, "custom");
                    return;
                case Kind.Innosetup:
                    serializer.Serialize(writer, "innosetup");
                    return;
                case Kind.Msi:
                    serializer.Serialize(writer, "msi");
                    return;
                case Kind.Nsis:
                    serializer.Serialize(writer, "nsis");
                    return;
                case Kind.Squirrel:
                    serializer.Serialize(writer, "squirrel");
                    return;
                case Kind.Zip:
                    serializer.Serialize(writer, "zip");
                    return;
            }
            throw new Exception("Cannot marshal type Kind");
        }

        public static readonly KindConverter Singleton = new KindConverter();
    }

    internal class ExtensionConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Extension) || t == typeof(Extension?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case ".exe":
                    return Extension.Exe;
                case ".msi":
                    return Extension.Msi;
            }
            throw new Exception("Cannot unmarshal type Extension");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Extension)untypedValue;
            switch (value)
            {
                case Extension.Exe:
                    serializer.Serialize(writer, ".exe");
                    return;
                case Extension.Msi:
                    serializer.Serialize(writer, ".msi");
                    return;
            }
            throw new Exception("Cannot marshal type Extension");
        }

        public static readonly ExtensionConverter Singleton = new ExtensionConverter();
    }

    public enum Role { 
        Graphic, 
        It, 
        None, 
        Tech 
    };

    internal class RoleConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(Role) || t == typeof(Role?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            switch (value)
            {
                case "Graphic":
                    return Role.Graphic;
                case "IT":
                    return Role.It;
                case "None":
                    return Role.None;
                case "Tech":
                    return Role.Tech;
            }
            throw new Exception("Cannot unmarshal type Role");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (Role)untypedValue;
            switch (value)
            {
                case Role.Graphic:
                    serializer.Serialize(writer, "Graphic");
                    return;
                case Role.It:
                    serializer.Serialize(writer, "IT");
                    return;
                case Role.None:
                    serializer.Serialize(writer, "None");
                    return;
                case Role.Tech:
                    serializer.Serialize(writer, "Tech");
                    return;
            }
            throw new Exception("Cannot marshal type Role");
        }

        public static readonly RoleConverter Singleton = new RoleConverter();
    }
}
