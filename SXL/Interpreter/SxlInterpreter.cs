using System.IO;
using System.Threading.Tasks;
using SXL.Interpreter.Parser;
using SXL.Sandboxing;

namespace SXL.Interpreter
{
    public class SxlInterpreter
    {
        private readonly SxlParser parser;
        
        public SxlInterpreter(SxlParser parser)
        {
            this.parser = parser;
        }

        public SxlInterpreter(string text)
        {
            parser = new SxlParser(text);
        }

        public SxlInterpreter(Stream stream)
        {
            parser = new SxlParser(stream);
        }

        private static async Task<string> ReadStreamAsync(Stream stream)
        {
            return await new StreamReader(stream).ReadToEndAsync();
        }

        public void Start()
        {
            
        }

        public void Stop()
        {
            
        }
    }
}