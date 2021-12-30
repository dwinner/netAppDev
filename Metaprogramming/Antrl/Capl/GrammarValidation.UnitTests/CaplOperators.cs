using System.IO;
using NUnit.Framework;

namespace GrammarValidation.UnitTests
{
    [TestFixture]
    public class CaplOperators
    {
        [Test]
        public void Arithmetic()
        {
            var exists = File.Exists("arithmetic_operators.can");

            // TODO: Add your test code here
            var answer = 42;
            Assert.That(answer, Is.EqualTo(42), "Some useful error message");
        }
    }
}