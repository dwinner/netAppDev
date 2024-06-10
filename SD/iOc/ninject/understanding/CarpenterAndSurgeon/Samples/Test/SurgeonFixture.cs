using Moq;
using NUnit.Framework;

namespace Samples.Test
{
   [TestFixture]
   internal class SurgeonFixture
   {
      [Test]
      public void CallingOperateCallsGrabOnForceps()
      {
         var forcepsMock = new Mock<Forceps>();

         var surgeon = new Surgeon(forcepsMock.Object);
         surgeon.Operate();

         forcepsMock.Verify(f => f.Grab());
      }
   }
}