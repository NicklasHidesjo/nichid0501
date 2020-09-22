public class Ball
{
	PVector pos;
	PVector vel;

	int maxSpeed = 10;

	color ballColor;
	int ballSize;

	Ball(float x, float y, int ballSize, color ballColor)
	{
		this.ballSize = ballSize;
		this.ballColor = ballColor;
		pos = new PVector(x,y);

		vel = new PVector();
		vel.x = random(2,10)-5;
		vel.y = random(2,10)-5;
	}

	void Update()
	{
		pos.x += vel.x;
		pos.y += vel.y;
		constrainBall();
		fill(ballColor);
		ellipse(pos.x, pos.y, ballSize, ballSize);
	}

	void constrainBall()
	{
		if(pos.x >= width - (ballSize/2) || pos.x <= ballSize/2)
		{
			vel.x += random(4) -2; // this adds a bit of randomness to the balls x velocity making it harder to predict how they will bounce
			if(vel.x > maxSpeed)
			{
				vel.x = maxSpeed;
			}
			if(vel.x < -maxSpeed)
			{
				vel.x = -maxSpeed;
			}
			vel.x *=-1;
		}

		if(pos.y >= height - (ballSize/2) || pos.y <= ballSize/2)
		{
			vel.y += random(4)-2; // this adds a bit of randomness to the balls y velocity making it harder to predict how they will bounce
			if(vel.y > maxSpeed)
			{
				vel.y = maxSpeed;
			}
			if(vel.y < -maxSpeed)
			{
				vel.y = -maxSpeed;
			}
			vel.y *=-1;	
		}
	}

	PVector GetPos()
	{
		return pos;
	}

	int GetSize()
	{
		return ballSize;
	}
}