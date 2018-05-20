using UnityEngine;

namespace TankProto
{
	/// <summary>
	/// Does damage.
	/// </summary>
	[RequireComponent(typeof(Collider))]
	[RequireComponent(typeof(Rigidbody))]
	[RequireComponent(typeof(HealthHandler))]
	public class PlayerDamageDealer : MonoBehaviour, IDamageDealer
	{
		public int Damage { get { return _damage; } }

		[Header("Damage Dealer Variables")]
		[SerializeField] [Range(0, 500)] private int _damage = 500;
		private HealthHandler _healthHandler = null;

		void OnEnable()
		{
			_healthHandler = GetComponent<HealthHandler>();
			_healthHandler.TriggerZeroHealthEvent += HandleBlowUp;
		}

		/// <summary>
		/// Loads game end scene.
		/// </summary>
		public void HandleBlowUp()
		{
			FindObjectOfType<SceneLoadingHandler>().LoadNextScene();
		}
	}
}
