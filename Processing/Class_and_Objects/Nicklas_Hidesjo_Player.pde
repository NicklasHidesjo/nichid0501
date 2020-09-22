public class Player
{
	float xMov = 0;
	float yMov = 0;
	float speed = 400f;

	float drag = 0.95f;

	float gravityPull = 1000f;

	int ballSize = 20;

	color playerColor;

	PVector playerPos;

	Player(int x,int y, color playerColor)
	{
		playerPos = new PVector(x,y);
		this.playerColor = playerColor;
	}

	void UpdatePlayer()
	{
		CalculateXMov();
		CalculateYMov();
		WrapandConstrainBall();
		ChangePlayerPos();
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

	void calculateGravity()
	{
		if(gravity)
		{
			yMov += gravityPull * deltaTime;
		}
		if(playerPos.y >= height - (ballSize/2))
		{
			yMov *= -0.8;
		}
	}

	void WrapandConstrainBall()
	{
		if(playerPos.x >= width - (ballSize/2))
		{
			playerPos.x = width - (ballSize/2);
		}
		if(playerPos.x <= ballSize/2)
		{
			playerPos.x = ballSize/2;
		}

		if(playerPos.y >= height - (ballSize/2))
		{
			playerPos.y = height -(ballSize/2);
		}
		if(playerPos.y <= ballSize/2)
		{
			playerPos.y = ballSize/2;
		}
	}

	void ChangePlayerPos()
	{
		PVector acceleration = new PVector(xMov,yMov);
		acceleration.limit(speed * 10);
		playerPos.add(acceleration.mult(deltaTime));
		fill(playerColor);
		ellipse(playerPos.x, playerPos.y,ballSize, ballSize);
	}

	boolean Collision()
	{
		boolean collided = false;
		for (int i = 0; i < balls.length; ++i) 
		{
			if(balls[i] == null){return collided;}
			PVector currentBallPos = balls[i].GetPos();
			int currentBallSize = balls[i].ballSize /2;

			if(CheckIfTouching(playerPos,currentBallPos,ballSize /2,currentBallSize))
			{
				collided = true;
			}
		}

		return collided;
	}

	boolean CheckIfTouching(PVector pos1, PVector pos2, int size1, int size2)
	{
		int maxDist = size1 + size2;

		if(abs(pos1.x - pos2.x) > maxDist || abs(pos1.y - pos2.y) > maxDist)
		{
			return false;
		}
		else if(dist(pos1.x, pos1.y, pos2.x, pos2.y) > maxDist)
		{
			return  false;
		}
		else 
		{
			return  true;
		}
	}
}