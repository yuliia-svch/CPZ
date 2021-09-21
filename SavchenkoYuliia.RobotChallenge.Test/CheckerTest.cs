using Robot.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SavchenkoYuliia.RobotChallenge.Test
{
    [TestClass]
    public class CheckerTest
    {
        [TestMethod]
        public void TestIsProfitableToAttack()
        {
            Robot.Common.Robot robotToAttack = new Robot.Common.Robot()
            { Energy = 500, Position = new Position(5, 6), OwnerName = "Enemy" };
            Robot.Common.Robot currentRobot = new Robot.Common.Robot()
            { Energy = 400, Position = new Position(3, 4), OwnerName = "blabla" };

            bool actual = Checker.IsProfitableToAttack(robotToAttack, currentRobot, 50);

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TestIsCellFree()
        {
            Robot.Common.Robot movingRobot = new Robot.Common.Robot() { Position = new Position(1, 3) };
            Position cell = new Position(4, 3);
            Map map = new Map()
            { MinPozition = new Position(0, 0), MaxPozition = new Position(99, 99) };
            List<Robot.Common.Robot> robots = new List<Robot.Common.Robot>()
            {
                new Robot.Common.Robot() {Position = new Position(4,4) },
                new Robot.Common.Robot() {Position = new Position(4,5) }
            };

            bool result = Checker.IsCellFree(cell, movingRobot, robots);

            Robot.Common.Robot movingRobot2 = new Robot.Common.Robot() { Position = new Position(1, 3) };
            Position cell2 = new Position(1, 3);
            List<Robot.Common.Robot> robots2 = new List<Robot.Common.Robot>()
            {
                new Robot.Common.Robot() {Position = new Position(4,4) },                
            };

            bool result2 = Checker.IsCellFree(cell2, movingRobot2, robots);
            Assert.IsTrue(result);
        }


    }
}
