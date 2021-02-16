using System.Collections;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core
{
    public class VertexBufferLayout : IEnumerable<VertexBufferLayoutElement>
    {
        private readonly List<VertexBufferLayoutElement> _elements;
        private int _nextOffset;
        
        public VertexBufferLayout()
        {
            _elements = new List<VertexBufferLayoutElement>();
            _nextOffset = 0;
        }
        
        public VertexBufferLayout AddFloatElement(int dimensions, bool normalized = false)
        {
            AddElement(dimensions, VertexAttribPointerType.Float, sizeof(float), normalized);
            return this;
        }
        
        public VertexBufferLayout AddUintElement(int dimensions, bool normalized = false)
        {
            AddElement(dimensions, VertexAttribPointerType.UnsignedInt, sizeof(uint), normalized);
            return this;
        }
        
        private void AddElement(int dimensions, VertexAttribPointerType type, int size, bool normalized)
        {
            VertexBufferLayoutElement element = new()
            {
                Dimensions = dimensions,
                Type = type,
                Offset = _elements.Count == 0 ? _nextOffset : _elements.Last().Offset + _nextOffset,
                Normalized = normalized
            };

            _elements.Add(element);
            _nextOffset = dimensions * size;
        }

        public IEnumerator<VertexBufferLayoutElement> GetEnumerator() => _elements.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}