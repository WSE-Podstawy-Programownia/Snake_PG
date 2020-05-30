using System;
using System.Threading;
using static System.Console;

class MainClass {
  static void WriteWelcomeMessage() 
  {
    string wiadomoscPowitalna = "\nWitaj w grze Snake! Naciśnij enter aby rozpocząć.";
    WriteLine(wiadomoscPowitalna);
    ReadLine();
    Clear();
  }
  
  static int gameAreaXSize = 41;
  static int gameAreaYSize = 21; 
  static char [,] gameArea = new char[gameAreaXSize, gameAreaYSize];
  static Point point = new Point();
  static Snek snake = new Snek ();

  static void PopulateGameArea () {
    for (int y = 0; y < gameAreaYSize; y++) {
        for (int x = 0; x < gameAreaXSize; x++) {
          if(y == 0 || y == gameAreaYSize-1 || x == 0 || x == gameAreaXSize-1) {
            gameArea[x, y] = '#'; 
          }          
          else {
            gameArea[x, y] = ' '; // dokłądny zapis wymiarów mapy (żeby była ramka, a nie jakiś kwadrat głupi)
          }
        }
    }
  }

  static void DrawGameArea () {
    Console.Clear();
    for (int y = 0; y < gameAreaYSize; y++) {
        for (int x = 0; x < gameAreaXSize; x++) {
          if (x == point.PointX && y == point.PointY) {
            Console.Write('@');
          }
          
          else {
            Console.Write(gameArea[x, y]); // wypisanie kolejnego znaku w danej linijce
          }
        }
        Console.WriteLine(""); // zeby zakonczyc linijke
    }
  }

  public static void Main (string[] args) {
    WriteWelcomeMessage(); // wypisanie wiadomosci powitalnej
    PopulateGameArea(); // zapelnienie tablicy danymi, nie w while bo wystarczy, że wygeneruje się raz
    
    while (true){      // powtarzanie caly czas
      point.RandomizePosition();
      DrawGameArea(); // wypisanie obszaru gry
      Thread.Sleep(2000); // odczekanie 500ms
    }
  }
    }

class Point {

 public int PointX;
 public int PointY;

    public void RandomizePosition () {
      
      Random RandomizePoint = new Random();
      PointX = RandomizePoint.Next(1,39);
      PointY = RandomizePoint.Next(1,19);
        
  }
}

  class Snek {

 public int SnekPointX;
 public int SnekPointY;

 
        
  }



  
  
  
