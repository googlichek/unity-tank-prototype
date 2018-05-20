namespace TankProto
{
	/// <summary>
	/// Interface for entities that can deal (and receive) damage.
	/// </summary>
	public interface IDamageDealer
	{
		int Damage { get; }

		void HandleBlowUp();
	}
}
