using System;
using System.Drawing;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string entrycode = "123627349283";


        WyswietlanieKodu(entrycode);
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
        if (kod.Length != 12) throw new Exception("zła ilosc cyfr!" + kod.Length);
        string borderCode = "101";
        string separateCode = "01010";

        String fullcode = entryCode + CountControlBit(entryCode);

        string exitCode = borderCode;
        string parity = Parity[fullcode[0] -'0'];

        for(int i = 0; i < 7; i++)
        {
            int 
        }

    }


    static void WyswietlanieKodu(String kod){
        int widthMul = 5;
        int width = kod.Length * widthMul;
        int height = 100;
        int offset = 5;
        

        Bitmap img = new Bitmap(width + 2*offset, height + 2*offset);

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
                    // Console.WriteLine(kod[x /100]);
                }
            }
        }
         img.Save("ean13.bmp");
         Process.Start(new ProcessStartInfo("ean13.bmp") { UseShellExecute = true });
    }

}
