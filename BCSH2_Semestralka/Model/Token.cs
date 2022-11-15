using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model
{
    public class Token
    {
        public TokenType Type { get; set; }
        public int Line { get; set; }
        public int LineToken { get; set; }
        public String? Value { get; set; }
        public enum TokenType
        {
            Double,
            String,
            Int,
            SemiColon,
            Colon,
            Exclamation,
            Comma,
            Equal,
            DoubleEqual,
            SmallerEqual,
            GreaterEqual,
            Smaller,
            Greater,
            Or,
            And,
            NotEqual,
            Plus,
            Minus,
            Multiplication,
            Division,
            LeftParenthesis,
            RightParenthesis,
            LeftBracket,
            RightBracket,
            If,
            Else,
            While,
            Var,
            Val,
            Return,
            Fun,
            DoubleLit,
            IntLit,
            StringLit,
            Ident,
            Quotation
        }

        public Token(TokenType type, int line, int lineToken)
        {
            Type = type;
            Line = line;
            LineToken = lineToken;
        }

        public Token(TokenType type, int line, int lineToken, string? value) : this(type, line, lineToken)
        {
            Value = value;
        }
    }
}
