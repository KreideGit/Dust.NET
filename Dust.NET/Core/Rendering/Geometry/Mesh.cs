using System;
using Dust.NET.Core.Rendering.Buffers;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core.Rendering.Geometry
{
    public class Mesh : IDisposable
    {
        private readonly VertexArray _vao;

        public Mesh(VertexBuffer<Vertex> vbo, IndexBuffer ibo)
        {
            _vao = new();
            _vao
                .AddVertexBuffer(vbo)
                .SetIndexBuffer(ibo);
        }

        public void Draw()
        {
            _vao.Bind();
            GL.DrawElements(PrimitiveType.Triangles, _vao.IndexBuffer.IndexCount, DrawElementsType.UnsignedInt, 0);
        }

        public void Dispose()
        {
            _vao.IndexBuffer.Dispose();
            _vao.Dispose();
        }
    }
}