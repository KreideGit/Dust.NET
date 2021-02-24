using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Dust.NET.Core.Rendering.Buffers.Layout;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core.Rendering.Buffers
{
    public class VertexBuffer<T> : IDisposable where T : struct
    {
        public VertexBufferLayout Layout { get; }
        public int VertexSize { get; }
        
        private readonly int _handle;
        
        public VertexBuffer(IEnumerable<T> vertices, VertexBufferLayout layout)
        {
            Layout = layout;
            VertexSize = Marshal.SizeOf(typeof(T));
            _handle = GL.GenBuffer();
            
            T[] data = vertices.ToArray();
            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * VertexSize, data, BufferUsageHint.StaticDraw);
        }

        public VertexBuffer(int numVertices, VertexBufferLayout layout)
        {
            Layout = layout;
            _handle = GL.GenBuffer();
            VertexSize = Marshal.SizeOf(typeof(T));
            
            Bind();
            GL.BufferData(BufferTarget.ArrayBuffer, numVertices * VertexSize, IntPtr.Zero, BufferUsageHint.DynamicDraw);
        }

        public void SetData(IEnumerable<T> vertices)
        {
            T[] data = vertices.ToArray();
            GL.BufferSubData(BufferTarget.ArrayBuffer, IntPtr.Zero, data.Length * VertexSize, data);
        }
        
        public void Bind() => GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
        public void Unbind() => GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        public void Dispose() => GL.DeleteBuffer(_handle);
    }
}