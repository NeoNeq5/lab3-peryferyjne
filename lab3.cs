using System;
using System.Drawing;
using System.Diagnostics;

class Program
{
        static void Main()
    {
        Console.Write("Podaj kod: ");
        string entrycode =Console.ReadLine();
        //string entrycode = "123627389281";

        bool isNumber = true;
        if(entrycode.Length == 12)
        {
            foreach(char c in entrycode)
            {
                if(c < '0' || c > '9')
                {
                    isNumber = false;
                }
            }
            if (isNumber)
            {
                CodeTheCode(entrycode);
            }
            else
            {
                Console.Write("nalezy wprowadzić tylko cyfry");
            }
            
        }
        else
        {
            Console.Write("niepoprawna długość ciągu");
        }
        
        
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
        WyswietlanieKodu(exitCode, fullcode);
    }


    static void WyswietlanieKodu(String kod, String entryCode){
        int widthMul = 2;
        int width = kod.Length * widthMul;
        int height = 100;
        int offset = 10;
        
        Font font = new Font("Arial", 16);

        Bitmap b = new Bitmap(width + 4*offset, height + 3*offset);
        Graphics img = Graphics.FromImage(b);

        img.Clear(Color.White);

        for(int x = 0; x < width; x++)
        {
                if (kod[x / widthMul] == '1')
                {   
                    if(x/widthMul < 3 || (x/widthMul >= 45 && x/widthMul < 50) || x/widthMul >= 92)
                        img.FillRectangle(Brushes.Black, 2*offset + x , offset, widthMul, height + 10);
                    else
                    img.FillRectangle(Brushes.Black, 2*offset + x , offset, widthMul, height);
                }
            
        }
        for (int i = 0; i < entryCode.Length; i++)
        {   
            if (i == 0)
                img.DrawString(entryCode[i].ToString(), font, Brushes.Black, new PointF(1, height + offset));
            else if (i >= 1 && i <= 6)
                img.DrawString(entryCode[i].ToString(), font, Brushes.Black, new PointF(offset +widthMul*4 + i * 6 * widthMul, height + offset));
            else
            img.DrawString(entryCode[i].ToString(), font, Brushes.Black, new PointF(offset + widthMul*14 + i * 6 * widthMul, height + offset));
        }
         b.Save("ean13.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
         Process.Start(new ProcessStartInfo("ean13.bmp") { UseShellExecute = true });
    }

}
