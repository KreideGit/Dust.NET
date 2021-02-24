using System.Globalization;
using OpenTK.Mathematics;

namespace Dust.NET.Core.Parsers.Obj
{
    public class ObjTextureCoordinateParser: IObjParser
    {
        public string Keyword { get; } = "vt";
        
        public void Parse(ObjModel model, string[] content)
        {
            Vector2 textureCoordinate = new Vector2();
            for (int i = 0; i < content.Length; i++)
            {
                textureCoordinate[i] = float.Parse(content[i], CultureInfo.InvariantCulture);
            }
            model.TextureCoordinates.Add(textureCoordinate);
        }
    }
}