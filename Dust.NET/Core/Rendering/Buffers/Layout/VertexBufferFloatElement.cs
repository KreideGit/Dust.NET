using Dust.NET.Core.Buffers.Layout;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core.Rendering.Buffers.Layout
{
    public struct VertexBufferFloatElement : IVertexBufferElement
    {
        public int Dimensions { get; }
        public int Size { get; }
        public VertexAttribPointerType Type { get; }
        public int Offset { get; set; }
        public bool Normalized { get; }

        public VertexBufferFloatElement(int dimensions, bool normalized = false)
        {
            Dimensions = dimensions;
            Size = sizeof(float);
            Type = VertexAttribPointerType.Float;
            Offset = 0;
            Normalized = normalized;
        }
    }
}