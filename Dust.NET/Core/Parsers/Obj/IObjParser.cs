namespace Dust.NET.Core.Parsers.Obj
{
    public interface IObjParser
    {
        string Keyword { get; }
        
        void Parse(ObjModel model, string[] content);
    }
}