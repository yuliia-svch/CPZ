using Robot.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace SavchenkoYuliia.RobotChallenge.Test
{
    [TestClass]
    public class FinderTest
    {
        [TestMethod]
        public void TestFindStationPosition()
        {
            Map map = new Map()
            { MinPozition = new Position(0, 0), MaxPozition = new Position(99, 99) };
            EnergyStation energyStation = new EnergyStation() { Position = new Position(14, 22) };
            map.Stations.Add(energyStation);

            EnergyStation result = Finder.FindStationByPosition(map, new Position(14, 22));

            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void TestFindAvailablePositionToGo()
        {
            Robot.Common.Robot currentRobot = new Robot.Common.Robot()
            { Energy = 40, Position = new Position(2, 4), OwnerName = "blabla" };
            Position positionToGo = new Position(6, 10);
            List<Robot.Common.Robot> robots = new List<Robot.Common.Robot>()
            {
                new Robot.Common.Robot() {Position = new Position(4,8) },
                new Robot.Common.Robot() {Position = new Position(4,5) },
                new Robot.Common.Robot() {Position = new Position(5,5) }
            };
            Map map = new Map()
            { MinPozition = new Position(0, 0), MaxPozition = new Position(99, 99) };
            List<int> expected = new List<int>() { 4, 7 };

            Position possible =
            Finder.FindAvailablePositionToGo(currentRobot, robots, map, positionToGo, 1);
            List<int> actual = new List<int>() { possible.X, possible.Y };

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFindEnemies()
        {
            string ownerName = "Yuliia";
            Robot.Common.Robot myRobot1 = new Robot.Common.Robot()
            { Energy = 100, Position = new Position(6, 2), OwnerName = ownerName };
            Robot.Common.Robot myRobot2 = new Robot.Common.Robot()
            { Energy = 100, Position = new Position(2, 4), OwnerName = ownerName };
            Robot.Common.Robot enemy1 = new Robot.Common.Robot()
            { Energy = 100, Position = new Position(6, 2), OwnerName = "Stranger" };
            Robot.Common.Robot enemy2 = new Robot.Common.Robot()
            { Energy = 100, Position = new Position(2, 4), OwnerName = "Stranger" };
            IList<Robot.Common.Robot> robots = new List<Robot.Common.Robot>()
            { myRobot1, enemy1, enemy2, myRobot2 };

            List<Robot.Common.Robot> enemies = Finder.FindEnemies(robots, ownerName);
            Robot.Common.Robot result = enemies.Find((r) => r.OwnerName == ownerName);

            Assert.IsNull(result);
        }

    }
}
