using System;
using System.Collections.Generic;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core
{
    public class Shader : IDisposable
    {
        private readonly Dictionary<ShaderType, string> _shaders;
        private readonly List<int> _shaderHandles;
        private int _programHandle;

        public Shader()
        {
            _shaders = new Dictionary<ShaderType, string>();
            _shaderHandles = new List<int>();
            _programHandle = 0;
        }

        public string this[ShaderType type]
        {
            get => _shaders[type];
            set => _shaders[type] = value;
        }

        public Shader LoadSourceFiles()
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

        public Shader Compile()
        {
            foreach (int handle in _shaderHandles)
            {
                GL.CompileShader(handle);
                Console.Write(GL.GetShaderInfoLog(handle));
            }

            return this;
        }

        public Shader Link()
        {
            if (_programHandle != 0)
            {
                GL.DeleteProgram(_programHandle);
            }

            _programHandle = GL.CreateProgram();
            foreach (int handle in _shaderHandles)
            {
                GL.AttachShader(_programHandle, handle);
            }
            
            GL.LinkProgram(_programHandle);
            Console.Write(GL.GetProgramInfoLog(_programHandle));
            
            foreach (int handle in _shaderHandles)
            {
                GL.DetachShader(_programHandle, handle);
                GL.DeleteShader(handle);
            }

            _shaderHandles.Clear();
            return this;
        }

        public void Bind()
        {
            GL.UseProgram(_programHandle);
        }

        public void Unbind()
        {
            GL.UseProgram(0);
        }

        public void Dispose()
        {
            GL.DeleteProgram(_programHandle);
        }
    }
}