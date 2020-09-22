long currentTime;
float deltaTime;
float time;

int score = 0;

Player myPlayer;
Ball_Handler ballHandler;


color base = color(100, 100, 100);
color playersColor = color(255,0,0);

// this is used to make sure only one ball is spawned every 3 seconds
int s = 0;
boolean newSecondPassed = false;

Ball[] balls;
Ball testBall;

String scoreText;
PFont font;

void setup() 
{
	size(1200,1000);
	ellipseMode(CENTER);
	CreateReferences();
	ballHandler.CreateBall();

}

void CreateReferences()
{
	myPlayer = new Player(width/2, height/2, playersColor);
	ballHandler = new Ball_Handler(myPlayer);
	font = createFont("Arial", 25,true);

}

void draw() 
{
	background(base);
	if(!myPlayer.Collision())
	{
		UpdateGame();
	}
	else 
	{
		DisplayGameOverScreen();

	}

}

void UpdateGame()
{
	DisplayGameInfo();
	CalculateDeltaTime();
	myPlayer.UpdatePlayer();
	ballHandler.MoveBalls();
	CreateBall();
}

void DisplayGameInfo()
{
	textFont(font,25);
	fill(0);
	textAlign(RIGHT);
	text("Score: ",100,50);
	text("Balls: ",90,80);
	textAlign(LEFT);
	score = ballHandler.currentBall * s;
	scoreText = "" + score;
	text(scoreText,100, 50);
	String numberOfBalls = "" +ballHandler.currentBall;
	text(numberOfBalls,100,80);
}

void CalculateDeltaTime()
{
	currentTime = millis();
	deltaTime = (currentTime - time) * 0.001f;
	time = currentTime;
}

void CreateBall()
{
	int rawTime = millis()/1000;
	if(rawTime != s)
	{
		s = rawTime;
		newSecondPassed = true;
	}
	if(s % 3 == 0 && newSecondPassed)
	{
		newSecondPassed = false;
		ballHandler.CreateBall();
	}
}

void DisplayGameOverScreen()
{
	textFont(font,25);
	fill(0);
	textAlign(CENTER);
	text("Game Over",width/2, height/2);
	String finalScore = "Final score: " + scoreText;
	text(finalScore, width/2, (height/2 + 50));
}


