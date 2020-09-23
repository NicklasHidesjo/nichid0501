public class Zombie extends Character
{

	public Zombie(float posX, float posY, float velX, float velY, int charSize)
	{
		super(posX,posY,velX,velY,charSize);
		ChangeSkinColor();
	} 	

	void ChangeSkinColor()
	{
		int r = (int)random(60,216);
		int g = (int)random(220,256);
		int b = (int)random(40,106);
		skinColor = color(r,g,b);			
	}

}