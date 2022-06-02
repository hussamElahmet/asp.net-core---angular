using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Olle.Models.Helpers
{

    public class DecoderMorse
    {
        public string FromMorse(string code)
        {
            Char DELIMETER = ' ';
            Dictionary<string, string> Secret = new Dictionary<string, string> {
                { ".-", "a" },
                { "-...", "b" },
                { "-.-.", "c" },
                { "-..", "d" },
                { ".", "e" },
                { "..-.", "f" },
                { "--.", "g" },
                { "....", "h" },
                { "..", "i" },
                { ".---", "j" },
                { "-.-", "k" },
                { ".-..", "l" },
                { "--", "m" },
                { "-.", "n" },
                { "---", "o" },
                { ".--.", "p" },
                { "--.-", "q" },
                { ".-.", "r" },
                { "...", "s" },
                { "-", "t" },
                { "..-", "u" },
                { "...-", "v" },
                { ".--", "w" },
                { "-..-", "x" },
                { "-.--", "y" },
                { "--..", "z" },
                {"-----","0"},
                {".----","1"},
                {"..---","2"},
                {"...--","3"},
                {"....-","4"},
                {".....","5"},
                {"-....","6"},
                {"--...","7"},
                {"---..","8"},
                {"----.","9"},   
                {"/"," "},
                {"-....-", "-" }
            };
            var res = code.Split(DELIMETER).Select(c=>Secret[c]).Aggregate(new StringBuilder(), (b, c)=> b.Append(c)).ToString();
            return res;
        }
    }
}
