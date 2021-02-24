using System.Globalization;
using OpenTK.Mathematics;

namespace Dust.NET.Core.Parsers.Obj
{
    public class ObjPositionParser : IObjParser
    {
        public string Keyword { get; } = "v";
        
        public void Parse(ObjModel model, string[] content)
        {
            Vector3 position = new Vector3();
            for (int i = 0; i < content.Length; i++)
            {
                position[i] = float.Parse(content[i], CultureInfo.InvariantCulture);
            }
            model.Positions.Add(position);
        }
    }
}