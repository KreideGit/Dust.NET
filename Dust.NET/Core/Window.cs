using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;

namespace Dust.NET.Core
{
    public class Window : GameWindow
    {
        public Window() 
            : base(new GameWindowSettings(), 
                new NativeWindowSettings()
                {
                    Size = new Vector2i(1280, 720)
                })
        {
        }

        private struct Vec3
        { 
            float x, y, z;

            public Vec3(float x, float y, float z)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
        }
        
        protected override void OnLoad()
        {
            GL.ClearColor(0.12f, 0.12f, 0.12f, 1.0f);

            ShaderProgram s1 = new()
            {
                [ShaderType.VertexShader] = "vertex.glsl",
                [ShaderType.FragmentShader] = "fragment.glsl"
            };
            s1
                .LoadSourceFiles()
                .Compile()
                .Link()
                .Bind();

            VertexBufferLayout layout = new();
            layout
                .AddFloatElement(3);
            
            VertexBuffer<Vec3> posBuffer = new(new[]
            {
                new Vec3(-0.5f, -0.5f, 0.0f),
                new Vec3(0.5f, -0.5f, 0.0f),
                new Vec3(0.0f,  0.5f, 0.0f)
            }, layout);
            
            VertexBuffer<Vec3> colBuffer = new(new[]
            {
                new Vec3(0.0f,  0.0f, 0.0f),
                new Vec3(0.0f,  0.0f, 0.0f),
                new Vec3(0.0f,  0.0f, 1.0f)
            }, layout);

            VertexArray va = new();
            va
                .AddVertexBuffer(posBuffer)
                .AddVertexBuffer(colBuffer);
            
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
            Context.SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}