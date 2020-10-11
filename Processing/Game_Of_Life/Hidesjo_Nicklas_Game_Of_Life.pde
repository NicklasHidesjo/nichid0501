Cell [][] cells;
Cell [][] cellbuffer;

Cell[][] storedCellOne;
Cell[][] storedCellTwo;
Cell[][] storedCellThree;

int cellSize = 10;

int tick = 100;
int highTick = 1000;
int lowTick = 10;
int time = 0;

boolean pause = false;
boolean stable = false;
boolean screenCleared = false;

int aliveChance = 20;

color aliveColor = color(0,0,256);
color deadColor = color(0);

int generations = 0;

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

			storedCellOne = new Cell[numberOfColumns][numberOfRows];
			storedCellTwo = new Cell[numberOfColumns][numberOfRows];
			storedCellThree = new Cell[numberOfColumns][numberOfRows];

			for (int i = 0; i < numberOfColumns; i++) 
			{
				for (int j = 0; j < numberOfRows; j++) 
				{
					cells[i][j] = new Cell(random(0, 100) < aliveChance, new PVector(i,j));
				}
			}
	}

	void draw() 
	{
		if(pause)
		{
			displayText();				
		}

		if(stable)
		{
			return;
		}

		if( millis() - time > tick)
		{
			background(0);
			checkStability();
			displayCells();
			storeCells();
			changeCells();
			displayText();
			time = millis();
		}
	}

	void displayCells()
	{
		for (int i = 0; i < numberOfColumns; i++) 
		{
			for (int j = 0; j < numberOfRows; j++) 
			{
				cells[i][j].draw();
			}
		}
	}	

	void changeCells()
	{
		if(!pause)
		{
			for (int i = 0; i < numberOfColumns; i++) 
			{
				for (int j = 0; j < numberOfRows; j++) 
				{
					cellbuffer[i][j] = cells[i][j];
				}
			}

			for (int i = 0; i < numberOfColumns; i++) 
			{
				for (int j = 0; j < numberOfRows; j++) 
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
			if(!stable && !screenCleared)
			generations++;			
		}
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

	void checkStability()
	{
		if(pause) 
		{
			stable = false;
			return;
		}
		if(generations <= 3)
		{
			stable = false;
			return;
		}

		if(generations % 3 == 0)
		{
			for (int i = 0; i < numberOfColumns; ++i) 
			{
				for (int j = 0; j < numberOfRows; ++j) 
				{
					if(storedCellThree[i][j].alive != cells[i][j].alive)
					{
						stable = false;
						return;
					}
				}
			}

		}

		else if(generations % 2 == 0)
		{
			for (int i = 0; i < numberOfColumns; ++i) 
			{
				for (int j = 0; j < numberOfRows; ++j) 
				{
					if(storedCellTwo[i][j].alive != cells[i][j].alive)
					{
						stable = false;
						return;
					}
				}
			}
		}

		else 
		{
			for (int i = 0; i < numberOfColumns; ++i) 
			{
				for (int j = 0; j < numberOfRows; ++j) 
				{
					if(storedCellOne[i][j].alive != cells[i][j].alive)
					{
						stable = false;
						return;

					}
				}
			}
		}
		stable = true;
		//return true;
	}

	void storeCells()
	{
		if(generations % 2 == 0)
		{
			for (int i = 0; i < numberOfColumns; i++) 
			{
				for (int j = 0; j < numberOfRows; j++) 
				{
					PVector cellposition = cells[i][j].gridPosition;
					boolean alive = cells[i][j].alive;
					storedCellTwo[i][j] = new Cell(alive, cellposition);
				}
			}
		}
		else if(generations %3 == 0)
		{
			for (int i = 0; i < numberOfColumns; i++) 
			{
				for (int j = 0; j < numberOfRows; j++) 
				{
					PVector cellposition = cells[i][j].gridPosition;
					boolean alive = cells[i][j].alive;
					storedCellThree[i][j] = new Cell(alive, cellposition);
				}
			}
		}
		else 
		{
			for (int i = 0; i < numberOfColumns; i++) 
			{
				for (int j = 0; j < numberOfRows; j++) 
				{
					PVector cellposition = cells[i][j].gridPosition;
					boolean alive = cells[i][j].alive;
					storedCellOne[i][j] = new Cell(alive, cellposition);
				}
			}
		}		
	}

	void displayText()
	{
		fill(255,0,0);
		if(pause)
		{
			textAlign(CENTER);
			textSize(64);
			text ("Paused", width/2,height/10);
		}

		else if(screenCleared)
		{
			return;
		}

		else if(stable)
		{
			textAlign(CENTER);
			textSize(32);
			text("Generations until stable: " + generations, width/2,height/1.5);

			textAlign(LEFT);
			textSize(16);
			text("Current tick time: " + tick, 10, 25);

		}

		else
		{
			textAlign(LEFT);
			textSize(16);
			text("Current generation: " + generations, 10, 25);
			text("Current tick time: " + tick, 10, 50);
		}
	}

	void restart()
	{
		pause = false;
		screenCleared = false;
		generations = 0;
		stable = false;
		tick = 100;
		CreateCells();
	}

	void clearScreen()
	{
		screenCleared = true;
		stable = false;
		pause = true;
		generations = 0;
		for (int i = 0; i < numberOfColumns; i++) 
		{
			for (int j = 0; j < numberOfRows; j++) 
			{
				cells[i][j].alive = false;
			}
		}
	}

	void mousePressed()
	{
		if(pause)
		{
			screenCleared = false;
			stable = false;
			int xCellPressed = (int)Math.floor(mouseX/cellSize);
			int yCellPressed = (int)Math.floor(mouseY/cellSize);

			cells[xCellPressed][yCellPressed].alive = !cells[xCellPressed][yCellPressed].alive;
		}
	}
