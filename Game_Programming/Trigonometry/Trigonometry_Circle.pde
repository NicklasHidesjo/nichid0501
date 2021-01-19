
int dotSize = 10;
float angle = 0;
float frame;
PVector center = new PVector();


int circleDots;
int circleRadius;
float circleRadiusMultiplier;
float circleSlice;
float circleSpeed;


int spiralDots;
int spiralRadius;
float spiralRadiusMultiplier;
float spiralSlice;
float spiralSpeed;

void setup() 
{
	frameRate(10);
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
	frame++;
}

void SetValues()
{
  	dotSize = 10;
	circleRadius = 200;	
	circleSpeed = 10f;
	circleDots = 40;
	circleRadiusMultiplier = 5f;
	circleSlice = PI * circleRadiusMultiplier / circleDots;

	spiralRadius = 200;
	spiralSpeed = 10f;
	spiralDots = 40;
	spiralRadiusMultiplier  = 5f;
	spiralSlice = PI * spiralRadiusMultiplier / spiralDots;
}

void DrawCircle()
{
	for (int i = 0; i < circleDots; ++i) 
	{
		angle = i * circleSlice + frame / circleSpeed;
		float x = center.x + cos(angle) * circleRadius;
		float y = center.y + sin(angle) * circleRadius;
		point(x,y);
	}
}

void DrawSpiral()
{
	for (int i = 0; i < spiralDots; ++i) 
	{
		float distance = ((float) i / spiralDots);
		angle = i * spiralSlice + frame / spiralSpeed;
		float x = center.x + cos(angle) * spiralRadius * distance;
		float y = center.y + sin(angle) * spiralRadius * distance;
		point(x,y);
	}
}
