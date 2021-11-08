using System.Linq;
using NUnit.Framework;
using SXL.Interpreter.Parser.Lexer;

namespace SXL_Tests
{
    [TestFixture]
    public class LexerTests
    {
        [Test]
        public void TestKeyword()
        {
            var lexer = new SxlLexer("protocol");
            
            Assert.True(lexer.Lex().First().Id == SxlTokenId.ProtocolKeyword);
        }

        [Test]
        public void TestInstruction()
        {
            var lexer = new SxlLexer("define a as import");
            
            Assert.True(lexer.Lex().First().Id == SxlTokenId.DefineInstruction);
        }

        [Test]
        public void TestString()
        {
            const string literal = "\"text\"";
            var lexer = new SxlLexer($"\"This {literal} you talk about, is it real?\"");
            
            Assert.True(lexer.Lex().First().Id == SxlTokenId.StringLiteral);
        }

        [Test]
        public void TestNumber()
        {
            var lexer = new SxlLexer("12.34 aaaaaaaaa 56.78").Lex();

            Assert.True(lexer.First().Id == SxlTokenId.NumericLiteral);
        }

        [Test]
        public void TestLexerError()
        {
            var lexer = new SxlLexer("|");
            lexer.Lex();

            Assert.True(lexer.Errors.Count() == 1);
        }
    }
}