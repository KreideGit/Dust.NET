using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core.Rendering.Buffers
{
    public class IndexBuffer : IDisposable
    {
        public int IndexCount { get; }
        
        private readonly int _handle;
        
        public IndexBuffer(IEnumerable<uint> indices)
        {
            uint[] data = indices.ToArray();
            
            IndexCount = data.Length;
            _handle = GL.GenBuffer();
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
            GL.BufferData(BufferTarget.ArrayBuffer, IndexCount * sizeof(uint), data, BufferUsageHint.StaticDraw);
        }

        public void Bind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);
        public void Unbind() => GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
        public void Dispose() => GL.DeleteBuffer(_handle);
    }
}