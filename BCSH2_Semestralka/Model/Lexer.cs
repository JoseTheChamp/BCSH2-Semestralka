using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BCSH2_Semestralka.Model
{
    public class Lexer
    {
        private String vstup;
        private int index = 0;
        private int tokenIndex = 0;
        private bool konec = false;
        private List<Token> tokens = new List<Token>();
        private bool isReadingString = false;
        private int line = 1;
        private int lineToken = 1;

        private char Next()
        {
            return vstup[index];
        }
        private char Pop()
        {
            index++;
            return vstup[index - 1];
        }
        private bool hasNext()
        {
            if (index <= vstup.Length - 1) return true;
            return false;
        }

        private Token GetToken(Token.TokenType tokenType) {
            lineToken++;
            return new Token(tokenType,line,lineToken-1);
        }
        private Token GetToken(Token.TokenType tokenType,string value)
        {
            lineToken++;
            return new Token(tokenType,line,lineToken - 1,value);
        }

        private List<char> breakPoints = new List<char>(new char[] {'!', ',', ';', '=', ':', '<', '>', '+', '-', '*', '/', '(', ')', '%','{','}','&','|','"', '\n', '\r' });
        private List<string> keyWords = new List<string>(new string[] { "double", "int", "var", "val", "string", "if", "else", "while", "return" , "fun"});
        public List<Token> Lexicate(String vstup)
        {
            this.vstup = vstup;
            tokens = new List<Token>();

            while (!konec)
            {
                char v = Next();
                if (breakPoints.Contains(v))
                {
                    ReadPoint();
                }
                else if (v == ' ' || v == '\u0009')
                {
                    Pop();
                }
                else
                {
                    ReadText();
                }
                if (!hasNext())
                {
                    konec = true;
                }
            }

            //reseting
            index = 0;
            tokenIndex = 0;
            konec = false;
            isReadingString = false;
            line = 1;
            lineToken = 1;

            return tokens;
        }
        private void ReadPoint()
        {
            char v = Pop();
            char v2 = ' ';
            if (v == '<' || v == '>' || v == '&' || v == '|' || v == '=' || v == '!')
            {
                if (hasNext())
                {
                    v2 = Next();
                        //case ':': tokens.Add(GetToken(Token.TokenType.Colon)); break;
                        switch (v2)
                        {
                            case '&': Pop(); tokens.Add(GetToken(Token.TokenType.And)); break;
                            case '|': Pop(); tokens.Add(GetToken(Token.TokenType.Or)); break;
                            case '=':
                            if (v == '>')
                            {
                                Pop();
                                tokens.Add(GetToken(Token.TokenType.GreaterEqual));
                            } else if (v == '<') {
                                Pop();
                                tokens.Add(GetToken(Token.TokenType.SmallerEqual));
                            } else if (v == '=') {
                                Pop();
                                tokens.Add(GetToken(Token.TokenType.DoubleEqual));
                            } else if (v == '!') {
                                Pop();
                                tokens.Add(GetToken(Token.TokenType.NotEqual));
                            }
                            else {
                                throw new Exception("Inproper Symbol [ & , | ....]");
                            }
                                break;
                            default:
                            switch (v)
                            {
                                case '<': tokens.Add(GetToken(Token.TokenType.Smaller)); break;
                                case '>': tokens.Add(GetToken(Token.TokenType.Greater)); break;
                                case '=': tokens.Add(GetToken(Token.TokenType.Equal)); break;
                                case '!': tokens.Add(GetToken(Token.TokenType.Exclamation)); break;
                                default: throw new Exception("Inproper Symbol [ & , | ....]"); break;
                            }
                            break;
                        }
                }
                else
                {
                    konec = true;
                }
            }
            else
            {
                switch (v)
                {
                    case ',': tokens.Add(GetToken(Token.TokenType.Comma)); break;
                    case ';': tokens.Add(GetToken(Token.TokenType.SemiColon)); break;
                    case '+': tokens.Add(GetToken(Token.TokenType.Plus)); break;
                    case '-': tokens.Add(GetToken(Token.TokenType.Minus)); break;
                    case '*': tokens.Add(GetToken(Token.TokenType.Multiplication)); break;
                    case '/': tokens.Add(GetToken(Token.TokenType.Division)); break;
                    case '(': tokens.Add(GetToken(Token.TokenType.LeftParenthesis)); break;
                    case ')': tokens.Add(GetToken(Token.TokenType.RightParenthesis)); break;
                    case '{': tokens.Add(GetToken(Token.TokenType.LeftBracket)); break;
                    case '}': tokens.Add(GetToken(Token.TokenType.RightBracket)); break;
                    case ':': tokens.Add(GetToken(Token.TokenType.Colon)); break;
                    case '"':
                        isReadingString = !isReadingString;
                        tokens.Add(GetToken(Token.TokenType.Quotation));
                        ReadText();
                        break;
                    case '\n': line++; lineToken = 1; break;
                    case '\r': break;
                }
            }
        }

        private void ReadText()
        {
            if (isReadingString)
            {
                string s = "";
                while (Next() != '"')
                {
                    s = s + Pop().ToString();
                    if (!hasNext())
                    {
                        break;
                    }
                }
                tokens.Add(GetToken(Token.TokenType.StringLit, s));
                Pop();
                tokens.Add(GetToken(Token.TokenType.Quotation));
                isReadingString =false;
            }
            else
            {
                char v = ' ';
                string s = "";
                string sLower = "";
                while (!breakPoints.Contains(Next()) && Next() != ' ' && Next() != '\u0009')
                {
                    /*char[] pole = new char[1];
                    pole[0] = Next();
                    byte[] asciiBytes = Encoding.ASCII.GetBytes(pole);
                    Console.WriteLine("Znak: " + Next());
                    Console.WriteLine("ZnakAS: " + asciiBytes[0]);*/
                    s = s + Pop().ToString();
                    if (!hasNext())
                    {
                        break;
                    }
                }
                s = s.Trim();
                sLower = s.ToLower();
                if (keyWords.Contains(sLower))
                {
                    switch (sLower)
                    {
                        case "fun": tokens.Add(GetToken(Token.TokenType.Fun)); break;
                        case "var": tokens.Add(GetToken(Token.TokenType.Var)); break;
                        case "val": tokens.Add(GetToken(Token.TokenType.Val)); break;
                        case "if": tokens.Add(GetToken(Token.TokenType.If)); break;
                        case "else": tokens.Add(GetToken(Token.TokenType.Else)); break;
                        case "while": tokens.Add(GetToken(Token.TokenType.While)); break;
                        case "return": tokens.Add(GetToken(Token.TokenType.Return)); break;
                        case "int": tokens.Add(GetToken(Token.TokenType.Int)); break;
                        case "string": tokens.Add(GetToken(Token.TokenType.String)); break;
                        case "double": tokens.Add(GetToken(Token.TokenType.Double)); break;
                    }
                }
                else
                {
                    double numd = 0;
                    int num = 0;
                    try
                    {
                        double d = Double.Parse(s, CultureInfo.InvariantCulture);
                        if (s.Contains('.'))
                        {
                            tokens.Add(GetToken(Token.TokenType.DoubleLit, s));
                        }
                        else
                        {
                            tokens.Add(GetToken(Token.TokenType.IntLit, s));
                        }
                    }
                    catch (Exception ex)
                    {
                        if (Int32.TryParse(s, out num))
                        {
                            tokens.Add(GetToken(Token.TokenType.IntLit, s));
                        }
                        else if (s != "\n")
                        {
                            //Figureout if ident or string
                            string reg = @"^[a-zA-Z_][a-zA-Z0-9_]*$";
                            if (Regex.IsMatch(s, reg))
                            {
                                tokens.Add(GetToken(Token.TokenType.Ident, s));
                            }
                            else
                            {
                                throw new Exception("Truly unexpected.1");
                            }
                        }
                    }
                }  
            }
            if (!hasNext())
            {
                konec = true;
            }
        }
    }
}
