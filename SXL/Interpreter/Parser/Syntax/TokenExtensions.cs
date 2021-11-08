using SXL.Interpreter.Parser.Lexer;

namespace SXL.Interpreter.Parser.Syntax
{
    public static class TokenExtensions
    {
        public static bool IsIdentifier(this SxlToken token)
        {
            return !token.IsKeyword() && !token.IsBinary();
        }
        
        public static bool IsKeyword(this SxlToken token)
        {
            switch (token.Id)
            {
                case SxlTokenId.ProtocolKeyword:
                    return true;
                default:
                    return token.IsInstruction();
            }
        }
        
        public static bool IsInstruction(this SxlToken token)
        {
            switch (token.Id)
            {
                case SxlTokenId.DefineInstruction:
                case SxlTokenId.AsInstruction:
                case SxlTokenId.ImportInstruction:
                case SxlTokenId.FromInstruction:
                    return true;
                default:
                    return false;
            }
        }
        
        public static bool IsBinary(this SxlToken token)
        {
            switch (token.Id)
            {
                case SxlTokenId.Assignment:
                    return true;
                default:
                    return token.IsConditional() || token.IsArithmetic();
            }
        }

        public static bool IsArithmetic(this SxlToken token)
        {
            switch (token.Id)
            {
                case SxlTokenId.Plus:
                case SxlTokenId.Minus:
                case SxlTokenId.Multiply:
                case SxlTokenId.Divide:
                    return true;
                default:
                    return false;
            }
        }
        
        public static bool IsConditional(this SxlToken token)
        {
            switch (token.Id)
            {
                case SxlTokenId.And:
                case SxlTokenId.Or:
                case SxlTokenId.LessThan:
                case SxlTokenId.LessThanOrEqual: 
                case SxlTokenId.GreaterThan: 
                case SxlTokenId.GreaterThanOrEqual: 
                case SxlTokenId.Not: 
                case SxlTokenId.Equals: 
                case SxlTokenId.Assignment: 
                case SxlTokenId.NotEqual:
                    return true;
                default:
                    return false;
            }
        }
    }
}