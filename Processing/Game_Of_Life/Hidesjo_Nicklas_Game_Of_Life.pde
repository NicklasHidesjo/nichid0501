	

	Cell [][] cells;
	Cell [][] cellbuffer;

	Cell[][] storedCellOne;
	Cell[][] storedCellTwo;

	int cellSize = 10;

	int tick = 100;
	int highTick = 1000;
	int lowTick = 20;
	int time = 0;

	boolean pause = false;
	boolean stable = false;
	
	int aliveChance = 20;

	color aliveColor = color(0,0,256);
	color deadColor = color(0);

	int generations = 1;
	
	PVector[] directions = 
		{
			new PVector(0,-1), // up
			new PVector(1,-1), // up-right corner
			new PVector(1,0), // right
			new PVector(1,1), // down-right corner
			new PVector(0,1), // down
			new PVector(-1,1), // down-left corner
			new PVector(-1,0), // left
			new PVector(-1,-1) // up-left corner 
		};


		int numberOfColumns;
		int numberOfRows;

	void setup() 
	{	
		size(1000,600);
		stroke(48);


		numberOfColumns = (int)Math.floor(width/cellSize);
		numberOfRows = (int)Math.floor(height/cellSize);

		CreateCells();
	}

	void CreateCells()
	{
		cells = new Cell[numberOfColumns][numberOfRows];
		cellbuffer = new Cell[numberOfColumns][numberOfRows];

		for (int i = 0; i < numberOfColumns; i ++) 
		{
			for (int j = 0; j < numberOfRows; j ++) 
			{
				cells[i][j] = new Cell(random(0, 100) < aliveChance, new PVector(i,j));
			}
		}
	}

	void draw() 
	{
		if(stable)
		{
			return;
		}
		if(pause)
		{
			return;
		}

			background(0);

			displayCells();

			if( millis()- time > tick)
			{
				changeCells();
				time = millis();
			}

			displayText();
	}


	void displayCells()
	{
		for (int i = 0; i < numberOfColumns; i ++) 
		{
			for (int j = 0; j < numberOfRows; j ++) 
			{
				if(cells[i][j].alive)
				{
					fill(aliveColor);
				}
				else 
				{
					fill(deadColor);
				}
				rect(cells[i][j].gridPosition.x * cellSize, cells[i][j].gridPosition.y * cellSize, cellSize, cellSize);
			}
		}
	}	

	void changeCells()
	{
		for (int i = 0; i < numberOfColumns; i ++) 
		{
			for (int j = 0; j < numberOfRows; j ++) 
			{
				cellbuffer[i][j] = cells[i][j];
			}
		}

		for (int i = 0; i < numberOfColumns; i ++) 
		{
			for (int j = 0; j < numberOfRows; j ++) 
			{
				int aliveNeighbours = neighboursAlive(cellbuffer[i][j]);

				if(cellbuffer[i][j].alive)
				{
					if(aliveNeighbours < 2 || aliveNeighbours > 3)
					{
						cells[i][j] = new Cell(false, new PVector(i,j));
					}
				}
				else if(aliveNeighbours == 3)
				{
					cells[i][j] = new Cell(true, new PVector(i,j));
				}
			}
		}

		generations++;
	}

	int neighboursAlive(Cell centerCell)
	{	
		int aliveNeighbours = 0;

		int xPosition = (int)centerCell.gridPosition.x;
		int yPosition = (int)centerCell.gridPosition.y;

		for (int i = 0; i < directions.length; ++i) 
		{
			int neighbourXPosition = (int)centerCell.gridPosition.x + (int)directions[i].x;
			int neighbourYPosition = (int)centerCell.gridPosition.y + (int)directions[i].y;
			if(neighbourXPosition < 0 || neighbourXPosition >= numberOfColumns || 
			   neighbourYPosition < 0 || neighbourYPosition >= numberOfRows)
			{
				continue;
			}
			if(cellbuffer[neighbourXPosition][neighbourYPosition].alive == true)
			{
				aliveNeighbours++;
			}
		}

		return aliveNeighbours;
	}


	void CheckStability()
	{

	}


	void displayText()
	{
		textAlign(LEFT);
		textSize(16);
		fill(255,0,0);
		text("Current generation: " + generations, 10, 25);
		text("Current tick time: " + tick, 10, 50);
	}
