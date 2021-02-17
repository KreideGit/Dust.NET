using System;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core
{
    public class VertexArray : IDisposable
    {
        public IndexBuffer IndexBuffer { get; private set; }
        
        private readonly int _handle;
        private int _attributeIndex;
        
        public VertexArray()
        {
            _handle = GL.GenVertexArray();
            _attributeIndex = 0;
        }

        public VertexArray AddVertexBuffer<T>(VertexBuffer<T> vertexBuffer) where T : struct
        {
            Bind();
            vertexBuffer.Bind();
            
            VertexBufferLayout layout = vertexBuffer.Layout;
            foreach (VertexBufferLayoutElement element in layout)
            {
                GL.EnableVertexAttribArray(_attributeIndex);
                GL.VertexAttribPointer(
                        _attributeIndex, 
                        element.Dimensions, 
                        element.Type, 
                        element.Normalized, 
                        vertexBuffer.VertexSize,
                        element.Offset
                    );
                _attributeIndex++;
            }

            return this;
        }

        public VertexArray SetIndexBuffer(IndexBuffer indexBuffer)
        {
            Bind();
            indexBuffer.Bind();
            IndexBuffer = indexBuffer;
            return this;
        }

        public void Bind() => GL.BindVertexArray(_handle);
        public void Unbind() => GL.BindVertexArray(0);
        public void Dispose() => GL.DeleteVertexArray(_handle);
    }
}