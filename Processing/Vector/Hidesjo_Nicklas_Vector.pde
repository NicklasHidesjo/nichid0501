
PVector ballPos = new PVector();
PVector ballDir = new PVector();

float speed = 0.1f;

// increase this value to make the ball slow down faster.
float drag = 1.5f;

int ballsize = 50;

int s = 0;
boolean newSecondPassed = true;

void setup() 
{
	size(512, 512);
	ellipseMode(CENTER);	
	fill(200,0,50);

	// sets the balls starting position
	ballPos.x = width/4;
	ballPos.y = height/4;


}

void draw() 
{
	background(100,0,200);
	DrawBall();
	if(mousePressed == true)
	{
		DrawLine();
	}	
	bounce();
	ApplyDrag();
}

void DrawBall()
{
	stroke(200,0,50);
	strokeWeight(1);
	ballPos.add(ballDir);
	ellipse(ballPos.x, ballPos.y, ballsize,ballsize);
}


void DrawLine()
{
	strokeWeight(5);
	stroke(0,200,100);
	line(ballPos.x, ballPos.y, mouseX,mouseY);
	ballDir.x = 0;
	ballDir.y = 0;
}

void bounce()
{
	if(ballPos.x <= 0 + (ballsize/2) || ballPos.x > width - (ballsize/2))
	{
		ballDir.x *= -1;
	}

	if(ballPos.y <= 0 + (ballsize/2) || ballPos.y > height - (ballsize/2))
	{
		ballDir.y *= -1;
	}
}

void ApplyDrag()
{
 	int inacurateS = int(millis()/1000);

	if(inacurateS != s)
	{
		s = inacurateS;
		newSecondPassed = false;
	}
	//changing the modulus factor will determine how often the drag is applied to the ball.
	if(s % 1 == 0 && !newSecondPassed)
	{
		newSecondPassed = true;
		ballDir.x = DecreaseSpeed(ballDir.x);
		ballDir.y = DecreaseSpeed(ballDir.y);
	}

}

float DecreaseSpeed(float directionalSpeed)
{
	if(directionalSpeed > 0)
	{
		directionalSpeed -= drag;
		if(directionalSpeed < 0)
		{
			directionalSpeed = 0;
		}
	}

	return directionalSpeed;
}

//void mousePressed()
//{
//	ballPos.x = mouseX;
//	ballPos.y = mouseY;
//}

void mouseReleased()
{
	PVector mousePos = new PVector(mouseX,mouseY);
	ballDir = mousePos.sub(ballPos);
	ballDir = ballDir.mult(speed);
}