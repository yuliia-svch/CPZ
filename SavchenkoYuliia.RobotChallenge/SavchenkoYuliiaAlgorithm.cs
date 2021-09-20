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
        public const int ROBOT_CREATION_LIMIT = 40;
        public const int START_ATTACK = 15;
        public int RoundNumber { get; set; }
        public string Author => "Savchenko Yuliia";
        public SavchenkoYuliiaAlgorithm()
        {
            Logger.OnLogRound += Logger_OnLogRound;
        }
        private void Logger_OnLogRound(object sender, LogRoundEventArgs e)
        {
            RoundNumber++;
        }

        public RobotCommand DoStep(IList<Robot.Common.Robot> robots, int robotToMoveIndex, Map map)
        {
            var myRobot = robots[robotToMoveIndex];
            int numberOfMyRobots = Finder.FindNumberOfRobots(robots, Author);
            Position stationToGo = Finder.FindNearestFreeStation(robots[robotToMoveIndex], map, robots);
            Robot.Common.Robot robotToAttack = Finder.FindRobotToAttack(robots, myRobot);

            if ((myRobot.Energy > 500) && (robots.Count < map.Stations.Count) && (numberOfMyRobots < 100)
                && (RoundNumber < ROBOT_CREATION_LIMIT))
            {
                return new CreateNewRobotCommand();
            }
            
            if (RoundNumber > START_ATTACK)
            {
                if ((Checker.IsProfitableToAttack(robotToAttack, myRobot, 20)))
                {
                    if (Checker.HasEnergyToGo(myRobot, robotToAttack.Position, 1))
                        return new MoveCommand() { NewPosition = robotToAttack.Position };
                }                   
            }
            if (stationToGo == null && robotToAttack != null)
            {
                if ((Checker.IsProfitableToAttack(robotToAttack, myRobot, 10)))
                {
                    if (Checker.HasEnergyToGo(myRobot, robotToAttack.Position, 1))
                        return new MoveCommand() { NewPosition = robotToAttack.Position };
                }
            }
            if (stationToGo == myRobot.Position)
            {
                EnergyStation energyStation = map.Stations.ToList().Find((s) => s.Position == stationToGo);
                if (energyStation.Energy != 0)
                    return new CollectEnergyCommand();
                else
                {
                    if ((Checker.IsProfitableToAttack(robotToAttack, myRobot, 10)))
                    {
                        if (Checker.HasEnergyToGo(myRobot, robotToAttack.Position, 1))
                            return new MoveCommand() { NewPosition = robotToAttack.Position };
                    }
                }
            }
            else
            {
                return new MoveCommand() { NewPosition = stationToGo };
            }
            return new CollectEnergyCommand();
        }

       
    }
}
