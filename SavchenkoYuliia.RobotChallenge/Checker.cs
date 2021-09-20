using System;
using Robot.Common;
using System.Collections.Generic;

namespace SavchenkoYuliia.RobotChallenge
{
	public class Checker
	{
        private const double PERCENTAGE = 0.1;
        private const int ENERGY_TO_LOSE = 30;
        public static bool IsStationAreaFree
            (Position stationPosition, Robot.Common.Robot currentRobot, IList<Robot.Common.Robot> robots)
        {
            IList<Robot.Common.Robot> occupants = Finder.FindOtherRobotsInStationArea(stationPosition, currentRobot, robots);
            if (occupants.Count != 0)
                return false;
            return true;
        }

        public static bool IsCellFree(Position cell, Robot.Common.Robot movingRobot, IList<Robot.Common.Robot> robots)
        {
            foreach (var robot in robots)
            {
                if (robot != movingRobot)
                {
                    if (robot.Position == cell)
                        return false;
                }
            }
            return true;
        }

        public static bool HasEnergyToGo(Robot.Common.Robot movingRobot, Position position2, int energyToLeave)
        {
            int energyToGo = DistanceHelper.FindDistance(movingRobot.Position, position2);
            if (((movingRobot.Energy - energyToGo) >= energyToLeave))
                return true;
            return false;
        }

        public static bool IsProfitableToAttack(Robot.Common.Robot enemyRobot, Robot.Common.Robot movingRobot, int profit)
        {
            int distance = DistanceHelper.FindDistance(enemyRobot.Position, movingRobot.Position);
            if ((enemyRobot.Energy * PERCENTAGE - distance - ENERGY_TO_LOSE <= profit) || (enemyRobot.OwnerName == movingRobot.OwnerName))
                return false;
            return enemyRobot == null ? false : true;
        }


    }


}
