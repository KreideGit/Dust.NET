using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core
{
    public struct VertexBufferLayoutElement
    {
        public int Dimensions { get; init; }
        public VertexAttribPointerType Type { get; init; }
        public int Offset { get; init; }
        public bool Normalized { get; init; }
    }
}