using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_6
{
    internal class Deque
    {
        public Node? Tail;
        public Node? Head;
        public Deque()
        {
            this.Tail = null;
            this.Head = null;
        }
        public bool insertHead(int data)
        {
            if (Head == null)
            {
                Head = new Node(data);
                Head.Prev = Head.Next = Head;
            }
            else if (Tail == null)
            {
                Tail = Head;
                Head = new Node(data);
                Head.Prev = Head.Next = Tail;
                Tail.Prev = Tail.Next = Head;
            }
            else
            {
                Node toAdd = new Node(data);
                Head.Prev = toAdd;
                Tail.Next = toAdd;
                toAdd.Next = Head;
                toAdd.Prev = Tail;
                Head = toAdd;
            }
            return true;
        }
        public bool removeHead()
        {
            if (Head == null) return false;

            if (Tail == null)
            {
                Head = null;
                return false;
            }

            if (Head.Next == Tail)
            {
                Head = new Node(Tail.data);
                Head.Next = Head.Prev = Head;
                Tail = null;
            }
            else
            {
                Head.Next.Prev = Tail;
                Tail.Next = Head.Next;
                Head = Head.Next;
            }
            return true;
        }
        public bool insertTail(int data)
        {
            if (Head == null)
                insertHead(data);
            else if (Tail == null)
            {
                Tail = new Node(data);
                Tail.Prev = Tail.Next = Head;
                Head.Next = Head.Prev = Tail;
            }
            else
            {
                Node toAdd = new Node(data);
                Head.Prev = toAdd;
                Tail.Next = toAdd;
                toAdd.Next = Head;
                toAdd.Prev = Tail;
                Tail = toAdd;
            }
            return true;
        }
        public bool removeTail()
        {
            if (Head == null) return false;

            if (Tail == null)
                Head = null;

            else if (Tail.Prev == Head)
            {
                Head = new Node(Tail.data);
                Head.Next = Head.Prev = Head;
                Tail = null;
            }
            else
            {
                Tail.Prev.Next = Head;
                Head.Prev = Tail.Prev;
                Tail = Tail.Prev;
            }
            return true;
        }
        public int head()
        {
            return Head.data;
        }
        public int tail()
        {
            return Tail.data;
        }
        public bool isEmpty()
        {
            if(Head == null) return true;
            return false;
        }
        public int size()
        {
            if(isEmpty()) return 0;

            Node temp = Head;
            int size = 1;
            while (temp != Head.Prev)
            {
                temp = temp.Next;
                size++;
            }
            return size;
        }
    }
    internal class Node
    {
        public Node Prev { get; set; }
        public Node Next { get; set; }
        public int data;
        public Node(int data)
        {
            this.data = data;
        }
    }
}
