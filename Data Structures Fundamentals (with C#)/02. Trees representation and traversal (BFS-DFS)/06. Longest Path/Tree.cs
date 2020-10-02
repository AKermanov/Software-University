namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children = new List<Tree<T>>();
            foreach (var child in children)
            {
                this.AddChild(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            var result = new StringBuilder();
            int depth = 0;
            this.OrderDfsForString(depth, result, this);

            return result.ToString().TrimEnd();
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var leafNodes = this.OrderBf()
                 .Where(node => this.IsLeaf(node));

            int deepestNodeDepth = 0;
            Tree<T> deepestNode = null;

            foreach (var node in leafNodes)
            {
                int currentDepth = this.GetDeepestLeftomostNode(node);
                if (currentDepth > deepestNodeDepth)
                {
                    deepestNodeDepth = currentDepth;
                    deepestNode = node;
                }
            }

            return deepestNode;
        }

      

        public List<T> GetLeafKeys()
        {
            var leafKeys = new List<T>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                if (this.IsLeaf(currentNode))
                {
                    leafKeys.Add(currentNode.Key);
                }

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }
            return leafKeys;
        }

        public List<T> GetMiddleKeys()
        {
            var middleKeys = new List<T>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                if (this.IsMiddle(currentNode))
                {
                    middleKeys.Add(currentNode.Key);
                }

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }
            return middleKeys;
        }

        public List<T> GetLongestPath()
        {
            var deepenstNode = this.GetDeepestLeftomostNode();
            var resultPath = new Stack<T>();
            var currentNode = deepenstNode;

            while (currentNode != null)
            {
                resultPath.Push(currentNode.Key);
                currentNode = currentNode.Parent;
            }

            return new List<T>(resultPath);
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }

        private void OrderDfsForString(int depth, StringBuilder result, Tree<T> subtree)
        {
            //result.Append(new string(' ', depth)).Append(subtree).Append(Environment.NewLine);

            result.AppendLine(new string(' ', depth) + subtree.Key);


            foreach (var child in subtree.Children)
            {
                this.OrderDfsForString(depth + 2, result, child);
            }
        }

        private bool IsLeaf(Tree<T> node)
        {
            return node.Children.Count == 0;
        }
        private bool IsRoot(Tree<T> node)
        {
            return node.Parent == null;
        }
        private bool IsMiddle(Tree<T> node)
        {
            return node.Parent != null && node.Children.Count > 0;
        }

        private List<Tree<T>> OrderBf()
        {
            var middleKeys = new List<Tree<T>>();
            var nodes = new Queue<Tree<T>>();

            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var currentNode = nodes.Dequeue();

                middleKeys.Add(currentNode);

                foreach (var child in currentNode.Children)
                {
                    nodes.Enqueue(child);
                }
            }
            return middleKeys;
        }

        private int GetDeepestLeftomostNode(Tree<T> node)
        {
            int depth = 0;
            Tree<T> current = node;
            while (current.Parent != null)
            {
                depth++;
                current = current.Parent;
            }

            return depth;
        }
    }

}
