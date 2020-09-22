public class Ball_Handler
{
	int maxBalls = 100;
	int currentBall = 0;

	int safeZone = 25;

 	// the variables for setting ball color
 	int r = 0;
 	int g = 0;
 	int b = 0;

 	// the variables for ball x, y position and size.
 	int x = 0;
 	int y = 0;
 	int size = 0;


 	Player myPlayer;

 	Ball_Handler(Player myPlayer) 
 	{
 		this.myPlayer = myPlayer;
 		balls = new Ball[maxBalls];
 	}

 	void CreateBall()
 	{
 		if (currentBall < balls.length) 
 		{
 			ChangeBallColor();
 			SetBallSpawn();

 			balls[currentBall] = new Ball(x,y,size, color(r,g,b));
 			currentBall++;
 		}	
 	}

 	void ChangeBallColor()
 	{
 		boolean differentColor = false;
 		while(!differentColor)
 		{
 			r = (int)random(255);
 			g = (int)random(255);
 			b = (int)random(255);
 			color ballColor = color(r,g,b);
 			if(ballColor != base && ballColor != playersColor && r < 200)
 			{
 				differentColor = true;
 			}
 		}		
 	}

 	void SetBallSpawn()
 	{
 		size = (int)random(10,30);

 		PVector playerPos = myPlayer.playerPos;
 		boolean safeX = false;
 		while(!safeX)
 		{
 			x = (int)random(size*2, width - (size*2));
 			if(x < playerPos.x - safeZone || x > playerPos.x + safeZone)
 			{
 				safeX = true;
 			}
 		}
 		boolean safeY = false;
 		while(!safeY)
 		{
 			y = (int)random(size*2, height - (size*2));
 			if(y < playerPos.y - safeZone || y > playerPos.y + safeZone)
 			{
 				safeY = true;
 			}

 		}
 	}

 	void MoveBalls()
 	{
 		for (int i = 0; i < balls.length; ++i) 
 		{ 
 			if(balls[i] == null){return;}
 			balls[i].Update();
 		}		
 	}
 }