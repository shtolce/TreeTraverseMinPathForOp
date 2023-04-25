using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Tests.Data;

namespace Tests.System.Linq
{
    [TestFixture]
    public class EnumerableExtensionsTest
    {
        private readonly DataProvider data = new DataProvider();

        [Test]
        public void ToEnumerableTest()
        {
            var tree = new TreeNodeCollection<string>
            {
                new TreeNode<string>
                {
                    Value = "1",
                    Children =
                    {
                        new TreeNode<string>("1.1"),
                        new TreeNode<string>("1.2")
                    }
                }
            };

            var values = tree.ToEnumerable<string>(x => x.Value).ToArray();

            Assert.AreEqual(3, values.Length);
            Assert.AreEqual("1", values[0]);
            Assert.AreEqual("1.1", values[1]);
            Assert.AreEqual("1.2", values[2]);
        }

        [Test]
        public void ToTreeTest()
        {
            var lines = data.Input.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Func<string, int> getLevel = line => line.TakeWhile(char.IsWhiteSpace).Count();
            Func<string, int, string> itemSelector = (line, level) => new string(line.SkipWhile(char.IsWhiteSpace).ToArray());
            var tree = lines.ToTree(getLevel, itemSelector);

            Assert.AreEqual(4, tree.Count);
            Assert.AreEqual(3, tree[0].Children.Count);
            Assert.AreEqual(2, tree[0].Children[0].Children.Count);
            Assert.AreEqual("1.1.1", tree[0].Children[0].Children[0].Value);
            Assert.AreEqual("3.1", tree[2].Children[0].Value);

            Func<TreeNode<string>, string> nodeToLine = x => new string('\t', x.Level) + x.Value;
            var outputLines = tree.ToEnumerable(nodeToLine).ToArray();
            data.Output = string.Join(Environment.NewLine, outputLines);

            Assert.IsTrue(lines.SequenceEqual(outputLines));
            Assert.AreEqual(data.Input, data.Output);
        }
    }
}
