namespace Dust.NET.Core.Parsers.Obj
{
    public class ObjMaterialParser : IObjParser
    {
        public string Keyword { get; } = "usemtl";

        public void Parse(ObjModel model, string[] content)
        {
            model.Meshes.Add(new ObjMesh());
        }
    }
}