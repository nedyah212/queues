using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace assignment_3
{
	public class Queue<T>
	{
		public int Size { get; set; }
		public Node<T> Head { get; set; }
		public Node<T> Tail { get; set; }

		public Queue() => Clear();
		
		public void Enqueue(T element)
		{
			Node<T> node = new Node<T>(element);

			if (Size == 0)
			{
				Head = node;
				Tail = node;
			}
			else
			{
				Tail.Next = node;
				Tail = node;
			}
			Size++;
		}

		public T Front()
		{
			if (IsEmpty())
				throw new ApplicationException();

			return Head.Element;
		}

		public T Dequeue()
		{
			if (IsEmpty())
				throw new ApplicationException();

			Node<T> oldHead = Head;
			Head = Head.Next;
			Size--;

			if (IsEmpty())
				Tail = null;

			return oldHead.Element;
		}

		public void Clear()
		{
			Size = 0;
			Head = null;
			Tail = null;
		}

		public bool IsEmpty()
		{
			return Size == 0;
		}
	}
}
