using System;
using static System.Console;

class MainClass {
  public static void Main (string[] args) 
  {
      string wiadomoscPowitalna = "\nWitaj w grze Snake! Naciśnij enter aby rozpocząć.";
    WriteLine(wiadomoscPowitalna);
    ReadLine();
    Clear();
  
    
    char[][] mapa = new char[50][];
    for (int wymiar = 0; wymiar < 50; wymiar++)
    {
      mapa[wymiar] = new char[50];
    }

    for (int kolumna = 0; kolumna < 50; kolumna++)
    {
      for (int rzad = 0; rzad < 50; rzad++)
      {
        if (kolumna == 0 || kolumna == 49 || rzad == 0 || rzad == 49)
        {
          mapa[kolumna][rzad] = '#';
        }      
        else {
          mapa[kolumna][rzad] = ' ';

          
        } 
        Write(mapa[kolumna][rzad]);
      } 
      Write("\n");
    }
    
    Random punkt = new Random();

      for (int ctr = 1)
    {
   Console.Write("{0,6}", rnd.Next(-100, 101));
   if (ctr % 5 == 0) Console.WriteLine();
    }

    }
  }
}

  
  
  
