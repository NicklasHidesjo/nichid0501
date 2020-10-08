public class Cell
{
	PVector gridPosition;
	boolean alive = false;

	public Cell (boolean alive, PVector gridPosition) 
	{
		this.alive = alive;
		this.gridPosition = gridPosition;
	}

}