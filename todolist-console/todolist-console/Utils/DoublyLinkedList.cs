using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace todolist_console.Utils
{
    [Serializable]
    public class DoublyLinkedList<T> : ICollection<T>
    {
        public DoublyLinkedList() { }

        public class Node
        {
            public T Value { get; }
            public Node Next { get; set; }
            public Node Previous { get; set; }

            public Node(T value)
            {
                Value = value;
            }
        }

        private Node head;
        private int count;

        public Node Head
        {
            get { return head; }
        }

        public int Count
        {
            get { return count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool IsEmpty
        {
            get { return count == 0; }
        }

        public void Add(T value)
        {
            Node newNode = new Node(value);
            if (head == null)
            {
                head = newNode;
                head.Next = head;
                head.Previous = head;
            }
            else
            {
                Node lastNode = head.Previous;
                newNode.Next = head;
                newNode.Previous = lastNode;
                lastNode.Next = newNode;
                head.Previous = newNode;
            }
            count++;
        }

        public void Clear()
        {
            head = null;
            count = 0;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (array.Length - arrayIndex < count)
                throw new ArgumentException("The destination array is not large enough.");

            Node currentNode = head;
            if (currentNode == null)
                return;

            int index = arrayIndex;
            do
            {
                array[index] = currentNode.Value;
                currentNode = currentNode.Next;
                index++;
            } while (currentNode != head);
        }

        public void Remove(T value)
        {
            if (head == null)
                return;

            Node currentNode = head;
            do
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Value, value))
                {
                    if (count == 1)
                    {
                        head = null;
                    }
                    else
                    {
                        currentNode.Previous.Next = currentNode.Next;
                        currentNode.Next.Previous = currentNode.Previous;
                        if (currentNode == head)
                        {
                            head = currentNode.Next;
                        }
                    }
                    count--;
                    return;
                }
                currentNode = currentNode.Next;
            } while (currentNode != head);
        }
        bool ICollection<T>.Remove(T item)
        {
            if (head == null)
                return false;

            Node currentNode = head;
            do
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Value, item))
                {
                    if (count == 1)
                    {
                        head = null;
                    }
                    else
                    {
                        currentNode.Previous.Next = currentNode.Next;
                        currentNode.Next.Previous = currentNode.Previous;
                        if (currentNode == head)
                        {
                            head = currentNode.Next;
                        }
                    }
                    count--;
                    return true;
                }
                currentNode = currentNode.Next;
            } while (currentNode != head);

            return false;
        }

        public bool Contains(T value)
        {
            if (head == null)
                return false;

            Node currentNode = head;
            do
            {
                if (EqualityComparer<T>.Default.Equals(currentNode.Value, value))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            } while (currentNode != head);

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (head == null)
                yield break;

            Node currentNode = head;
            do
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            } while (currentNode != head);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
