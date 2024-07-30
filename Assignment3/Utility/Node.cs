using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment3
{
    public class Node
    {
        // Attributes
        private User _data;
        private Node _next;

        public User Data { get { return _data; } set { _data = value; } }
        public Node Next { get { return _next; } set { _next = value; } }

        // Constructors
        public Node(User data)
        {
            this._data = data;
            this._next = null;
        }

        public Node(User data, Node next)
        {
            this._data = data;
            this._next = next;
        }
    }
}
