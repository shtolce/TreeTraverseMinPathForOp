using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class EnumerableExtensions
    {
        private static IEnumerable<TreeNode<T>> GetFlatten<T>(TreeNodeCollection<T> nodes)
        {
            foreach (var node in nodes)
            {
                yield return node;

                foreach (var subnode in GetFlatten(node.Children))
                    yield return subnode;
            }
        }

        /// <summary>
        /// Разворачивает дерево в плоскую последовательность элементов.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="nodes"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<T> ToEnumerable<T>(this TreeNodeCollection<T> nodes, Func<TreeNode<T>, T> selector)
            => GetFlatten(nodes).Select(selector);

        private static TreeNodeCollection<T> GetNodesByLevel<T>(int level, TreeNodeCollection<T> nodes)
        {
            if (nodes.Level == level)
                return nodes;

            if (nodes.Count == 0)
                nodes.Add(new TreeNode<T>());

            var lastNode = nodes[nodes.Count - 1];

            return GetNodesByLevel(level, lastNode.Children);
        }

        /// <summary>
        /// Превращает последовательность элементов в дерево,
        /// применяя переданную функцию getLevel 
        /// для получения уровня вложенности элемента в дереве
        /// и функцию selector для преобразования элемента в нужный тип.
        /// </summary>
        /// <typeparam name="TIn"></typeparam>
        /// <typeparam name="TOut"></typeparam>
        /// <param name="items"></param>
        /// <param name="getLevel"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static TreeNodeCollection<TOut> ToTree<TIn, TOut>(this IEnumerable<TIn> items, Func<TIn, int> getLevel, Func<TIn, int, TOut> selector)
        {
            var result = new TreeNodeCollection<TOut>();

            foreach (var value in items)
            {
                var level = getLevel(value);
                var convertedValue = selector(value, level);
                var node = new TreeNode<TOut>(convertedValue);
                var nodesByLevel = GetNodesByLevel(level, result);

                nodesByLevel?.Add(node);
            }

            return result;
        }

        /// <summary>
        /// Перечисляет элементы с индексами.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<KeyValuePair<int, T>> Enumerate<T>(this IEnumerable<T> items)
        {
            var index = 0;

            foreach (var item in items)
            {
                yield return new KeyValuePair<int, T>(index++, item);
            }
        }

        /// <summary>
        /// Применяет к каждому элементу функцию callback,
        /// передав в нее сам элемент и его индекс.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="callback"></param>
        public static void ForEach<T>(this IEnumerable<T> items, Action<T, int> callback)
        {
            foreach (var pair in items.Enumerate())
            {
                callback(pair.Value, pair.Key);
            }
        }
    }
}
