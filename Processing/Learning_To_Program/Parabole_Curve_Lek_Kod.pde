PVector[] numbers = new PVector[200];

boolean xDecrease = false;
boolean yDecrease = false;
boolean distanceDecrease = false;

int distance = 1; // change this to alter the entire look of the parabole (play with it on your own risk).


void setup()
{
  size(1000, 1000);
  strokeWeight(3);
  SetLinePositions();
}  

void SetLinePositions()
{ 


  float distance = 800/(numbers.length/2);
  
  int xBase = 500;
  int yBase = 100;

  for(int i= 0; i< numbers.length; i++)
  {
    numbers[i] = new PVector(xBase,yBase);
    
    // if you put a / or * factor after distance down here you will be able to create wierd shapes.
    if(yDecrease)
      yBase -= distance;
    else
      yBase += distance;
    
    if(xDecrease)
      xBase -= distance;
    else
      xBase += distance;
    
    if(xBase >= 900 || xBase <= 100)
      xDecrease = !xDecrease;

    if(yBase >= 900 || yBase <= 100)
      yDecrease = !yDecrease;
  }
}

void draw()
{
  background(255, 0, 0);
  DrawRomb();

  if(distance < 1 || distance >= numbers.length-1)
    distanceDecrease = !distanceDecrease;
  
  if(distanceDecrease)
   distance--; 
 else
   distance++; 


 for (int i=0; i < numbers.length; i++)
 {
    int point2 = i+ distance;
    if(point2 > numbers.length-1)
      point2 = point2 - numbers.length;

    line(numbers[i].x, numbers[i].y, numbers[point2].x, numbers[point2].y);
 }
}

void DrawRomb()
{
  line(100, 500, 500, 100);
  line(500, 100, 900, 500);
  line(900, 500, 500, 900);
  line(500, 900, 100, 500);
}