using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static Dictionary<int, TreeValue> pathes;
        static List<TreeNode<TreeValue>> minPath;

        static TreeValue[,] array;
        static int pathNo;
        /// <summary>
        /// реализуем IComparable,для поисков max min
        /// </summary>
        private struct TreeValue:IComparable<TreeValue>
        {
            public int op;
            public int workshop;
            public int weight;

            public TreeValue(int op, int workshop, int weight)
            {
                this.op = op;
                this.workshop = workshop;
                this.weight = weight;
            }

            public int CompareTo(TreeValue other)
            {
                if (this.weight>other.weight)
                    return 1;
                if (this.weight < other.weight)
                    return -1;
                else
                    return 0;
            }

            public override string ToString()
            {
                return $"op:{op},wshp:{workshop},weight:{weight}";
            }
        }

        public static void Main(string[] args)
        {
            minPath = new List<TreeNode<TreeValue>>();

            pathes = new Dictionary<int, TreeValue>();
            pathNo = 0;
            array = new TreeValue[10, 10];
            var tree = new TreeNodeCollection<TreeValue>();
            TreeNode<TreeValue> nodeRoot = new TreeNode<TreeValue>(new TreeValue(0, 0, 0));
            tree.Add(nodeRoot);

            //Основной узел 1_2
            TreeNode<TreeValue> node12 = new TreeNode<TreeValue>(new TreeValue(1, 2, 2));
            nodeRoot.Children.Add(node12);
            TreeNode<TreeValue> node12_21 = new TreeNode<TreeValue>(new TreeValue(2, 1, 1));
            node12.Children.Add(node12_21);
            TreeNode<TreeValue> node12_21_32 = new TreeNode<TreeValue>(new TreeValue(3, 2, 4));
            node12_21.Children.Add(node12_21_32);
            TreeNode<TreeValue> node12_21_31 = new TreeNode<TreeValue>(new TreeValue(3, 1, 10));
            node12_21.Children.Add(node12_21_31);
            TreeNode<TreeValue> node12_22 = new TreeNode<TreeValue>(new TreeValue(2, 2, 2));
            node12.Children.Add(node12_22);
            TreeNode<TreeValue> node12_22_32 = new TreeNode<TreeValue>(new TreeValue(3, 2, 1));
            node12_22.Children.Add(node12_22_32);
            TreeNode<TreeValue> node12_22_31 = new TreeNode<TreeValue>(new TreeValue(3, 1, 1000));
            node12_22.Children.Add(node12_22_31);
            //Основной узел 1_1
            TreeNode<TreeValue> node11 = new TreeNode<TreeValue>(new TreeValue(1, 1, 1));
            nodeRoot.Children.Add(node11);
            TreeNode<TreeValue> node11_21 = new TreeNode<TreeValue>(new TreeValue(2, 1, 4));
            node11.Children.Add(node11_21);
            TreeNode<TreeValue> node11_21_32 = new TreeNode<TreeValue>(new TreeValue(3, 2, 1));
            node11_21.Children.Add(node11_21_32);
            TreeNode<TreeValue> node11_21_31 = new TreeNode<TreeValue>(new TreeValue(3, 1, 1000));
            node11_21.Children.Add(node11_21_31);
            TreeNode<TreeValue> node11_22 = new TreeNode<TreeValue>(new TreeValue(2, 2, 2));
            node11.Children.Add(node11_22);
            TreeNode<TreeValue> node11_22_32 = new TreeNode<TreeValue>(new TreeValue(3, 2, 5));
            node11_22.Children.Add(node11_22_32);
            TreeNode<TreeValue> node11_22_31 = new TreeNode<TreeValue>(new TreeValue(3, 1, 1000));
            node11_22.Children.Add(node11_22_31);

            //дополнить матрицу, копированием пустых столбцов с предыдущей строки
            //TraverseNode(tree[0],0);

            //Рекурсивное получение пути
            //в стеке путей, минимальный путь будет от конца стека до родителя
            GetMinBranch(tree[0], 0);

            Console.ReadLine();
        }//main

        /// <summary>
        /// Делает обход по дереву по пути заполняя матрицу путей
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level"></param>
        static void TraverseNode(TreeNode<TreeValue> node,int level)
        {
            array[pathNo,level] = node.Value;
 
            if (node.Parent != null)
            {
                Console.WriteLine($"node:{node.Value} level:{level} - parent:{node.Parent.Value}");
            }
            else
            {
                Console.WriteLine($"node:{node.Value} level:{level}");
            }

            level++;
            foreach (var leaf in node.Children)
            {
                TraverseNode(leaf,level);
                if (leaf.Children.Count == 0)
                    pathNo++;
            }
        }

        /// <summary>
        /// Рекурсивно находит минимальный путь
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        static TreeNode<TreeValue> GetMinBranch(TreeNode<TreeValue> node, int level)
        {
            TreeNode<TreeValue> minBranch = null;
            int minWeight = node.Value.weight;
            int prevMinWeight = int.MaxValue;
            level++;
            foreach (var leaf in node.Children)
            {
                var gettedMinBranch = GetMinBranch(leaf, level);
                int childMinWeight = leaf.Value.weight;
                if (gettedMinBranch != null)
                {
                    childMinWeight += gettedMinBranch.Value.weight;
                }
                if (prevMinWeight >= childMinWeight)
                {
                    prevMinWeight = childMinWeight;
                    minBranch = leaf;
                    //начинаем процедуру удаления мусора в стеке пути
                    //найдем все листья кроме текущего
                    var exlcudeList = minPath.Where(x => x.AllParents.Contains(leaf.PrevNode)).ToList();
                    minPath = minPath.Except(exlcudeList).ToList();

                }
                else 
                {
                    var exlcudeList = minPath.Where(x => x.AllParents.Contains(leaf)).ToList();
                    minPath = minPath.Except(exlcudeList).ToList();
                }

            }
            if (minBranch == null)
            {
                return null;
            }
            else
            {
                minPath.Add(minBranch);
            }


            return minBranch;
        }


    }
}
