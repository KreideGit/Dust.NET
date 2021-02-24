using System.Collections.Generic;
using OpenTK.Mathematics;

namespace Dust.NET.Core.Parsers.Obj
{
    public class ObjFaceParser: IObjParser
    {
        public string Keyword { get; } = "f";
        
        public void Parse(ObjModel model, string[] content)
        {
            List<Vector3i> polygon = new List<Vector3i>();
            for (int i = 0; i < content.Length; i++)
            {
                string[] vertex = content[i].Split('/');
                Vector3i indices = new Vector3i();
                
                for (int j = 0; j < vertex.Length; j++)
                {
                    indices[j] = vertex[j] == string.Empty ? 0 : int.Parse(vertex[j]);
                }
                polygon.Add(indices);
            }
            model.CurrentMesh.Indices.AddRange(polygon);
        }
    }
}