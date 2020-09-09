void setup()
{
	size(1000, 1000);
	strokeWeight(3);

}  

void draw()
{
	background(255, 0, 0);
	int numberOfLines = 40;
	int offset = width/numberOfLines;

	for (int i=0; i < width; i += offset)
	{
		if(i % 3 == 0 )
		{
			stroke(0,0,0);	
		}
		line(0,i,i,height);
		stroke(100,100,100);
	}
}