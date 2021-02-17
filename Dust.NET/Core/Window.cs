using System.ComponentModel;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Dust.NET.Core
{
    public class Window : GameWindow
    {
        private VertexArray _vao;
        private VertexBuffer<Vector3> _posVbo;
        private VertexBuffer<Vector2> _texVbo;
        private VertexBufferLayout _posLayout;
        private VertexBufferLayout _texLayout;
        private IndexBuffer _ibo;
        private ShaderProgram _shaderProgram;
        private Texture _texture;
        
        public Window() 
            : base(new GameWindowSettings(), 
                new NativeWindowSettings()
                {
                    Size = new Vector2i(1280, 720)
                })
        {
        }

        protected override void OnLoad()
        {
            GL.ClearColor(0.12f, 0.12f, 0.12f, 1.0f);
                
            _shaderProgram = new()
            {
                [ShaderType.VertexShader] = "vertex.glsl",
                [ShaderType.FragmentShader] = "fragment.glsl"
            };
            _shaderProgram
                .LoadSourceFiles()
                .Compile()
                .Link()
                .Bind();

            _posLayout = new();
            _posLayout
                .AddFloatElement(3);

            _texLayout = new();
            _texLayout
                .AddFloatElement(2);
            
            _posVbo = new(new[]
            {
                new Vector3(-0.5f, -0.5f, 0.0f),
                new Vector3(0.5f, -0.5f, 0.0f),
                new Vector3(0.0f,  0.5f, 0.0f)
            }, _posLayout);
            
            _texVbo = new(new[]
            {
                new Vector2(0.0f,  0.0f),
                new Vector2(1.0f,  0.0f),
                new Vector2(0.5f,  1.0f)
            }, _texLayout);

            _ibo = new(new uint[]
            {
                0, 1, 2
            });

            _vao = new();
            _vao
                .AddVertexBuffer(_posVbo)
                .AddVertexBuffer(_texVbo)
                .SetIndexBuffer(_ibo);

            _texture = new("bricks.jpg");
            _texture.Bind();
            
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.DrawElements(PrimitiveType.Triangles, _vao.IndexBuffer.IndexCount, DrawElementsType.UnsignedInt, 0);
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _posVbo.Dispose();
            _texVbo.Dispose();
            _ibo.Dispose();
            _vao.Dispose();
            _shaderProgram.Dispose();
            _texture.Dispose();
            base.OnClosing(e);
        }
    }
}