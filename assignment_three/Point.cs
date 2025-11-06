using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2_stack
{
	public class Point
	{
		public int Row { get; }
		public int Column { get; }
		public Point(int row, int column) => (Row,  Column) = (row, column);
		public override string ToString() => $"[{Row}, {Column}]";
	}
}
