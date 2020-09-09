
int xOffset = 0;
int yOffset = 0;

int offsetIncrease = 1; // högre värde ger snabbare rörelse utav namnet


// dessa styr huruvida x/yOffset ska öka eller minska-
boolean xDecrease = false; 
boolean yDecrease = false;


// Dessa styr det högsta och lägsta värdet som x/yOffset kan ha utan att texten hamnar utanför skärmen.
int xMin = -5;
int xMax = 145; 
int yMin = -90;
int yMax = 130;

void setup()
{
  size(768, 432);
  stroke(0,255,0);
}

void draw()
{
  background(0,0,0);
  strokeWeight(20.0);
  
  MoveName();
  DrawName();

}

void MoveName()
{
  if(xDecrease) 
  {
    xOffset -= offsetIncrease;
    if(xOffset <= xMin)
    {
      xDecrease = false;
      ChangeColor();
    }
  }
  else
  {
    xOffset += offsetIncrease;
    if(xOffset >= xMax)
    {
      xDecrease = true;
      ChangeColor();
    }
  }

  if (yDecrease) 
  {
    yOffset -= offsetIncrease;
    if(yOffset <= yMin)
    {
      yDecrease = false;
      ChangeColor();
    }
  }
  else { 
    yOffset += offsetIncrease;
    if(yOffset >= yMax)
    {
      yDecrease = true;
      ChangeColor();
    }  
  }
}


void ChangeColor()
{
  int color1 = randomInt(50,255);
  int color2 = randomInt(50,255);
  int color3 = randomInt(50,255);
  stroke(color1, color2, color3);
}

int randomInt(int minNumber,int maxNumber)
{
  return int(random(minNumber,maxNumber));
}


void DrawName()
{
  DrawN();
  DrawI();
  DrawC();
  DrawK();
  DrawL();
  DrawA();
  DrawS();
}

void DrawN()
{
  int x1 = 15;
  int x2 = 80;
  int y1 = 100;
  int y2 = 280;
  DrawLine(x1,y1,x1,y2);
  DrawLine(x1,y1,x2,y2);
  DrawLine(x2,y1,x2,y2);
}
void DrawI()
{
  int x = 110;
  int y1 = 140;
  int y2 = 130;
  int y3 = 280;
  int y4 = 180;

  DrawLine(x,y1,x,y2);
  DrawLine(x,y3,x,y4);
}
void DrawC()
{
  int x = 180;
  int y = 230;
  int w = 90; 
  int h = 100;
  float s = -6;
  float e = -1;

  noFill();
  DrawArc(x,y,w,h,s, e);
}
void DrawK()
{
  int x1 = 250;
  int x2 = 300;
  int x3 = 275;
  int x4 = 314;
  int y1 = 140;
  int y2 = 280;
  int y3= 240;
  int y4 = 180;
  int y5 = 230;

  DrawLine(x1,y1,x1,y2);
  DrawLine(x1,y3,x2,y4);
  DrawLine(x3,y5,x4,y2);
}
void DrawL()
{
  int x = 345;
  int y1 = 140;
  int y2 = 280;
  DrawLine(x,y1,x,y2);
}
void DrawA()
{
  int x1 = 420;
  int x2 = 490;
  int y1 = 237;
  int y2 = 240;
  int w = 80;
  int h = 90;
  int w2 = 60;
  int h2 = 90;
  int s = 1;
  int e = 3;
  
  noFill();
  DrawElipse(x1, y1, w,h);
  DrawArc(x2,y2,w2,h2,s,e);
}
void DrawS()
{
  int x = 570;
  int y1 = 255;
  int y2 = 195;
  int w1 = 83;
  int w2 = 70;
  int h1 = 71;
  int h2 = 60;
  int s1 = -2;
  int s2 = 2;
  int e1 = 3;
  int e2 = 6;
  noFill();
  DrawArc(x,y1,w1,h1,s1,e1);
  DrawArc(x,y2,w2,h2,s2,e2);
}


void DrawLine(int x1, int y1, int x2, int y2)
{
  line(x1 + xOffset, y1 + yOffset, x2 + xOffset, y2 + yOffset);
}
void DrawElipse(int x, int y, int w, int h)
{
  ellipse(x + xOffset,y + yOffset,w,h);
} 
void DrawArc(int x, int y, int w, int h, float s, float e)
{
  arc(x + xOffset,y + yOffset,w,h,s,e);
}
