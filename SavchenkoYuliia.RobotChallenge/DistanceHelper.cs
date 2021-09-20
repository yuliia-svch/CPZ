using System;

namespace AlberdaRoman.RobotChallange
{
	public class DistanceHelper
	{
		public static int FindDistance(Position a, Position b)
		{
			return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
		}
	}
}
