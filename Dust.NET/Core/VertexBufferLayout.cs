using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using OpenTK.Graphics.OpenGL4;

namespace Dust.NET.Core
{
    public class VertexBufferLayout : IEnumerable<IVertexBufferElement>
    {
        private readonly Dictionary<string, IVertexBufferElement> _elements;
        private int _nextOffset;
        
        public VertexBufferLayout()
        {
            _elements = new Dictionary<string, IVertexBufferElement>();
            _nextOffset = 0;
        }

        public IVertexBufferElement this[string name]
        {
            get => _elements[name];
            set
            {
                value.Name = name;
                value.Offset = _elements.Count == 0 ? _nextOffset : _elements.Last().Value.Offset + _nextOffset;
                _elements[name] = value;
                _nextOffset += value.Dimensions * value.Size;
            }
        }

        public IEnumerator<string, IVertexBufferElement> GetEnumerator() => _elements.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}