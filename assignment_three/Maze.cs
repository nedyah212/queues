using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_3
{
	public class Maze
	{
		public Point StartingPoint { get; set; }
		public int RowLength { get => CharMaze.Length; }
		public int ColumnLength { get => CharMaze[0].Length; }

		private char[][] CharMaze;
		private Stack<Point> Path;

		private string TestMessage = "No exit found in maze!\n\n";
		private bool ExitStatus = false;
		private bool HasSearched = false;

		public Maze(string filename)
		{
			string[] fileLines = File.ReadAllLines(filename);

			string[] dimensions = fileLines[0].Split(' ');
			int rows = int.Parse(dimensions[0]);

			string[] startPosition = fileLines[1].Split(' ');
			int startRow = int.Parse(startPosition[0]);
			int startColumn = int.Parse(startPosition[1]);

			CharMaze = new char[rows][];

			for (int i = 0; i < RowLength; i++)
			{
				CharMaze[i] = fileLines[i + 2].ToCharArray();
			}

			StartingPoint = new Point(startRow, startColumn);
			Path = new Stack<Point>();
		}

		public Maze(int startingRow, int startingColumn, char[][] existingMaze)
		{
			CharMaze = existingMaze;

			if (CharMaze[startingRow][startingColumn] == 'W' ||
				CharMaze[startingRow][startingColumn] == 'E')
				throw new ApplicationException();

			if (startingColumn >= ColumnLength || startingColumn < 0)
				throw new IndexOutOfRangeException();

			StartingPoint = new Point(startingRow, startingColumn);
			Path = new Stack<Point>();
		}

		public char[][] GetMaze() => CharMaze;

		public string PrintMaze()
		{
			string maze = "";
			for (int r = 0; r < RowLength; r++)
			{
				for (int c = 0; c < ColumnLength; c++)
					maze += CharMaze[r][c];

				if (r < RowLength - 1)
					maze += "\n";
			}
			return maze;
		}

		public string BreadthFirstSearch()
		{
			HasSearched = true;
			Queue<Point> queue = new Queue<Point>();
			Point exitPoint = null;

			CharMaze[StartingPoint.Row][StartingPoint.Column] = 'V';
			StartingPoint.Parent = null;
			queue.Enqueue(StartingPoint);

			while (!queue.IsEmpty() && !ExitStatus)
			{
				Point currentPosition = queue.Dequeue();

				Point[] directions = new Point[]
{
					new Point(currentPosition.Row + 1, currentPosition.Column),
					new Point(currentPosition.Row, currentPosition.Column + 1),   
					new Point(currentPosition.Row, currentPosition.Column - 1),   
					new Point(currentPosition.Row - 1, currentPosition.Column)     
				};

				foreach (Point child in directions)
				{
					char cellChar = CharMaze[child.Row][child.Column];

					if (cellChar == 'E')
					{
						ExitStatus = true;
						child.Parent = currentPosition;
						exitPoint = child;
						break;
					}
					if (cellChar == ' ')
					{
						CharMaze[child.Row][child.Column] = 'V';
						child.Parent = currentPosition;
						queue.Enqueue(child);
					}
				}
			}
			return BuildResults(exitPoint);
		}

		private string BuildResults(Point exitPoint)
		{
			if (ExitStatus && exitPoint != null)
			{
				List<Point> pathList = new List<Point>();
				Point current = exitPoint;

				while (current != null)
				{
					pathList.Add(current);
					current = current.Parent;
				}

				Path = new Stack<Point>();
				foreach (Point point in pathList)
				{
					Path.Push(point);
				}

				pathList.Reverse();

				foreach (Point point in pathList)
				{
					if (CharMaze[point.Row][point.Column] != 'E')
						CharMaze[point.Row][point.Column] = '.';
				}

				string result = $"Path to follow from Start {StartingPoint.ToString()} to Exit {exitPoint.ToString()} - {pathList.Count} steps:\n";
				foreach (Point point in pathList)
				{
					result += $"{point.ToString()}\n";
				}
				result += PrintMaze();
				return result;
			}
			else
			{
				Path = new Stack<Point>();
				return "No exit found in maze!\n\n" + PrintMaze();
			}
		}
		public Stack<Point> GetPathToFollow()
		{
			if (!HasSearched)
				throw new ApplicationException();

			if (Path.IsEmpty())
				return new Stack<Point>();

			return Path.Copy(Path.Size);
		}
	}
}