using System;

class Program
{
    static void Main()
    {

    }
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


    static void WyswietlanieKodu(String kod){
        
    }


}
