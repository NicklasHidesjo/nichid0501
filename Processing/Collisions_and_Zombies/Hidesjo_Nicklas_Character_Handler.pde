public class Character_Handler
{
	Character[] characters;

	int maxCharacters = 100;
	int currentlyspawned = 0;

	Character_Handler() 
	{
		characters = new Character[maxCharacters];
		CreateCharacter();
	}

	void CreateCharacter()
	{
		int chosenOne = (int)random(maxCharacters);
		while (currentlyspawned < characters.length) 
		{
			int charSize = (int)random(20,30);
			int posX = (int)random(1000);
			int posY = (int)random(1000);

			int velX = (int)random(100,500)-300;
			int velY = (int)random(100,500)-300;
			characters[currentlyspawned] = new Human(posX,posY,velX,velY,charSize);
			if(currentlyspawned == chosenOne)
			{
				characters[currentlyspawned] = new Zombie(posX,posY,velX,velY,charSize);
			}
			currentlyspawned++;
		}
	}

	void MoveCharacters()
	{
		for (int i = 0; i < characters.length; ++i) 
		{
			characters[i].Update();
			if(characters[i] instanceof Zombie)
			{
				for (int j = 0; j < characters.length; ++j)
				{
					if(CheckZombieCollision(characters[i], characters[j]) && characters[j] instanceof Human)
					{
						int posX = (int)characters[j].pos.x;
						int posY = (int)characters[j].pos.y;
						int velX = (int)characters[j].vel.x;
						int velY = (int)characters[j].vel.y;
						int charSize = characters[j].charSize;

						characters[j] = new Zombie(posX,posY,velX,velY,charSize);
					}
				}
			}
		}		
	}

	boolean CheckZombieCollision(Character one, Character two)
	{	
		int maxDistance = one.charSize/2 + two.charSize/2;
		if(abs(one.pos.x - two.pos.x) > maxDistance || abs(one.pos.y - two.pos.y) > maxDistance) {return false;}
		else if(dist(one.pos.x,one.pos.y,two.pos.x,two.pos.y) > maxDistance) {return false;}
		else {return true;}
	}
}
