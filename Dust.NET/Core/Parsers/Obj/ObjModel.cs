using System.Collections.Generic;
using System.Linq;
using OpenTK.Mathematics;

namespace Dust.NET.Core.Parsers.Obj
{
    public class ObjModel
    {
        public List<Vector3> Positions { get; } = new List<Vector3>();
        public List<Vector2> TextureCoordinates { get; } = new List<Vector2>();
        public List<Vector3> Normals { get; } = new List<Vector3>();
        public List<ObjMesh> Meshes { get; } = new List<ObjMesh>();
        public ObjMesh CurrentMesh => Meshes.Last();
    }
}