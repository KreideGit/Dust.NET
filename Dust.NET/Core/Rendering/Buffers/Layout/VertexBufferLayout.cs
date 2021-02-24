using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Dust.NET.Core.Buffers.Layout;

namespace Dust.NET.Core.Rendering.Buffers.Layout
{
    public class VertexBufferLayout : IEnumerable<KeyValuePair<string, IVertexBufferElement>>
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
            init => AddElement(name, value);
        }

        private void AddElement(string name, IVertexBufferElement element)
        {
            element.Offset = _elements.Count == 0 ? _nextOffset : _elements.Last().Value.Offset + _nextOffset;
            _nextOffset += element.Dimensions * element.Size;
            _elements[name] = element;
        }

        public IEnumerator<KeyValuePair<string, IVertexBufferElement>> GetEnumerator() => _elements.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}