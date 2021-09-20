using System;
using Robot.Common;
using System.Collections.Generic;
using System.Linq;

namespace SavchenkoYuliia.RobotChallenge
{
    public class Finder
    {
        private const double PERCENTAGE = 0.1;
        private const int ENERGY_TO_LOSE = 30;
        private const int STATION_AREA = 2;
        public static Position FindNearestFreeStation(Robot.Common.Robot movingRobot, Map map,
                IList<Robot.Common.Robot> robots)
        {
            EnergyStation nearest = null;
            int minDistance = int.MaxValue;
            foreach (var station in map.Stations)
            {
                if (Checker.IsStationAreaFree(station.Position, movingRobot, robots))
                {
                    int d = DistanceHelper.FindDistance(station.Position, movingRobot.Position);
                    if (d < minDistance)
                    {
                        minDistance = d;
                        nearest = station;
                    }
                }
            }
            if (nearest != null)
            {
                Position result = null;
                List<Position> positions = FindAvailableCellsAroundStation(nearest.Position, map);
                int minDistanceAroundStation = int.MaxValue;
                foreach (Position p in positions)
                {
                    int d = DistanceHelper.FindDistance(p, movingRobot.Position);
                    if (d < minDistanceAroundStation)
                    {
                        minDistanceAroundStation = d;
                        result = p;
                    }
                }
                return result;
            }
            return nearest == null ? null : nearest.Position;

        }

        private static List<Robot.Common.Robot> FindEnemies(IList<Robot.Common.Robot> robots, string ownerName)
        {
            return robots.ToList().FindAll((r) => r.OwnerName != ownerName);
        }

        public static IList<Robot.Common.Robot> FindOtherRobotsInStationArea
        (Position stationPosition, Robot.Common.Robot currentRobot, IList<Robot.Common.Robot> robots)
        {
            IList<Robot.Common.Robot> occupants = new List<Robot.Common.Robot>();
            foreach (var robot in robots)
            {
                if (DistanceHelper.FindDistance(stationPosition, robot.Position) <= STATION_AREA)
                {
                    if (robot != currentRobot)
                        occupants.Add(robot);
                }
            }
            return occupants;
        }

        public static int FindNumberOfRobots(IList<Robot.Common.Robot> robots, string ownerName)
        {
            int res = 0;
            foreach (var robot in robots)
                if (robot.OwnerName == ownerName)
                    res++;
            return res;
        }

        private static List<Position> FindAvailableCellsAroundStation(Position position, Map map)
        {
            int X = position.X;
            int Y = position.Y;
            List<Position> positions = new List<Position>();
            positions.Add(position);
            for (int i = X - 2; i <= X + 2; i++)
            {
                Position newPosition = new Position(i, Y);
                if (map.IsValid(newPosition) && newPosition != position)
                    positions.Add(newPosition);
            }
            for (int j = Y - 2; j <= Y + 2; j++)
            {
                Position newPosition = new Position(X, j);
                if (map.IsValid(newPosition) && newPosition != position)
                    positions.Add(newPosition);
            }
            for (int i = X - 1; i <= X + 1; i += 2)
            {
                for (int j = Y - 1; j <= Y + 1; j += 2)
                {
                    Position newPosition = new Position(i, j);
                    if (map.IsValid(newPosition) && newPosition != position)
                        positions.Add(newPosition);
                }
            }
            return positions;
        }

        public static Robot.Common.Robot FindRobotToAttack
            (IList<Robot.Common.Robot> robots, Robot.Common.Robot movingRobot)
        {
            List<Robot.Common.Robot> enemies = FindEnemies(robots, movingRobot.OwnerName);
            enemies.Sort(new EnemyComparer() { myRobot = movingRobot });
            enemies.Reverse();
            for (int i = 0; i < enemies.Count; i++)
            {
                if (Checker.HasEnergyToGo(movingRobot, enemies[i].Position, 40))
                {
                    return enemies[i];
                }
            }
            return null;
        }

        public class EnemyComparer : IComparer<Robot.Common.Robot>
        {
            public Robot.Common.Robot myRobot { get; set; }
            public int Compare(Robot.Common.Robot x, Robot.Common.Robot y)
            {
                int profit1 = (int)(x.Energy * PERCENTAGE) - 
                    (DistanceHelper.FindDistance(x.Position, myRobot.Position) + ENERGY_TO_LOSE);
                int profit2 = (int)(y.Energy * PERCENTAGE) - 
                    (DistanceHelper.FindDistance(y.Position, myRobot.Position) + ENERGY_TO_LOSE);
                if (profit1 > profit2)
                    return 1;
                else if (profit1 < profit2)
                    return -1;
                else
                    return 0;
            }
        }

    }
}
