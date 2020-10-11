public class Cell
{
	PVector gridPosition;
	boolean alive = false;
	int size;

	public Cell (boolean alive, PVector gridPosition) 
	{
		this.alive = alive;
		this.gridPosition = gridPosition;
		this.size = cellSize;
	}

	void draw()
	{
		if(alive)
		{
			fill(aliveColor);
		}
		else 
		{
			fill(deadColor);
		}
		rect(gridPosition.x * size, gridPosition.y * size, size, size);
	}

}