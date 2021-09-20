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
        private const int QUANTITY = 2;
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
            return nearest == null ? null : nearest.Position;

        }

        public static Position FindPartOfTheWay(Position a, Position b)
        {
            if ((a.X < b.X) && (a.Y < b.Y))
                return new Position(a.X + (b.X - a.X) / QUANTITY, a.Y + (b.Y - a.Y) / QUANTITY);
            else if ((a.X > b.X) && (a.Y > b.Y))
                return new Position(a.X - (a.X - b.X) / QUANTITY, a.Y - (a.Y - b.Y) / QUANTITY);
            else if ((a.X < b.X) && (a.Y > b.Y))
                return new Position(a.X + (b.X - a.X) / QUANTITY, a.Y - (a.Y - b.Y) / QUANTITY);
            else if ((a.X > b.X) && (a.Y < b.Y))
                return new Position(a.X - (a.X - b.X) / QUANTITY, a.Y + (b.Y - a.Y) / QUANTITY);
            else if (a.X == b.X)
            {
                if (a.Y > b.Y)
                    return new Position(a.X, a.Y - (a.Y - b.Y) / QUANTITY);
                else if (a.Y < b.Y)
                    return new Position(a.X, a.Y + (b.Y - a.Y) / QUANTITY);
            }
            else if (a.Y == b.Y)
            {
                if (a.X > b.X)
                    return new Position(a.X - (a.X - b.X) / QUANTITY, a.Y);
                else if (a.X < b.X)
                    return new Position(a.X + (b.X - a.X) / QUANTITY, a.Y);
            }

            return new Position(a.X, b.Y);
        }

        public static Position FindSmallerPartOfTheWay(Position a, Position b)
        {
            Position partOfTheWay = FindPartOfTheWay(a, b);
            return FindPartOfTheWay(a, partOfTheWay);
        }

        public static Position FindAvailablePositionToGo(Robot.Common.Robot movingRobot,
            IList<Robot.Common.Robot> robots, Map map, Position positionToGo, int energyToLeave)
        {
            Position particle = FindPartOfTheWay(movingRobot.Position, positionToGo);
            bool isParticleFree =
                Checker.IsCellFree(particle, movingRobot, robots);
            bool hasEnergyToMoveParticle =
                Checker.HasEnergyToGo(movingRobot, particle, energyToLeave);
            if (isParticleFree && hasEnergyToMoveParticle)
                return particle;

            Position smallerParticle = FindSmallerPartOfTheWay(movingRobot.Position, positionToGo);
            bool isSmallerParticleFree =
                Checker.IsCellFree(smallerParticle, movingRobot, robots);
            bool hasEnergyToMoveSmallerParticle =
                Checker.HasEnergyToGo(movingRobot, smallerParticle, energyToLeave);
            if (isSmallerParticleFree && hasEnergyToMoveSmallerParticle)
                return smallerParticle;

            return null;
        }

        public static List<Robot.Common.Robot> FindEnemies(IList<Robot.Common.Robot> robots, string ownerName)
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
        public static EnergyStation FindStationByPosition(Map map, Position position)
        {
            if (position == null)
                return null;
            return map.Stations.ToList().Find((s) => s.Position == position);
        }
      

        public static Robot.Common.Robot FindRobotToAttack
            (List<Robot.Common.Robot> enemies, Robot.Common.Robot movingRobot)
        {
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
