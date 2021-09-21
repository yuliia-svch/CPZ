using Robot.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SavchenkoYuliia.RobotChallenge.Test
{
    [TestClass]
    public class DistanceHelperTest
    {
        [TestMethod]
        public void TestFindDistance()
        {
            Position a = new Position(2, 2);
            Position b = new Position(3, 4);
            Assert.AreEqual(5, DistanceHelper.FindDistance(a, b));
        }
    }
}
