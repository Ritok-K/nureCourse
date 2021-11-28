using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WeaponAlmanac.Data_Model.Serializers
{
    public class BitmapJsonConverter : JsonConverter<Bitmap>
    {
        public override Bitmap Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var res = default(Bitmap);

            var bytes = reader.GetBytesFromBase64();
            using(var stream = new MemoryStream(bytes))
            {
                res = new Bitmap(stream);
            }

            return res;
        }

        public override void Write(Utf8JsonWriter writer, Bitmap bitmap, JsonSerializerOptions options)
        {
            using(var stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                var bytes = stream.GetBuffer();

                writer.WriteBase64StringValue(new ReadOnlySpan<byte>(bytes));
            }
        }
    }
}
