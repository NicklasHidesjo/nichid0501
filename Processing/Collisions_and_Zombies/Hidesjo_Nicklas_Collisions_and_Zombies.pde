long currentTime;
float deltaTime;
long time;

int worldEndTime = 0;

Character_Handler charHandler = new Character_Handler();

PFont font;


void setup() 
{
	font = createFont("Arial", 25,true);
	size(1000, 1000);
}

void draw() 
{
	background(100, 100, 100);
	charHandler.MoveCharacters();
	CalculateDeltaTime();

	if(!CheckForHumans())
	{
		textFont(font,25);
		fill(0);
		textAlign(CENTER);
		text("World Over",width/2, height/2);
		String timeText = "Time until humanity perished: " + worldEndTime;
		text(timeText, width/2, (height/2 + 50));
	}	
	
}

boolean CheckForHumans()
{
	for (int i = 0; i < charHandler.characters.length; ++i) 
	{
		if(charHandler.characters[i] instanceof Human)
			return true;
	}
	if(worldEndTime == 0)
	{
		worldEndTime = (int)time/1000;
	}
	return false;
}

void CalculateDeltaTime()
{
	currentTime = millis();
	deltaTime = (currentTime - time) * 0.001f;
	time = currentTime;
}