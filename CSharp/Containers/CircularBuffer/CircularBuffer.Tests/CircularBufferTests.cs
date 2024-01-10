using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CircularBuffer.Tests
{
    [TestClass]
    public class CircularBufferTests
    {
        private const int randomDataLength = 100;

        private static Random rnd;

        [ClassInitialize()]
        public static void Initialize(TestContext testContext)
        {
            rnd = new Random();
        }

        [ClassCleanup()]
        public static void Cleanup()
        {
        }

        protected static byte[] GenerateRandomData(int length)
        {
            var data = new byte[length];
            rnd.NextBytes(data);
            return data;
        }

        private TestContext testContext;

        public CircularBufferTests()
        {
        }
        
        public TestContext TestContext
        {
            get
            {
                return testContext;
            }
            set
            {
                testContext = value;
            }
        }
        
        [TestInitialize()]
        public void TestInitialize()
        {
        }

        [TestCleanup()]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void EmptyBuffer()
        {
            var data = GenerateRandomData(randomDataLength);
            var buffer = new CircularBuffer<byte>(data.Length);
            buffer.Put(data);

            var retBuffer = new byte[buffer.Size];
            buffer.Get(retBuffer);
            CollectionAssert.AreEqual(data, retBuffer);
            Assert.IsTrue(buffer.Size == 0);
        }

        [TestMethod]
        public void FillBuffer()
        {
            var data = GenerateRandomData(randomDataLength);
            var buffer = new CircularBuffer<byte>(data.Length);
            buffer.Put(data);

            CollectionAssert.AreEqual(data, buffer.ToArray());
        }

        [TestMethod]
        public void WrapAroundBuffer()
        {
            var data = GenerateRandomData(randomDataLength);
            var buffer = new CircularBuffer<byte>(data.Length, true);
            buffer.Put(GenerateRandomData(randomDataLength / 2));
            buffer.Put(data);

            buffer.Skip(randomDataLength / 2);
            CollectionAssert.AreEqual(data, buffer.ToArray());
        }
    }
}
