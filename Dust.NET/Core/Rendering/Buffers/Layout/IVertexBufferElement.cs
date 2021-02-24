using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core.Buffers.Layout
{
    public interface IVertexBufferElement
    {
        int Dimensions { get; }
        int Size { get; }
        VertexAttribPointerType Type { get; }
        int Offset { get; set; }
        bool Normalized { get; }
    }
}