public boolean moveRight,moveLeft,moveUp,moveDown,gravity;


void keyPressed()
{
	if(keyCode == RIGHT || key == 'd')
	{
		moveRight = true;
	}
	if (keyCode == LEFT || key == 'a')
	{
		moveLeft = true;
	}
	if(keyCode == UP || key =='w')
	{
		moveUp = true;
	}
	if(keyCode == DOWN || key == 's')
	{
		moveDown = true;
	}
	if(key == 'g')
	{
		gravity = !gravity;
	}
}

void keyReleased()
{
	if(keyCode == RIGHT || key == 'd')
	{
		moveRight = false;
	}
	if (keyCode == LEFT || key == 'a')
	{
		moveLeft = false;
	}
		if(keyCode == UP || key == 'w')
	{
		moveUp = false;
	}
	if(keyCode == DOWN || key == 's')
	{
		moveDown = false;
	}
}