using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace System.Collections.Generic
{
	public class TreeNodeCollection<T> : IList<TreeNode<T>>
	{
        private List<TreeNode<T>> _nodes = new List<TreeNode<T>>();

        public TreeNode<T> Parent { get; internal set; }

        public int Level => Parent?.Level + 1 ?? 0;

        /// <summary>
        /// Возвращает общее число TreeNode объектов в коллекции.
        /// </summary>
		public int Count => _nodes.Count;

        /// <summary>
        /// Возвращает значение, указывающее, является ли коллекция доступной только для чтения.
        /// </summary>
		public bool IsReadOnly { get; } = false;

        /// <summary>
        /// Возвращает или задает TreeNode по указанному индексу в коллекции.
        /// </summary>
        /// <param name="index">Индекс элемента в дереве</param>
        /// <returns>Элемент, расположенный по данному индексу</returns>
		public TreeNode<T> this[int index]
        {
            get
            {
                return _nodes[index];
            }

            set
            {
                _nodes[index].SetParent(null);
                value.SetParent(Parent);
                value.Index = index;
                _nodes[index] = value;
            }
        }

        /// <summary>
        /// Создает узел дерева с указанным текстом и вставляет его по указанному индексу.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
		public void Insert(int index, TreeNode<T> value)
        {
            value.Parent?.Children.Remove(value);
            value.SetParent(Parent);
            value.Index = index;
            _nodes.Skip(index).ForEach((x, i) => x.Index++);
            _nodes.Insert(index, value);
        }

        /// <summary>
        /// Добавляет новый узел дерева в конец текущей коллекции узлов дерева.
        /// </summary>
        /// <param name="value"></param>
		public void Add(TreeNode<T> value)
        {
            value.Parent?.Children.Remove(value);
            value.SetParent(Parent);
            value.Index = _nodes.Count;
            _nodes.Add(value);
        }

        /// <summary>
        /// Удаляет указанный узел дерева из коллекции узлов дерева.
        /// </summary>
        /// <param name="index"></param>
		public bool Remove(TreeNode<T> value)
        {
            var index = _nodes.IndexOf(value);

            if (index == -1)
                return false;

            RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Удаляет узел дерева из коллекции узлов дерева с заданным индексом.
        /// </summary>
        /// <param name="index"></param>
		public void RemoveAt(int index)
        {
            _nodes[index].SetParent(null);
            _nodes.Skip(index + 1).ForEach((x, i) => x.Index--);
            _nodes.RemoveAt(index);
        }

        /// <summary>
        /// Удаляет все узлы дерева из коллекции.
        /// </summary>
		public void Clear()
        {
            _nodes.ForEach(x => x.SetParent(null));
            _nodes.Clear();
        }

        /// <summary>
        /// Возвращает перечислитель, который может использоваться для итерации по коллекции узлов дерева.
        /// </summary>
        /// <returns></returns>
		public IEnumerator<TreeNode<T>> GetEnumerator() => _nodes.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _nodes.GetEnumerator();

        /// <summary>
        /// Возвращает индекс указанного узла дерева в коллекции.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(TreeNode<T> value) => _nodes.IndexOf(value);

        /// <summary>
        /// Определяет, является ли указанный узел дерева членом коллекции.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
		public bool Contains(TreeNode<T> value) => _nodes.Contains(value);

        /// <summary>
        /// Копирует всю коллекцию в массив, существующие в указанной позиции в массиве.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
		public void CopyTo(TreeNode<T>[] array, int index) => _nodes.CopyTo(array, index);
    }
}
