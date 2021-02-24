using OpenTK.Mathematics;

namespace Dust.NET.Core.Rendering
{
    public struct Vertex
    {
        public Vector3 Position { get; set; }
        public Vector2 TextureCoordinate { get; set; }
        public Vector3 Normal { get; set; }

        public Vertex(float x, float y, float z, float s, float t, float nx, float ny, float nz)
        {
            Position = new Vector3(x, y, z);
            TextureCoordinate = new Vector2(s, t);
            Normal = new Vector3(nx, ny, nz);
        }
    }
}