using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace UnitTest
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestMethod1()
        {
            var d = new Dictionary<int, int>();
            d.Add(1, 2);
            d.Add(2, 3);
            Console.WriteLine(X.Core.Utility.Serialize.ToJson(d));
        }
        [TestMethod]
        public void TestMethod2()
        {
            var d = X.Core.Utility.Serialize.FromJson<Dictionary<int, int>>("{\"1\":2,\"2\":2,\"4\":0,\"5\":2,\"6\":2,\"7\":2,\"8\":2,\"9\":2,\"10\":2,\"11\":2,\"12\":2,\"13\":2,\"14\":2,\"15\":2,\"16\":2,\"17\":2,\"18\":2,\"19\":2,\"20\":2,\"21\":2,\"22\":2,\"24\":2}");
        }

    }
}
