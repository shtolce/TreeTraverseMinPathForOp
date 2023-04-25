using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace System.Collections.Generic
{
    /// <summary>
	/// Представляет узел в дереве элементов.
	/// </summary>
	public class TreeNode<T>
	{
        /// <summary>
		/// Возвращает значение узла.
		/// </summary>
		/// <value>Значение</value>
		public T Value { get; set; }

        /// <summary>
        /// Возвращает родительский узел дерева для текущего узла дерева.
        /// </summary>
        /// <value>Родитель</value>
        public TreeNode<T> Parent { get; internal set; }

        /// <summary>
        /// Возвращает коллекцию узлов, вложенных в этот узел дерева.
        /// </summary>
        /// <value>Дочерние элементы</value>
        public TreeNodeCollection<T> Children { get; } = new TreeNodeCollection<T>();

        /// <summary>
        /// Возвращает предыдущий одноуровневый элемент узла дерева.
        /// </summary>
        /// <value>Предыдущий узел</value>
        public TreeNode<T> PrevNode => Index - 1 >= 0 ? Parent.Children[Index - 1] : null;

        /// <summary>
        /// Возвращает следующий одноуровневый элемент узла дерева.
        /// </summary>
        /// <value>Следующий узел</value>
        public TreeNode<T> NextNode => Index + 1 < Parent.Children.Count ? Parent.Children[Index + 1] : null;

        /// <summary>
        /// Возвращает значение, указывающее является ли узел корневым.
        /// </summary>
        /// <value><c>true</c> if is root; otherwise, <c>false</c>.</value>
        public bool IsRoot => Parent == null;

        /// <summary>
        /// Возвращает позицию узла в коллекции дочерних элементов родителя.
        /// </summary>
        /// <value>Индекс</value>
        public int Index { get; internal set; }

        /// <summary>
        /// Возвращает отсчитываемую от нуля глубину узла дерева.
        /// </summary>
        /// <value>Глубина</value>
        public int Level { get; private set; }

        /// <summary>
        /// Возвращает всех родителей узла.
        /// </summary>
		public IEnumerable<TreeNode<T>> AllParents
        {
            get
            {
                var currentNode = this;

                while (!currentNode.IsRoot)
                {
                    currentNode = currentNode.Parent;
                    yield return currentNode;
                }
            }
        }

        internal void SetParent(TreeNode<T> node)
        {
            Parent = node;
            Level = node?.Level + 1 ?? 0;
        }

        public TreeNode()
        {
            Children.Parent = this;
        }

        public TreeNode(T value) : this()
        {
            Value = value;
        }
    }
}
