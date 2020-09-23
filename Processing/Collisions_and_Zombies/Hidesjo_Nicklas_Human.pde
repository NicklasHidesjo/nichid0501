public class Human extends Character 
{
	public Human (float posX, float posY, float velX, float velY, int charSize) 
	{
		super(posX,posY,velX,velY,charSize);
		ChangeSkinColor();
	} 	

	void ChangeSkinColor()
	{
		int r = (int)random(210,256);
		int g = (int)random(125,156);
		int b = (int)random(80,126);
		skinColor = color(r,g,b);			
	}

}