using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace assignment_2_stack
{
	public class Stack<T>
	{	
		public int Size { get; set; }
		public Node<T> Head { get; set; }
		public Stack() => Clear();

		public bool IsEmpty() => Size == 0;
		public void Clear() => (Size, Head) = (0, null);
		
		public void Push(T element) 
		{
			Node<T> newHead = new Node<T>(element,Head);
			Head = newHead;
			Size++;
		}

		public T Top()
		{
			if (IsEmpty())
				throw new ApplicationException();
		
			return Head.Element;
		}

		public T Pop()
		{
			T element = Top();
			Head = Head.Next;
			Size--;
			return element;
		}

		public Stack<T> Copy(int pathSize, bool reversed = false)
		{	

			Stack<T> temp = new Stack<T>();
			Stack<T> copy = new Stack<T>();

			for (int i = 0; i < pathSize; i++)
			{
				temp.Push(Pop());
			}

			for (int i = 0; i < pathSize; i++)
			{
				T point = temp.Pop();
				Push(point);
				copy.Push(point);
			}

			if (reversed)
			{
				for (int i = 0; i < pathSize; i++)
				{
					temp.Push(copy.Pop());
				}
				return temp;
			}

			return copy;
		}	
}	}
