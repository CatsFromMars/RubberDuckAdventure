using System;
using System.Collections.Generic;

public class CommandCenter 
{
	private int areaWidth;
	private int areaHeight;
	private List<Robot> robots;

	public CommandCenter() 
	{
		robots = new List<Robot>();
	}

	public void setSize(int x, int y) 
	{
		areaWidth = x;
		areaHeight = y;
	}

	public void startRobot(int x, int y, char facing) 
	{
		robots.Add(new Robot(x, y, facing));
	}

	public bool isLegalMove(int[] position)
	{
		if ((position[0] < 0) || (position[0] >= areaWidth) ||
		   (position[1] < 0) || (position[1] >= areaHeight))
		{
			return false;
		}

		return true;
	}

	/* Moves most recently added robot and return its end postion */
	public int[] moveRobot(string actions) 
	{
		Robot robot = robots[robots.Count - 1];

		foreach (char c in actions) 
		{
			if ((c == 'L') || (c == 'R'))
			{
				robot.turn(c);
			}
			else if (c == 'M')
			{
				int[] nextPos = robot.findNextPos();
				if (isLegalMove(nextPos))
				{
					robot.move(nextPos[0], nextPos[1]);
				}
				else
				{
					Console.WriteLine(String.Format("Invalid move to {0},{1}", 
									  nextPos[0], nextPos[1]));
				}
			}
			else
			{
				Console.WriteLine(String.Format("Unknown command '{0}'", c));
			}
		}

		return robot.getLocation();
	}

	public void Main()
	{
		Console.WriteLine("5 5");  
		char robotFinalFacing = 'D';
		int[] robotFinalPosition = new int[] {-1,-1};
		bool arenaValid = false;
		bool robotValid = false;
		bool actionsValid = false;
		bool validChars = true;

		while (!arenaValid)
		{
			Console.WriteLine("Yo");
			Console.WriteLine("Enter plateau dimentions (e.g. '5 5') : ");
			string arena = Console.ReadLine();
			if (arena == null)
			{
				Console.WriteLine("Yo");
			}
			string[] arenaValues =  arena.Split(' ');
			int arenaX = 0;
			int arenaY = 0;

			try
			{
				arenaX = Convert.ToInt32(arenaValues[0]);
				arenaY = Convert.ToInt32(arenaValues[1]);
			}
			catch (FormatException e)
			{
				Console.WriteLine("Non-digit characters found - retry");
			}
			catch (OverflowException e)
			{
				Console.WriteLine("Integer too large for Int32 - retry");
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine("Too few arguments supplied - retry");
			}
			finally
			{
				if ((arenaX > 0) && (arenaY > 0))
				{
					setSize(arenaX, arenaY);
					arenaValid = true;
				}
				else
				{
					Console.WriteLine("Dimentions invalid - retry");
				}
			}
		}

		while (!robotValid)
		{
			Console.WriteLine("Enter robot parameters (e.g. '1 2 N') : ");
			string robotParams = Console.ReadLine();
			string[] robotValues = robotParams.Split(' ');
			int robotX = 0;
			int robotY = 0;
			char robotFacing = 'N';

			try
			{
				robotX = Convert.ToInt32(robotValues[0]);
				robotY = Convert.ToInt32(robotValues[1]);
				robotFacing = Convert.ToChar(robotValues[2]);
			}
			catch (ArgumentNullException e)
			{
				Console.WriteLine("No direction given - retry");
			}
			catch (FormatException e)
			{
				Console.WriteLine("Too many characters found - retry");
			}
			catch (OverflowException e)
			{
				Console.WriteLine("Integer too large for Int32 - retry");
			}
			catch (IndexOutOfRangeException e)
			{
				Console.WriteLine("Too few arguments supplied - retry");
			}
			finally
			{
				if ((robotX > 0) && (robotY > 0))
				{
					startRobot(robotX, robotY, robotFacing);
					robotValid = true;
				}
				else
				{
					Console.WriteLine("Robot location invalid - retry");
				}
			}
		}

		while (!actionsValid)
		{
			Console.WriteLine("Enter actions (e.g. 'LRMLRLRMM') : ");
			string actions = Console.ReadLine();

			if (actions == null)
			{
				Console.WriteLine("No actions given - retry");
				continue;
			}

			foreach (char c in actions)
			{
				if ((c != 'L') || (c != 'R') || (c != 'M'))
				{
					Console.WriteLine("Invalid characters found - retry");
					validChars = false;
					break;
				}
			}

			if (validChars)
			{
				robotFinalPosition = moveRobot(actions);
				actionsValid = true;
			}
		}

		switch (robotFinalPosition[2])
		{
			case 0:
				robotFinalFacing = 'N';
				break;
			case 1:
				robotFinalFacing = 'E';
				break;
			case 2:
				robotFinalFacing = 'S';
				break;
			case 3:
				robotFinalFacing = 'W';
				break;
			default:
				Console.WriteLine("Error - Robot facing direction invalid");
				break;
		}

		Console.WriteLine(String.Format("{0} {1} {2}", robotFinalPosition[0], 
		                  robotFinalPosition[1], robotFinalFacing));
	}
}