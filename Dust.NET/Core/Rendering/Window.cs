using System;
using System.ComponentModel;
using Dust.NET.Core.Parsers.Obj;
using Dust.NET.Core.Rendering.Geometry;
using Dust.NET.Core.Rendering.Scene;
using Dust.NET.Core.Rendering.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Dust.NET.Core.Rendering
{
    public class Window : GameWindow
    {
        private ShaderProgram _shaderProgram;
        private Transform _transform;
        private Camera _camera;
        private float _counter;
        private Model _model;
        private bool[] _activeKeys;
        
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
            _activeKeys = new bool[(int)Keys.LastKey];
            
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

            _model = new(ObjLoader.Load("gt3.obj"), "diffuse.png");

            _transform = new()
            {
                Position = new Vector3(0.0f, 0.0f, -2f)
            };
            
            _camera = new(new Vector3(0.0f, 2.0f, 3.0f), Size.X / (float) Size.Y);

            GL.Enable(EnableCap.DepthTest);
            //GL.Enable(EnableCap.CullFace);
            //GL.CullFace(CullFaceMode.Front);
            CursorGrabbed = true;
            _counter = 0.0f;
            base.OnLoad();
        }
        
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            _transform.Rotation = new Vector3(0.0f, _counter, 0.0f);
            _shaderProgram.UploadMat4("mvp", _transform.GetMVP(_camera));
            _model.Draw();
            Context.SwapBuffers();
            _counter += 0.0005f;
            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            if (_activeKeys[(int)Keys.W])
            {
                _camera.Position += 0.001f * _camera.Front;
            }
            else if (_activeKeys[(int)Keys.S])
            {
                _camera.Position -= 0.001f * _camera.Front;
            }
            else if (_activeKeys[(int)Keys.A])
            {
                _camera.Position -= 0.001f * Vector3.Normalize(Vector3.Cross(_camera.Front, _camera.Up));
            }
            else if (_activeKeys[(int)Keys.D])
            {
                _camera.Position += 0.001f * Vector3.Normalize(Vector3.Cross(_camera.Front, _camera.Up));
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _model.Dispose();
            _shaderProgram.Dispose();
            base.OnClosing(e);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0, 0, e.Width, e.Height);
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            _camera.Yaw += e.DeltaX * 0.075f;
            _camera.Pitch -= e.DeltaY * 0.075f;
            base.OnMouseMove(e);
        }

        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            _activeKeys[(int)e.Key] = true;
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyboardKeyEventArgs e)
        {
            _activeKeys[(int)e.Key] = false;
        }
    }
}