using NUnit.Framework;

namespace GrammarValidation.UnitTests
{
    [SetUpFixture]
    public class CaplSetupFixture
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // TODO: Add code here that is run before
            //  all tests in the assembly are run            
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // TODO: Add code here that is run after
            //  all tests in the assembly have been run
        }
    }
}