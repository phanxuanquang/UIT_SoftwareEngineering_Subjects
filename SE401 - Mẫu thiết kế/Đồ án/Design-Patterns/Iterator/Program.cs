using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.AddNode(1);
            tree.AddNode(2);
            tree.AddNode(3);
            tree.AddNode(4);
            tree.AddNode(5);
            tree.AddNode(6);
            tree.AddNode(7);

            Console.WriteLine("Depth-First Search:");
            IEnumerator<int> dfsIterator = tree.GetEnumerator();
            while (dfsIterator.MoveNext())
            {
                int value = dfsIterator.Current;
                Console.WriteLine(value);
            }

            Console.WriteLine("Breadth-First Search:");
            BreadthFirstIterator<int> bfsIterator = new BreadthFirstIterator<int>(tree.Root);
            while (bfsIterator.MoveNext())
            {
                int value = bfsIterator.Current;
                Console.WriteLine(value);
            }
        }

        public class DepthFirstIterator<T> : IEnumerator<T>
        {
            private Stack<Node<T>> stack;

            public T Current { get; private set; }

            object IEnumerator.Current => Current;

            public DepthFirstIterator(Node<T> root)
            {
                stack = new Stack<Node<T>>();
                stack.Push(root);
                Current = default(T);
            }

            public bool MoveNext()
            {
                if (stack.Count == 0)
                {
                    return false;
                }

                Node<T> current = stack.Pop();
                Current = current.Value;

                if (current.Right != null)
                {
                    stack.Push(current.Right);
                }

                if (current.Left != null)
                {
                    stack.Push(current.Left);
                }

                return true;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public void Dispose()
            {
                // Dispose resources if needed
            }
        }

        public class BreadthFirstIterator<T> : IEnumerator<T>
        {
            private Queue<Node<T>> queue;

            public T Current { get; private set; }

            object IEnumerator.Current => Current;

            public BreadthFirstIterator(Node<T> root)
            {
                queue = new Queue<Node<T>>();
                queue.Enqueue(root);
                Current = default(T);
            }

            public bool MoveNext()
            {
                if (queue.Count == 0)
                {
                    return false;
                }

                Node<T> current = queue.Dequeue();
                Current = current.Value;

                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }

                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }

                return true;
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }

            public void Dispose()
            {
                // Dispose resources if needed
            }
        }

        public class Node<T>
        {
            public T Value { get; set; }
            public Node<T> Left { get; set; }
            public Node<T> Right { get; set; }

            public Node(T node)
            {
                Value = node;
                Left = null;
                Right = null;
            }
        }

        public class BinaryTree<T> : IEnumerable<T>
        {
            public Node<T> Root { get; private set; }

            public BinaryTree()
            {
                Root = null;
            }

            public void AddNode(T value)
            {
                Node<T> newNode = new Node<T>(value);

                if (Root == null)
                {
                    Root = newNode;
                }
                else
                {
                    Queue<Node<T>> queue = new Queue<Node<T>>();
                    queue.Enqueue(Root);

                    while (queue.Count > 0)
                    {
                        Node<T> current = queue.Dequeue();

                        if (current.Left == null)
                        {
                            current.Left = newNode;
                            break;
                        }
                        else if (current.Right == null)
                        {
                            current.Right = newNode;
                            break;
                        }
                        else
                        {
                            queue.Enqueue(current.Left);
                            queue.Enqueue(current.Right);
                        }
                    }
                }
            }

            public IEnumerator<T> GetEnumerator()
            {
                return new DepthFirstIterator<T>(Root);
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return new DepthFirstIterator<T>(Root);
            }
        }
    }
}
