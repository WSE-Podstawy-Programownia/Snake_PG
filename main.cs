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
  static SnekPoint snekPunkt = new SnekPoint();
  static int moveDirection = 2;

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
          else if (x == snekPunkt.X && y == snekPunkt.Y) {
            Console.Write('@');
          }
          else {
            Console.Write(gameArea[x, y]); // wypisanie kolejnego znaku w danej linijce
          }
        }
        Console.WriteLine(""); // zeby zakonczyc linijke
    }
  }

  static void SpawnSnake () {
    snekPunkt.X = 1;
    snekPunkt.Y = 1;
  }

  static void DirectionChange () {
    int input = Console.Read();
    if (input > -1)
    {
      char wsadInput = Convert.ToChar(input);
      if (wsadInput == 'w') {
        moveDirection = 0;
      }
      if (wsadInput == 's') {
        moveDirection = 2;
      }
      if (wsadInput == 'a') {
        moveDirection = 1;
      }
      if (wsadInput == 'd') {
        moveDirection = 3;
      }
    }
  }

  public static void Main (string[] args) {
    WriteWelcomeMessage(); // wypisanie wiadomosci powitalnej
    PopulateGameArea(); // zapelnienie tablicy danymi, nie w while bo wystarczy, że wygeneruje się raz
    SpawnSnake();

    while (true){      // powtarzanie caly czas
      point.RandomizePosition();
      DrawGameArea(); // wypisanie obszaru gry
      Thread.Sleep(2000); // odczekanie 500ms
      snekPunkt.SnekMovement(moveDirection); 
      DirectionChange();
    }
  }
}

class Point {
  public int PointX;
  public int PointY;

  public void RandomizePosition () {
      
    Random RandomizePoint = new Random();
    PointX = RandomizePoint.Next(1,39); // wybiera losową liczbę z przedziału 1-39
    PointY = RandomizePoint.Next(1,19);
  }
}

class SnekPoint
{
  public int X;
  public int Y;

  public void SnekMovement (int direction) {
    if (direction == 0) {    
      Y--;  // jeżeli kierunek jest ustawiony w górę (0), to snek z każdym odświeżeniem przemieszcza się o 1 w tył na płaszczyźnie Y (Y--) 
    }

    if (direction == 2) {    
      Y++;  // jeżeli kierunek jest ustawiony w dół (2), to snek z każdym odświeżeniem przemieszcza się o 1 do przodu na płaszczyźnie Y (Y++) 
    }

    if (direction == 1) {    
      X--;   // jeżeli kierunek jest ustawiony w lewo (1), to snek z każdym odświeżeniem przemieszcza się o 1 w tył na płaszczyźnie X (X--) 
    }

    if (direction == 3) {    
      X++; // jeżeli kierunek jest ustawiony w prawo (3), to snek z każdym odświeżeniem przemieszcza się o 1 w przód na płaszczyźnie X (X++) 
    }
  }
}




  
  
  
