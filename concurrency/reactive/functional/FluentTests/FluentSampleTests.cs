﻿using FluentAssertions;

namespace FluentTests;
/*
[TestClass]
public class FluentSampleTests
{
   [TestMethod]
   public void NonFluentTest()
   {
      var actual = "ABCDEFGHI";

      Assert.IsTrue(actual.StartsWith("AB"));
      Assert.IsTrue(actual.EndsWith("HI"));
      Assert.IsTrue(actual.Contains("EF"));
      Assert.AreEqual(9, actual.Length);
   }

   [TestMethod]
   public void FluentTest()
   {
      var actual = "ABCDEFGHI";
      actual.Should().StartWith("AB")
         .And.EndWith("HI")
         .And.Contain("EF")
         .And.HaveLength(9);
   }
}*/