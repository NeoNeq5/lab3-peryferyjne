using System;
using System.Drawing;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string entrycode = "123627349283";
        CodeTheCode(entrycode);
        
    }

        static readonly string[] A = {
        "0001101","0011001","0010011","0111101","0100011",
        "0110001","0101111","0111011","0110111","0001011"
        };

   
        static readonly string[] B = {
            "0100111","0110011","0011011","0100001","0011101",
            "0111001","0000101","0010001","0001001","0010111"
        };

        static readonly string[] C = {
            "1110010","1100110","1101100","1000010","1011100",
            "1001110","1010000","1000100","1001000","1110100"
        };

        static readonly string[] Parity = {
            "AAAAAA", "AABABB", "AABBAB", "AABBBA", "ABAABB",
            "ABBAAB", "ABBBAA", "ABABAB", "ABABBA", "ABBABA"
        };


    static string CountControlBit (string code)
    {
        int sum = 0;
        for (int i=0; i< code.Length; i++){

            int n= int.Parse(code[i].ToString());
            if ( (i+1)%2==0){
               sum+=n*3;
            } else {
               sum+=n;
            }
         }
        int controlN = 10-(sum % 10);
        string controlS = controlN.ToString();
        string fullCode= code + controlS;
        return fullCode;
    }


    static void CodeTheCode(string entryCode)
    {
        if (entryCode.Length != 12) throw new Exception("zła ilosc cyfr!" + entryCode.Length);
        string borderCode = "101";
        string separateCode = "01010";

        String fullcode = CountControlBit(entryCode);

        string exitCode = borderCode;
        string parity = Parity[fullcode[0] -'0'];

        for(int i = 1; i < 7; i++)
        {
            int num = fullcode[i] - '0';
            exitCode += (parity[i-1] == 'A') ? A[num] : B[num];
        }

        exitCode += separateCode;

        for(int i = 7; i < 13; i++)
        {
            int num = fullcode[i] - '0';
            exitCode += C[num];
        }
        exitCode += borderCode;
        WyswietlanieKodu(exitCode, entryCode);
    }


    static void WyswietlanieKodu(String kod, String entryCode){
        int widthMul = 2;
        int width = kod.Length * widthMul;
        int height = 100;
        int offset = 5;
        
        Font font = new Font("Arial", 16);

        Bitmap b = new Bitmap(width + 2*offset, height + 2*offset);
        Graphics img = Graphics.FromImage(b);

        for(int x = 0; x < width + 2*offset; x++)
        {
            for(int y = 0; y < height + 2*offset; y++){
                img.SetPixel(x, y, Color.White);
            }
        }

        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++){
                if( kod[x / widthMul] == '1')
                {
                    img.SetPixel(x + offset, y + offset, Color.Black);
                    
                }
                    img.DrawString(entryCode[x / (7 * widthMul)].ToString(), font, Brushes.Black, new PointF(x / (7 * widthMul) *14 , height - 20));
                
            }
        }
         img.Save("ean13.bmp");
         Process.Start(new ProcessStartInfo("ean13.bmp") { UseShellExecute = true });
    }

}
