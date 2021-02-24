using OpenTK.Mathematics;

namespace Dust.NET.Core.Extensions
{
    public static class VectorExtensions
    {
        public static float[] ToFloatArray(this Vector3 vector)
        {
            return new[] { vector[0], vector[1], vector[2] };
        }

        public static float[] ToFloatArray(this Vector2 vector)
        {
            return new[] { vector[0], vector[1] };
        }
    }
}