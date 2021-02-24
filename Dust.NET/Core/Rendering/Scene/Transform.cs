using OpenTK.Mathematics;

namespace Dust.NET.Core.Rendering.Scene
{
    public class Transform
    {
        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                CalculateModelMatrix();
            }
        }
        public Vector3 Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                CalculateModelMatrix();
            }
        }
        public Vector3 Scale
        {
            get => _scale;
            set 
            {
                _scale = value;
                CalculateModelMatrix();
            }
        }

        private Matrix4 _modelMatrix;
        private Vector3 _position;
        private Vector3 _rotation;
        private Vector3 _scale;

        public Transform()
        {
            Position = Vector3.Zero;
            Rotation = Vector3.Zero;
            Scale = Vector3.One;
        }

        public Matrix4 GetMVP(Camera camera)
        {
            return _modelMatrix * camera.GetViewMatrix() * camera.GetProjectionMatrix();
        }

        private void CalculateModelMatrix()
        {
            Matrix4 position = Matrix4.CreateTranslation(Position);
            Matrix4 rotationX = Matrix4.CreateRotationX(Rotation.X); 
            Matrix4 rotationY = Matrix4.CreateRotationY(Rotation.Y); 
            Matrix4 rotationZ = Matrix4.CreateRotationZ(Rotation.Z);
            Matrix4 rotation = rotationX * rotationY * rotationZ;
            Matrix4 scale = Matrix4.CreateScale(Scale);

            _modelMatrix = scale * rotation * position;
        }
    }
}