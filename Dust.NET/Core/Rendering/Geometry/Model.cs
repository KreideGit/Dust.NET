using System;
using System.Collections.Generic;
using Dust.NET.Core.Parsers.Obj;
using Dust.NET.Core.Rendering.Buffers;
using Dust.NET.Core.Rendering.Buffers.Layout;
using OpenTK.Mathematics;

namespace Dust.NET.Core.Rendering.Geometry
{
    public class Model : IDisposable
    {
        private readonly List<Mesh> _meshes;
        private readonly Texture _texture;
        private readonly VertexBuffer<Vertex> _vbo;
        
        public Model(ObjModel model, string texturePath)
        {
            _meshes = new();
            _texture = new(texturePath);
            
            List<Vertex> vertices = new();
            List<uint> meshIndices = new();
            List<uint[]> totalIndices = new();
            
            long baseN = Math.Max(model.Positions.Count, Math.Max(model.TextureCoordinates.Count, model.Normals.Count));
            Dictionary<long, uint> vertexLookup = new();
            uint indexCounter = 0;

            foreach (ObjMesh mesh in model.Meshes)
            {
                meshIndices.Clear();
                foreach (Vector3i vertex in mesh.Indices)
                {
                    int posIndex = vertex[0] - 1;
                    int texIndex = vertex[1] - 1;
                    int norIndex = vertex[2] - 1;

                    long encodedVertex = posIndex * baseN * baseN + texIndex * baseN + norIndex;
                    if (vertexLookup.TryGetValue(encodedVertex, out uint index))
                    {
                        meshIndices.Add(index);
                    }
                    else
                    {
                        vertices.Add(new Vertex(
                            model.Positions[posIndex][0],
                            model.Positions[posIndex][1],
                            model.Positions[posIndex][2],
                            model.TextureCoordinates[texIndex][0],
                            model.TextureCoordinates[texIndex][1],
                            model.Normals[norIndex][0],
                            model.Normals[norIndex][1],
                            model.Normals[norIndex][2]
                        ));

                        vertexLookup.Add(encodedVertex, indexCounter);
                        meshIndices.Add(indexCounter);
                        indexCounter++;
                    }
                }
                totalIndices.Add(meshIndices.ToArray());
            }

            _vbo = new(vertices, new()
            {
                ["position"] = new VertexBufferFloatElement(3),
                ["textureCoordinate"] = new VertexBufferFloatElement(2),
                ["normal"] = new VertexBufferFloatElement(3)
            });

            foreach (uint[] indices in totalIndices)
            {
                _meshes.Add(new Mesh(_vbo, new IndexBuffer(indices)));
            }
        }

        public void Draw()
        {
            _texture.Bind();
            foreach (Mesh mesh in _meshes)
            {
                mesh.Draw();
            }
        }

        public void Dispose()
        {
            _vbo.Dispose();
            foreach (Mesh mesh in _meshes)
            {
                mesh.Dispose();
            }
            _texture.Dispose();
        }
    }
}