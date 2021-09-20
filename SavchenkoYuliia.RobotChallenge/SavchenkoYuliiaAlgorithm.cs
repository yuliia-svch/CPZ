using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robot.Common;

namespace SavchenkoYuliia.RobotChallenge
{
    public class SavchenkoYuliiaAlgorithm : IRobotAlgorithm
    {
        public string Author {
            get { return "Yuliia Savchenko"; }
        }

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            var myRobot = robots[robotToMoveIndex];
                if ((movingRobot.Energy > 500) && (robots.Count < map.Stations.Count))
            {
            return new CreateNewRobotCommand();
            }

            Position stationPosition = FindNearestFreeStation(robots[robotToMoveIndex], map, robots);

            if (stationPosition == null)
            return null;
            if (stationPosition == movingRobot.Position)
            return new CollectEnergyCommand();
            else
            {
            return new MoveCommand(){NewPosition = stationPosition };
            }
            
        }
    }
}
