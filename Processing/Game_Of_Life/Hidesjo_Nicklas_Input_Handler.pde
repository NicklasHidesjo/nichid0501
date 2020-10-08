


void keyPressed()
{
	if(key == '+')
	{
		tick += 10;
		if(tick > highTick)
		{
			tick = highTick;
		}
	}
	if(key == '-')
	{
		tick -= 10;
		if(tick < lowTick)
		{
			tick = lowTick;
		}
	}

	if(key == 'p')
	{
		pause = !pause;
	}
}