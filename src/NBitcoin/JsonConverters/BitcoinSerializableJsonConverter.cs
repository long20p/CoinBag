#if !NOJSONNET
using NBitcoin;
using NBitcoin.DataEncoders;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace NBitcoin.JsonConverters
{
#if !NOJSONNET
	public
#else
	internal
#endif
	class BitcoinSerializableJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IBitcoinSerializable).GetTypeInfo().IsAssignableFrom(objectType.GetTypeInfo());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null)
                return null;

            try
            {

                var obj = (IBitcoinSerializable)Activator.CreateInstance(objectType);
                var bytes = Encoders.Hex.DecodeData((string)reader.Value);
                obj.ReadWrite(bytes);
                return obj;
            }
            catch (EndOfStreamException)
            {
            }
            catch (FormatException)
            {
            }
            throw new JsonObjectException("Invalid bitcoin object of type " + objectType.Name, reader);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var bytes = ((IBitcoinSerializable)value).ToBytes();
            writer.WriteValue(Encoders.Hex.EncodeData(bytes));
        }
    }
}
#endif