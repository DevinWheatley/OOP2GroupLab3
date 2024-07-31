using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
     public class CannotRemoveException : Exception
    {
        public CannotRemoveException() : base() { }
        public CannotRemoveException(string message) : base(message) { }
        public CannotRemoveException(string message, Exception innerException) : base(message) { }
    }
    public class SLL : ILinkedListADT
    {
        // Attributes
        private Node head;
        private Node tail;
        private int size;

        // Constructor
        public SLL()
        {
            this.head = null;
            this.tail = null;
            size = 0;
        }

        /// <summary>
        /// Checks if the list is empty.
        /// </summary>
        /// <returns>True if it is empty.</returns>
        public bool IsEmpty()
        {
            return size == 0;
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            head = null;
            tail = null;
            size = 0;
        }

        /// <summary>
        /// Adds to the end of the list.
        /// </summary>
        /// <param name="value">Value to append.</param>
        public void AddLast(User value)
        {
            Node newNode = new Node(value);

            // if the list is empty, simply insert the value
            if (this.IsEmpty())
            {
                head = newNode;
                tail = newNode;
            }
            // if the list is populated, insert the value at the end
            else
            {
                tail.Next = newNode;
                tail = newNode;
            }
            // update size of the list
            size++;
        }

        /// <summary>
        /// Prepends (adds to beginning) value to the list.
        /// </summary>
        /// <param name="value">Value to store inside element.</param>
        public void AddFirst(User value)
        {
            Node newNode = new Node(value);

            // if the list is empty, simply insert the value
            if (this.IsEmpty())
            {
                head = newNode;
                tail = newNode;
            }
            // if the list is populated, insert the value at the beginning
            else
            {
                newNode.Next = head;
                head = newNode;
            }
            // update size of the list
            size++;
        }

        /// <summary>
        /// Adds a new element at a specific position.
        /// </summary>
        /// <param name="value">Value that element is to contain.</param>
        /// <param name="index">Index to add new element at.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if index is negative or past the size of the list.</exception>
        public void Add(User value, int index)
        {
            // Check that the index is in range
            // Throws exception if not
            if (index > 0 || index > size)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            Node newNode = new Node(value);

            // Index = 0, will run AddFirst(value)
            // Outcome = Value added to the beginning of the list
            if (index == 0)
            {
                AddFirst(value);
                size++; // update size of the list
            }
            // Index = size, will run AddLast(value)
            // Outcome = Value added to the end of the list
            if (index == size)
            {
                AddLast(value);
                size++; // update size of the list
            }
            else
            {
                var pointer = head; // pointer points to the current node
                for (int i = 0; i < index - 1; i++)
                {
                    pointer = pointer.Next; // brings the pointer to the correct node index
                }
                newNode.Next = pointer.Next; // newNode and pointer point to the same Node
                pointer.Next = newNode; // reroutes the pointer.Netx to the newNode
            }
        }

        /// <summary>
        /// Replaces the value  at index.
        /// </summary>
        /// <param name="value">Value to replace.</param>
        /// <param name="index">Index of element to replace.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if index is negative or larger than size - 1 of list.</exception>
        public void Replace(User value, int index)
        {
            // Check that the index is in range
            // Throws exception if not
            if (index > 0 || index > size)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            Node newNode = new Node(value);
        }

        /// <summary>
        /// Gets the number of elements in the list.
        /// </summary>
        /// <returns>Size of list (0 meaning empty)</returns>
        public int Count()
        {
            return size;
        }

        /// <summary>
        /// Removes first element from list
        /// </summary>
        /// <exception cref="CannotRemoveException">Thrown if list is empty.</exception>
        public void RemoveFirst()
        {   
            // check if the first data is empty, throw a message
            if (this.IsEmpty())
            {
                throw new CannotRemoveException("The list is empty.");
            }
            if (this.head != null)  // if the first node has data, remove it
            {
                this.head = this.head.Next;

                size--; // update size of the list
            }
        }

        /// <summary>
        /// Removes last element from list
        /// </summary>
        /// <exception cref="CannotRemoveException">Thrown if list is empty.</exception>
        public void RemoveLast()
        {
            // if the list is empty, throw a message
            if (this.IsEmpty())
            {
                throw new CannotRemoveException("The list is empty.");
            }

            // if the list has only one node
            if (this.head.Next == null)
            {
                this.head = null; // make head empty
                size--;  // update size of the list
                return;
            }

            Node pointer = this.head;
            // move to the second last node using while loop
            while (pointer.Next.Next != null)
            {
                pointer = pointer.Next; // pointer is the second last node
            }
            pointer.Next = null; // give null to the last node and remove from the list
            size--;
        }

        /// <summary>
        /// Removes element at index from list, reducing the size.
        /// </summary>
        /// <param name="index">Index of element to remove.</param>
        /// <exception cref="IndexOutOfRangeException">Thrown if index is negative or larger than size - 1 of list.</exception>
        public void Remove(int index)
        {
            if (index < 0 || index > size - 1)
            {
                throw new IndexOutOfRangeException("Index out of range");
            }

            if (this.IsEmpty())
            {
                throw new CannotRemoveException("The list is empty.");
            }

            // remove the first element
            if (index == 0)
            {
                RemoveFirst();
                return;
            }

            if (index == size - 1)
            {
                RemoveLast();
                return;
            }
            
            //remove a specific index
            Node current = this.head;
            for (int i = 0; i < index -1; i++)
            {
                current = current.Next;
            }
            current.Next = current.Next.Next;
            size--;
        }

        /// <summary>
        /// Gets the value at the specified index.
        /// </summary>
        /// <param name="index">Index of element to get.</param>
        /// <returns>Value of node at index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown if index is negative or larger than size - 1 of list.</exception>
        public User GetValue(int index)
        {

        }

        /// <summary>
        /// Gets the first index of element containing value.
        /// </summary>
        /// <param name="value">Value to find index of.</param>
        /// <returns>First of index of node with matching value or -1 if not found.</returns>
        public int IndexOf(User value)
        {

        }

        /// <summary>
        /// Go through nodes and check if one has value.
        /// </summary>
        /// <param name="value">Value to find index of.</param>
        /// <returns>True if element exists with value.</returns>
        public bool Contains(User value)
        {

        }
        
        /// <summary>
        /// Go through nodes and reverse the order of the list.
        /// </summary>
        public void Reverse(User value)
        {
            if (size == 0 || size == 1) // If list is empty or only has one Node then there's nothing to reverse
            {
                return;
            }
            
            Node pointerPrevious = null;
            Node pointer = head;
            Node pointerNext = null;

            while (pointer != null)
            {
                pointerNext = pointer.Next; // Save the next Node in pointerNext
                pointer.Next = pointerPrevious; // Redirect the current Node to the Node preceding it
                pointerPrevious = pointer; // Save the current Node in pointerPrevious
                pointer = pointerNext; // Move the pointer to the next Node
            }
            head = pointerPrevious; // Updates Head Node
        }
    }
}
