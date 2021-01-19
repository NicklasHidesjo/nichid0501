

int frame = 0;
float sinCurveSpeed = 0.04f;
float cosCurveSpeed = 0.1f;
int numberOfPoints = 0;
int dotDistance = 1;
int dotSize = 10;

int sinWaveHeight = 400;
int cosWaveHeight = 800;
int heightMultiplier = 100;



void setup() 
{
	size(displayWidth, displayHeight);
	numberOfPoints = displayWidth / 4;
	dotDistance = (displayWidth / numberOfPoints) + dotSize;
}

void draw() 
{
	background(255);
	DrawCurves();
}

void DrawCurves()
{
	for (int i = 0; i < numberOfPoints; ++i) 
	{
		strokeWeight(dotSize);
		DrawSinPoint(i);
		DrawCosPoint(i);
	}
	frame++;
}

void DrawSinPoint(int i)
{
	stroke(256, 0, 0);
	point(i * dotDistance, sinWaveHeight + sin((frame + i ) * sinCurveSpeed) * heightMultiplier);
}

void DrawCosPoint(int i)
{
	stroke(0,0,256);
	point(i * dotDistance, cosWaveHeight + cos((frame + i) * cosCurveSpeed + PI/2) * heightMultiplier);
}