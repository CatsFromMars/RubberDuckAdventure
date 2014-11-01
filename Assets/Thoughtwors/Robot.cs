//inherit directions and move turn procedures from RobotNavigator 
public class Robot : Navigation
{
	public int xPos;
	public int yPos;
	public new enum Directions{N,E,S,W};
	public Directions facing;

	public Robot(int x, int y, char dir) 
	{
		xPos = x;
		yPos = y;

		switch (dir)
		{
			case 'N':
				facing = Directions.N;
				break;
			case 'E':
				facing = Directions.E;
				break;
			case 'S':
				facing = Directions.S;
				break;
			case 'W':
				facing = Directions.W;
				break;
			default:
				throw new System.ArgumentException("Invalid robot direction");
		}
	}

	public int[] getLocation()
	{
		return new int[3] {xPos, yPos, (int)facing};
	}

	public void turn(char dir) 
	{
		if (dir == 'L') 
		{
			if (facing != Directions.N) 
			{
				facing -= 1;
			}
			else 
			{
				facing = Directions.W;
			}
		}
		else {
			if (facing != Directions.W) 
			{
				facing += 1;
			}
			else 
			{
				facing = Directions.N;
			}
		}

	}

	public void move(int x, int y)
	{
		xPos = x;
		yPos = y;
	}
	
	/* Where the robot will end up if it moves */
	public int[] findNextPos()
	{
		int resultX = xPos;
		int resultY = yPos;

		switch (facing)
		{
			case Directions.N:
				resultY += 1;
				break;
			case Directions.E:
				resultX += 1;
				break;
			case Directions.S:
				resultY -= 1;
				break;
			case Directions.W:
				resultX -= 1;
				break;
			default:
				throw new System.ArgumentException("Invalid robot direction");
		}

		return new int[2] {resultX, resultY};
	}
}