using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core
{
    public class ShaderProgram : IDisposable
    {
        private readonly Dictionary<ShaderType, string> _shaders;
        private readonly List<int> _shaderHandles;
        private int _handle;

        public ShaderProgram()
        {
            _shaders = new Dictionary<ShaderType, string>();
            _shaderHandles = new List<int>();
            _handle = 0;
        }

        public string this[ShaderType type]
        {
            get => _shaders[type];
            init => _shaders[type] = value;
        }

        public ShaderProgram LoadSourceFiles()
        {
            foreach (KeyValuePair<ShaderType, string> shader in _shaders)
            {
                int handle = GL.CreateShader(shader.Key);
                string content = File.ReadAllText(shader.Value);
                
                GL.ShaderSource(handle, content);
                _shaderHandles.Add(handle);
            }

            return this;
        }

        public ShaderProgram Compile()
        {
            foreach (int handle in _shaderHandles)
            {
                GL.CompileShader(handle);
                Console.Write(GL.GetShaderInfoLog(handle));
            }

            return this;
        }

        public ShaderProgram Link()
        {
            _handle = GL.CreateProgram();
            foreach (int handle in _shaderHandles)
            {
                GL.AttachShader(_handle, handle);
            }
            
            GL.LinkProgram(_handle);
            Console.Write(GL.GetProgramInfoLog(_handle));
            
            foreach (int handle in _shaderHandles)
            {
                GL.DetachShader(_handle, handle);
                GL.DeleteShader(handle);
            }

            _shaderHandles.Clear();
            return this;
        }

        public void Bind() => GL.UseProgram(_handle);
        public void Unbind() => GL.UseProgram(0);
        public void Dispose() => GL.DeleteProgram(_handle);
    }
}