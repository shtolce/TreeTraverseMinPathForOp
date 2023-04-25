using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;
using Tests.Data;


namespace Tests.System.Collections.Generic
{
	[TestFixture]
	public class TreeNodeCollectionTest
	{
        private DataProvider data = new DataProvider();

	    [Test]
	    public void InsertTest()
	    {
	        var tree = new TreeNodeCollection<int>
	        {
                new TreeNode<int>(0),
                new TreeNode<int>(1),
                new TreeNode<int>(2),
            };

            tree.Insert(2, new TreeNode<int>(10));

	        var isEqual = new[] {0, 1, 10, 2}.SequenceEqual(tree.ToEnumerable(node => node.Value));

            Assert.IsTrue(isEqual);
            Assert.AreEqual(2, tree[2].Index);
            Assert.AreEqual(3, tree[3].Index);
	    }

	    [Test]
	    public void RemoveAtTest()
	    {
            var tree = new TreeNodeCollection<int>
            {
                new TreeNode<int>(0),
                new TreeNode<int>(1),
                new TreeNode<int>(2),
            };

	        tree.RemoveAt(1);

	        var isEqual = new[] {0, 2}.SequenceEqual(tree.ToEnumerable(node => node.Value));

            Assert.IsTrue(isEqual);
            Assert.AreEqual(1, tree[1].Index);
	    }
    }
}
