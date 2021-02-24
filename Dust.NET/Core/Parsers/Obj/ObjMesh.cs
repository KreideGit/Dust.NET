using System.Collections.Generic;
using OpenTK.Mathematics;

namespace Dust.NET.Core.Parsers.Obj
{
    public class ObjMesh
    {
        public List<Vector3i> Indices { get; } = new List<Vector3i>();
    }
}