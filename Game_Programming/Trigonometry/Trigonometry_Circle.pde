
int dotSize = 10;
float angle = 0;
PVector center = new PVector();


int circleDots;
int circleRadius;
float circleRadiusMultiplier;
float circleSlice;
float circleSpeed;
int circleSpeedDivider;


int spiralDots;
int spiralRadius;
float spiralRadiusMultiplier;
float spiralSlice;
float spiralSpeed;
int spiralSpeedDivider;

void setup() 
{
	size(580, 460);
	center = new PVector(width/2,height/2);
}

void draw()
{
  	strokeWeight(dotSize);
	background(255);
	SetValues();
	DrawCircle();
	DrawSpiral();
}

void SetValues()
{
	dotSize = 10;

	circleRadius = 200;	  
  	circleSpeedDivider = 100;
	circleDots = 40;
	circleRadiusMultiplier = 5f;
	circleSlice = PI * circleRadiusMultiplier / circleDots;
	circleSpeed = millis() / circleSpeedDivider;
  
  
	spiralRadius = 200;
	spiralSpeedDivider = 100;
	spiralDots = 40;
	spiralRadiusMultiplier  = 5f;
	spiralSlice = PI * spiralRadiusMultiplier / spiralDots;
	spiralSpeed = millis() / spiralSpeedDivider;
}

void DrawCircle()
{
	for (int i = 0; i < circleDots; ++i) 
	{
		angle = i * circleSlice;
		float x = center.x + cos(angle + circleSpeed) * circleRadius;
		float y = center.y + sin(angle + circleSpeed) * circleRadius;
		point(x,y);
	}
}

void DrawSpiral()
{
	for (int i = 0; i < spiralDots; ++i) 
	{
		float distance = ((float) i / spiralDots);
		angle = i * spiralSlice;
		float x = center.x + cos(angle + spiralSpeed) * spiralRadius * distance;
		float y = center.y + sin(angle + spiralSpeed) * spiralRadius * distance;
		point(x,y);
	}
}
