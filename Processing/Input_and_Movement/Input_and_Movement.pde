float deltaTime;
float time;

float xAcceleration = 0;
float yAcceleration = 0;
float speed = 1f;
float topGear = 20;

float drag = 0.9f;

int ballSize = 50;

PVector ballPos = new PVector(); 
PVector velocity = new PVector(0,0);


void setup() 
{
	size(600,600);
	ballPos = new PVector(width/2, height/2);
	ellipseMode(CENTER);
}

void draw() 
{
	background(100,100,100);
	CalculateDeltaTime();

	MoveLeftRight();
	MoveUpDown();

	MoveBall();
	WrapandConstrainBall();
}

void CalculateDeltaTime()
{
	long currentTime = millis();
	deltaTime = (currentTime - time) * 0.001f;
	time = deltaTime;
}

void MoveLeftRight()
{
	if(moveLeft)
	{
		xAcceleration -= speed;
	}

	if(moveRight)
	{
		xAcceleration += speed;
	}

	if(!moveLeft && !moveRight)
	{	
		xAcceleration = 0f;
		velocity.x *= drag;
	}
}

void MoveUpDown()
{
	if(moveUp)
	{
		yAcceleration -= speed;
	}
	if(moveDown)
	{
		yAcceleration += speed;
	}
	if(!moveUp && !moveDown)
	{
		yAcceleration = 0f;
		velocity.y *= drag;
	}
}

void WrapandConstrainBall()
{
	if(ballPos.x >= width)
	{
		ballPos.x = 1;
	}
	if(ballPos.x <= 0)
	{
		ballPos.x = width;
	}

	if(ballPos.y >= height - (ballSize/2))
	{
		ballPos.y = height -(ballSize/2);
	}
	if(ballPos.y <= 0 + (ballSize/2))
	{
		ballPos.y = ballSize/2;
	}
}

void MoveBall()
{
	PVector acceleration = new PVector(xAcceleration,yAcceleration);
	velocity.add(acceleration.mult(deltaTime));
	velocity.limit(topGear);
	ballPos.add(velocity);

	ellipse(ballPos.x, ballPos.y,ballSize, ballSize);
}


