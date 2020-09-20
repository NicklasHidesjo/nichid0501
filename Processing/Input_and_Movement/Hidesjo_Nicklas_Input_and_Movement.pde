long currentTime;
float deltaTime; 
float time;

float xMov = 0;
float yMov = 0;
float speed = 200f;

float drag = 0.95f;

float gravityPull = 1000f;

int ballSize = 50;

PVector ballPos = new PVector(); 


void setup() 
{
	size(600,600);
	ballPos = new PVector(width/2, height/2);
	ellipseMode(CENTER);
}

void draw() 
{
	background(100,100,100);
	CalculateDeltaTime(1);

	CalculateXMov();
	CalculateYMov();
	CalculateGravity();

	MoveBall();
	WrapandConstrainBall();
	CalculateDeltaTime(0);
}

void CalculateDeltaTime(int start)
{
	if(start == 1)
	{
		currentTime = millis();
		deltaTime = (currentTime - time) * 0.001f;
	}
	else 
	{
		time = currentTime;
	}
}

void CalculateXMov()
{
	if(moveLeft)
	{
		if(xMov > 0 )
		{
			xMov = 0;
		}
		xMov -= speed * deltaTime;
	}

	if(moveRight)
	{
		if(xMov < 0)
		{
			xMov = 0;
		}

		xMov += speed * deltaTime;
	}

	if(!moveLeft && !moveRight)
	{	
		xMov *= drag;
	}
}

void CalculateYMov()
{
	if(moveUp)
	{
		if(yMov > 0)
		{
			yMov = 0;
		}
		yMov -= speed * deltaTime;
	}
	if(moveDown)
	{
		if(yMov < 0)
		{
			yMov = 0;
		}
		yMov += speed * deltaTime ;
	}
	if(!moveUp && !moveDown && !gravity)
	{
		yMov *= drag;
	}
}

void CalculateGravity()
{
	if(gravity)
	{
		yMov += gravityPull * deltaTime;
	}
	if(ballPos.y >= height - (ballSize/2))
	{
		yMov *= -0.8;
	}
}

void MoveBall()
{
  PVector acceleration = new PVector(xMov,yMov);

  acceleration.limit(speed * 10);

  ballPos.add(acceleration.mult(deltaTime));

  ellipse(ballPos.x, ballPos.y,ballSize, ballSize);
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
