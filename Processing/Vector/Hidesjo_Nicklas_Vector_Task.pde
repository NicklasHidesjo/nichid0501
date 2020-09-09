

boolean trackingMouse = false;

PVector ballPos = new PVector(100,100);
PVector mousePos = new PVector(0,0);
PVector ballSpeed = new PVector(0,0);

int maxBallSpeed = 10;
int ballSize = 50;
int speedDivider = 100;


void setup()
{
	size(1200, 800);
	stroke(0,255,0);
	fill(0,255,0);
	strokeWeight(2);
}

void draw()
{		
	background(0,0,0);
	TrackMouse();
	MoveBall();
	BounceBall();
}

void TrackMouse()
{
	if(trackingMouse)
	{
		mousePos.x = mouseX;
		mousePos.y = mouseY;
		line(ballPos.x,ballPos.y,mouseX,mouseY);
	}
}

void MoveBall()
{
	ballPos.add(ballSpeed);
	ellipse(ballPos.x, ballPos.y, ballSize,ballSize);
}

void BounceBall()
{
	if(ballPos.x > (width - ballSize/2) || ballPos.x < (0 + ballSize/2))
	{
		ballSpeed.x = ballSpeed.x *-1;
	}
	if(ballPos.y > (height - ballSize/2) || ballPos.y < (0 + ballSize/2))
	{
		ballSpeed.y = ballSpeed.y *-1;
	}
	ballSpeed.limit(maxBallSpeed);
}

void mousePressed()
{
	trackingMouse = true;
}

void mouseReleased()
{
	trackingMouse = false;
	SetDirectionAndSpeed();
}

void SetDirectionAndSpeed()
{
	PVector direction = new PVector(mousePos.x - ballPos.x, mousePos.y - ballPos.y).normalize();
	float distance = PVector.dist(ballPos,mousePos) / speedDivider;
	ballSpeed.add(direction.mult(distance));
}
