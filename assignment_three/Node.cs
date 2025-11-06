using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2_stack
{
	/// <summary>
	/// Class that represenets a variable type node
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class Node<T>
	{
		public T Element { get; set; }
		public Node<T>? Next { get; set; }
	    ///public Node<T>? Previous { get; set; }

		/// <summary>
		/// Constructor for Node class.
		/// </summary>
		/// <param name="element"></param>
		/// <param name="previousNode"></param>
		/// <param name="nextNode"></param>
		public Node(T element = default, Node<T> nextNode = null)
		{
			Element = element;
			Next = nextNode;
			///Previous = previousNode;
		}
	}
}
