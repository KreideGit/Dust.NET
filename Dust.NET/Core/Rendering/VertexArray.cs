using System;
using System.Collections.Generic;
using Dust.NET.Core.Buffers.Layout;
using Dust.NET.Core.Rendering.Buffers;
using Dust.NET.Core.Rendering.Buffers.Layout;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core.Rendering
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
            foreach (KeyValuePair<string, IVertexBufferElement> element in layout)
            {
                GL.EnableVertexAttribArray(_attributeIndex);
                GL.VertexAttribPointer(
                        _attributeIndex, 
                        element.Value.Dimensions, 
                        element.Value.Type, 
                        element.Value.Normalized, 
                        vertexBuffer.VertexSize,
                        element.Value.Offset
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