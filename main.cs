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
 static void WriteGoodbyeMessage()
  {
    Clear();
    string wiadomoscPozegnalna = "\nPrzegrales! Haha!";
    WriteLine(wiadomoscPozegnalna);
    ReadLine();
  }

  static int gameAreaXSize = 41;
  static int gameAreaYSize = 21;
  static char [,] gameArea = new char[gameAreaXSize, gameAreaYSize];
  static Point point = new Point();
  static Snek snake = new Snek();
  static int moveDirection = 2;
  static bool dead = false;
 
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
          else if (snake.IsThisPositionMe(x, y)) {
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
    snake.CreatePoints(6);
  }
 
  static void CheckDirectionChange () {
    if (Console.KeyAvailable){
      ConsoleKeyInfo input = Console.ReadKey();

      if (input.KeyChar == 'w') {
        moveDirection = 0;
      }
      if (input.KeyChar == 's') {
        moveDirection = 2;
      }
      if (input.KeyChar == 'a') {
        moveDirection = 1;
      }
      if (input.KeyChar == 'd') {
        moveDirection = 3;
      }
    }
  }

  static void CheckCollision()
  {
    int myNextX = snake.points[0].X;
    int myNextY = snake.points[0].Y;
    if (moveDirection == 0) {    
      myNextY--;  // jeżeli kierunek jest ustawiony w górę (0), to snek z każdym odświeżeniem przemieszcza się o 1 w tył na płaszczyźnie Y (Y--)
    }
 
    if (moveDirection == 2) {    
      myNextY++;  // jeżeli kierunek jest ustawiony w dół (2), to snek z każdym odświeżeniem przemieszcza się o 1 do przodu na płaszczyźnie Y (Y++)
    }
 
    if (moveDirection == 1) {    
      myNextX--;   // jeżeli kierunek jest ustawiony w lewo (1), to snek z każdym odświeżeniem przemieszcza się o 1 w tył na płaszczyźnie X (X--)
    }
 
    if (moveDirection == 3) {    
      myNextX++; // jeżeli kierunek jest ustawiony w prawo (3), to snek z każdym odświeżeniem przemieszcza się o 1 w przód na płaszczyźnie X (X++)
    }

    if (myNextX == point.PointX && myNextY == point.PointY) {
      snake.AddPoint(myNextX, myNextY);
      point.RandomizePosition();
    }
    else if(gameArea[myNextX, myNextY] == '#' || snake.IsThisPositionMe(myNextX, myNextY)) {
      dead = true;
    }

  }
 
  public static void Main (string[] args) {
    WriteWelcomeMessage(); // wypisanie wiadomosci powitalnej
    PopulateGameArea(); // zapelnienie tablicy danymi, nie w while bo wystarczy, że wygeneruje się raz
    SpawnSnake(); // tworzenie snejka na poczatku gry
    point.RandomizePosition();
 
    while (dead == false){      // powtarzanie dopoki nie jest dead
      CheckDirectionChange();
      DrawGameArea(); // wypisanie obszaru gry
      Thread.Sleep(200); // odczekanie 500ms
      CheckCollision();
      snake.MoveSnek(moveDirection);
    }

    WriteGoodbyeMessage(); // wypisanie wiadomosci koncowej
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
 
class Snek {
  public SnekPoint[] points;
  public int length;
 
  public void CreatePoints(int number) {
    points = new SnekPoint[number];
    length = number;
    int changingY = 1;
    for (int pointNumber = number-1; pointNumber >= 0; pointNumber--){
      points[pointNumber] = new SnekPoint();
      points[pointNumber].Y = changingY;
      points[pointNumber].X = 1;
      changingY++;
    }
  }
 
  public void MoveSnek(int direction){
    for (int pointNumber = length-1; pointNumber >= 1; pointNumber--){
      points[pointNumber].X = points[pointNumber-1].X;
      points[pointNumber].Y = points[pointNumber-1].Y;
    }
    points[0].SnekMovement(direction);
  }
 
  public bool IsThisPositionMe(int x, int y)
  {
    for (int pointNumber = 0; pointNumber < length; pointNumber++)
    {
      if (points[pointNumber].X == x && points[pointNumber].Y == y)
      {
        return true;
      }
    }
    return false;
  }

  public void AddPoint(int x, int y)
  {
    SnekPoint[] newPoints = new SnekPoint[length+1];
    newPoints[0] = new SnekPoint();
    newPoints[0].X = x;
    newPoints[0].Y = y;

    for (int pointNumber = 0; pointNumber < length; pointNumber++)
    {
      newPoints[pointNumber+1] = new SnekPoint();
      newPoints[pointNumber+1].X = points[pointNumber].X;
      newPoints[pointNumber+1].Y = points[pointNumber].Y;
    }
    
    length++;
    points = newPoints;
  }
}




  
  
  
