using System.Globalization;
using OpenTK.Mathematics;

namespace Dust.NET.Core.Parsers.Obj
{
    public class ObjNormalParser : IObjParser
    {
        public string Keyword { get; } = "vn";
        
        public void Parse(ObjModel model, string[] content)
        {
            Vector3 normal = new Vector3();
            for (int i = 0; i < content.Length; i++)
            {
                normal[i] = float.Parse(content[i], CultureInfo.InvariantCulture);
            }
            model.Normals.Add(normal);
        }
    }
}