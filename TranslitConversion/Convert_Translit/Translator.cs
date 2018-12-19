using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Convert_Translit
{
    public static class Translator
    {
        private static Dictionary<char, string> LatToArm = new Dictionary<char, string>();
        private static string previous = "";
        static Translator()
        {
            LatToArm.Add('a', "ա"); LatToArm.Add('A', "Ա");
            LatToArm.Add('b', "բ"); LatToArm.Add('B', "Բ");
            LatToArm.Add('d', "դ"); LatToArm.Add('D', "Դ");
            LatToArm.Add('c', "ց"); LatToArm.Add('C', "Ց");
            LatToArm.Add('e', "ե"); LatToArm.Add('E', "Ե");
            LatToArm.Add('f', "ֆ"); LatToArm.Add('F', "Ֆ");
            LatToArm.Add('g', "գ"); LatToArm.Add('G', "Գ");
            LatToArm.Add('h', "հ"); LatToArm.Add('H', "Հ");
            LatToArm.Add('i', "ի"); LatToArm.Add('I', "Ի");
            LatToArm.Add('j', "ջ"); LatToArm.Add('J', "Ջ");
            LatToArm.Add('k', "կ"); LatToArm.Add('K', "Կ");
            LatToArm.Add('l', "լ"); LatToArm.Add('L', "Լ");
            LatToArm.Add('m', "մ"); LatToArm.Add('M', "Մ");
            LatToArm.Add('n', "ն"); LatToArm.Add('N', "Ն");
            LatToArm.Add('o', "ո"); LatToArm.Add('O', "Ո");
            LatToArm.Add('p', "պ"); LatToArm.Add('P', "Պ");
            LatToArm.Add('q', "ք"); LatToArm.Add('Q', "Ք");
            LatToArm.Add('r', "ր"); LatToArm.Add('R', "Ր");
            LatToArm.Add('s', "ս"); LatToArm.Add('S', "Ս");
            LatToArm.Add('t', "տ"); LatToArm.Add('T', "Տ");
            LatToArm.Add('u', "ու"); LatToArm.Add('U', "Ու");
            LatToArm.Add('v', "վ"); LatToArm.Add('V', "Վ");
            LatToArm.Add('w', "վ"); LatToArm.Add('W', "Վ");
            LatToArm.Add('x', "խ"); LatToArm.Add('X', "Խ");
            LatToArm.Add('y', "յ"); LatToArm.Add('Y', "Յ");
            LatToArm.Add('z', "զ"); LatToArm.Add('Z', "Զ");
            LatToArm.Add('@', "ը"); LatToArm.Add('.', ":");
        }


        /// <summary>
        /// Returning a converted text from Latin to Arm characters
        /// </summary>
        /// <param name="input"></param>
        /// <param name="oldText"></param>
        /// <returns></returns>
        public static string ConvertText(this string input, string oldText)
        {
            int len = input.Length;
            string output = (input.Substring(0,len - 1) == previous)?
                oldText + ConvertFullText(input[len-1].ToString()) :
                ConvertFullText(input);
            previous = input;
            return output;
        }


        // Converting the text cahr by char
        private static string ConvertFullText(this string input)
        {
            string output = "";
            foreach (char tar in input)
            {
                try
                {
                    output = $"{output}{LatToArm[tar]}";
                }
                catch
                {
                    output = $"{output}{tar}";
                }
            }
            return output;
        }
    }
}
