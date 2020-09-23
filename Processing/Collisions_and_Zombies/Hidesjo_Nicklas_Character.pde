public class Character
{
	int spawnedNumber = 0;


	PVector pos;
	PVector vel;

	color skinColor;
	int charSize;

	Character(float posX, float posY, float velX, float velY, int charSize)
	{
		this.charSize = charSize;
		pos = new PVector(posX,posY);
		vel = new PVector(velX,velY);


	}

	void Update()
	{	
		pos.x += vel.x * deltaTime;
		pos.y += vel.y * deltaTime;
		wrap();
		fill(skinColor);
		ellipse(pos.x, pos.y, charSize, charSize);
	}

	void wrap()
	{
		if(pos.x >= width - (charSize/2) || pos.x <= charSize/2)
		{
			if(pos.x <= (charSize/2))
			{
				pos.x = width - (charSize/2);
			}
			else 
			{
				pos.x = charSize/2;
			}
		}

		if(pos.y >= height - (charSize/2) || pos.y <= charSize/2)
		{
			if(pos.y <= charSize/2)
			{
				pos.y = height - (charSize/2);
			}
			else 
			{
				pos.y = charSize/2;	
			}
		}
	}

	PVector GetPos()
	{
		return pos;
	}

	int GetSize()
	{
		return charSize;
	}

}