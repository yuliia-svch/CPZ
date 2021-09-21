using System;
using System.Collections.Generic;
using Robot.Common;

namespace SavchenkoYuliia.RobotChallenge
{
    public class SavchenkoYuliiaAlgorithm : IRobotAlgorithm
    {
        public const int ROBOT_CREATION_LIMIT = 40;
        public const int START_ATTACK = 5;
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
            try
            {
                var currentRobot = robots[robotToMoveIndex] ?? throw new ArgumentOutOfRangeException();
                int numberOfMyRobots = Finder.FindNumberOfRobots(robots, Author);
                Position stationPosition = Finder.FindNearestFreeStation(robots[robotToMoveIndex], map, robots);
                EnergyStation energyStation = Finder.FindStationByPosition(map, stationPosition);
                List<Robot.Common.Robot> enemies = Finder.FindEnemies(robots, Author);
                Robot.Common.Robot robotToAttack = Finder.FindRobotToAttack(enemies, currentRobot);
                MoveCommand attackCommand =
                    CreateAttackCommand(RoundNumber, numberOfMyRobots, START_ATTACK, 15, 5, currentRobot, robotToAttack);
                if (RoundNumber > 30 && attackCommand != null)
                    return attackCommand;
                if (currentRobot.Energy >= 300 && numberOfMyRobots < 100)
                {
                    return new CreateNewRobotCommand();
                }

                if (currentRobot.Energy == 0)
                    return new CollectEnergyCommand();
                            
                
                if (stationPosition == null)
                {
                    if (attackCommand != null)
                        return attackCommand;
                }              

                if (stationPosition == currentRobot.Position)
                {
                    if (energyStation != null)
                    {
                        if (energyStation.Energy != 0)
                            return new CollectEnergyCommand();
                        else
                        {
                            if (attackCommand != null)
                                return attackCommand;
                        }
                    }
                }
                else
                {
                    Position positionToMoveToStation =
                    Finder.FindAvailablePositionToGo(currentRobot, robots, map, stationPosition, 1);                    
                    if (Checker.HasEnergyToGo(currentRobot, stationPosition, 1))
                        return new MoveCommand() { NewPosition = stationPosition };
                    if (positionToMoveToStation != null)
                        return new MoveCommand() { NewPosition = positionToMoveToStation };

                }
                return new MoveCommand() { NewPosition = stationPosition };
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        public static MoveCommand CreateAttackCommand
            (int roundNumber, int numberOfMyRobots, int roundNumberToAttack, int numberOfMyRobotsToAttack, int profit,
            Robot.Common.Robot movingRobot, Robot.Common.Robot robotToAttack)
        {
            if (roundNumber > roundNumberToAttack && numberOfMyRobots > numberOfMyRobotsToAttack)
            {
                if (robotToAttack != null)
                {
                    if ((Checker.IsProfitableToAttack(robotToAttack, movingRobot, profit)))
                    {
                        if (Checker.HasEnergyToGo(movingRobot, robotToAttack.Position, 1))
                            return new MoveCommand() { NewPosition = robotToAttack.Position };
                    }
                }
            }
            return null;
        }
    }
}
