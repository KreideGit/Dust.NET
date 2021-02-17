using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core
{
    public class VertexBufferFloatElement : IVertexBufferElement
    {
        public string Name { get; }
        public int Dimensions { get; }
        public VertexAttribPointerType Type { get; }
        public int Offset { get; set; }
        public bool Normalized { get; }

        public VertexBufferFloatElement(string name, int dimensions, bool normalized = false)
        {
            Name = name;
            Dimensions = dimensions;
            Type = VertexAttribPointerType.Float;
            Offset = 0;
            Normalized = normalized;
        }
    }
}