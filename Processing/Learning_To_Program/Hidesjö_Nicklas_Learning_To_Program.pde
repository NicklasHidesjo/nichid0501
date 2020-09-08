float angle;

ParabolicCurveCreator curveCreator;

void setup()
{
	size(600,600);
	strokeWeight(3);
	angle = 90;

	int	numberOfLines = 20;
	PVector axis1 = new PVector(height,0);
	PVector axis2 = new PVector(height,0).rotate(radians(angle));

	background(50,166,240);
	curveCreator = new ParabolicCurveCreator(axis1,axis2,numberOfLines);
	curveCreator.DrawParabole();
}	

public class ParabolicCurveCreator
{
	PVector axis1, axis2;
	int numberOfLines;

	public ParabolicCurveCreator (PVector axis1, PVector axis2, int numberOfLines) 
	{
		this.axis1 = axis1;
		this.axis2 = axis2;
		this.numberOfLines = numberOfLines;
	}


	void DrawParabole()
	{
		PVector zero = new PVector(0,0);

  	for(int i = 1; i < numberOfLines; i++)
  	{
  		if(i % 3 == 0 )
  		{
  			stroke(0,0,0);	
  		}
  		else 
  		{
  			stroke(100,100,100);
  		}

  		PVector ax1 = PVector.lerp(axis1, zero, 1f / numberOfLines * i);
  		PVector ax2 = PVector.lerp(axis2, zero, 1-1f/numberOfLines *(i+1));

  		line(ax1.x,ax1.y,ax2.x,ax2.y);
  	}
  }

}
