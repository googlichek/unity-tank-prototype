namespace TankProto
{
	/// <summary>
	/// Container for data that should exist throughout
	/// scenes or different monobehaviours.
	/// </summary>
	public struct GameData
	{
		/// <summary>
		/// Player score. For keeping thing a little less boring.
		/// </summary>
		public static int Score = 0;

		/// <summary>
		/// Number of active enemies in scene.
		/// </summary>
		public static int EnemiesInAction = 0;
	}
}
