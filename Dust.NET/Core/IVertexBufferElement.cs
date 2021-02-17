using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core
{
    public interface IVertexBufferElement
    {
        string Name { get; set; }
        int Dimensions { get; }
        int Size { get; }
        VertexAttribPointerType Type { get; }
        int Offset { get; set; }
        bool Normalized { get; }
    }
}