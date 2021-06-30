using CamLib;
using NUnit.Framework;

namespace Tests.Editor
{
    public class NearlyEqualTests
    {
        [Test]
        public void PositiveTinyFloatingPoint()
        {
            Assert.IsTrue((-1f).IsEqual(-1.0000001f));
        }
        [Test]
        public void NegativeTinyFloatingPoint()
        {
            Assert.IsTrue(1f.IsEqual(1.0000001f));
        }
        
        [Test]
        public void Zero()
        {
            Assert.IsTrue((-1f).IsEqual(-1f));
        }
        [Test]
        public void NegativeOne()
        {
            Assert.IsTrue((-1f).IsEqual(-1f));
        }
        [Test]
        public void NegativeTen()
        {
            Assert.IsTrue((-10f).IsEqual(-10f));
        }        
        [Test]
        public void PositiveOne()
        {
            Assert.IsTrue(1f.IsEqual(1f));
        }
        [Test]
        public void PositiveTen()
        {
            Assert.IsTrue(10f.IsEqual(10f));
        }

    }
}