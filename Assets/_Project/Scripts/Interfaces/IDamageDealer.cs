namespace TankProto
{
	public interface IDamageDealer
	{
		int Damage { get; }

		void HandleBlowUp();
	}
}
