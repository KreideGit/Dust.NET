using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dust.NET.Core.Parsers.Obj
{
    public static class ObjLoader
    {
        private static List<IObjParser> Parsers { get; } = new List<IObjParser>()
        {
            new ObjPositionParser(),
            new ObjTextureCoordinateParser(),
            new ObjNormalParser(),
            new ObjFaceParser(),
            new ObjMaterialParser()
        };
        
        public static ObjModel Load(string filePath)
        {
            ObjModel result = new ObjModel();
            string[] content = File.ReadAllLines(filePath);
            
            foreach (string line in content)
            {
                string[] splits = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (splits.Length == 0)
                {
                    continue;
                }
                
                foreach (var parser in Parsers)
                {
                    if (parser.Keyword == splits[0])
                    {
                        parser.Parse(result, splits.Skip(1).ToArray());
                    }
                }
            }

            return result;
        }
    }
}