using CISample.BusinessManager.Logic;
using NSubstitute;
using NUnit.Framework;

namespace CISample.UnitTests
{
    [TestFixture]
    public class MakeCalculationsTests
    {
        [Test]
        public void SumIntegers_Tuple_ReturnsTrue()
        {
            var calculations = new MakeCalculations();

            var result = calculations.SumIntegers(2, 2);

            Assert.AreEqual(4, result);
        }
    }
}
