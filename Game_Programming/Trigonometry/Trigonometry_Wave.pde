

int sinCurveSpeedDivider = 100;
float sinCurveSpeed = 0.1f;
int sinWavePos = 400;
int sinCurveHeight = 100;

int cosCurveSpeedDivider = 100;
float cosCurveSpeed = 0.1f;
int cosWavePos = 800;
int cosCurveHeight = 100;

int numberOfPoints = 0;
int dotDistance = 1;
int dotSize = 10;



void setup() 
{
	size(displayWidth, displayHeight);
}

void draw() 
{
	background(0);
	SetValues();
	DrawCurves();
}

void SetValues()
{
	sinCurveSpeedDivider = 40;
	sinCurveSpeed = 0.1f;
	sinWavePos = displayHeight / 2;
	sinCurveHeight = 200;

	cosCurveSpeedDivider = 40;
	cosCurveSpeed = 0.1f;
	cosWavePos = displayHeight / 2;
	cosCurveHeight = 200;

	dotSize = 10;
	numberOfPoints = displayWidth / 10;
	dotDistance = (displayWidth / numberOfPoints) + dotSize;
}

void DrawCurves()
{
	for (int i = 0; i < numberOfPoints; ++i) 
	{
		strokeWeight(dotSize);
		DrawSinPoint(i);
		DrawCosPoint(i);
	}
}

void DrawSinPoint(int i)
{
	float curveSpeed = ((millis() /cosCurveSpeedDivider) + i) * sinCurveSpeed;
	stroke(256, 0, 0);
	float x = i * dotDistance;
	float y = sinWavePos + sin(curveSpeed) * sinCurveHeight;
	point(x,y);
}

void DrawCosPoint(int i)
{
	float curveSpeed = ((millis() / sinCurveSpeedDivider) + i) * cosCurveSpeed + PI/2;
	stroke(0,0,256);
	float x = i * dotDistance;
	float y = cosWavePos + cos(curveSpeed) * cosCurveHeight;
	point(x,y);
}