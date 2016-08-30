using UnityEngine;

using System.Collections;
using Rewired;

public class ShipAction : MonoBehaviour {

	private Player _playerInput;
	private int _playerID = 0;
	[SerializeField]
	private Transform[] _guns;
	[SerializeField]
	private GameObject _projectile;
	[SerializeField]
	private float _shotsPerSecond;
	private float _shotCooldown;
	private int _nextGun = 0;

	private void Awake() {

		//if (!isLocalPlayer) {
		//	enabled = false;
		//}

		_playerInput = ReInput.players.GetPlayer(_playerID);
		_projectile = (GameObject)Resources.Load("Projectiles/RedProjectile");
	}
	
	// Update is called once per frame
	void Update () {

		_shotCooldown -= Time.deltaTime;
		if (_playerInput.GetButton("Fire Primary")) {
			if (_shotCooldown < 0) {
				FirePrimary();
			}
		}
	}

	private void FirePrimary() {

		Transform gun = _guns[_nextGun];
		GameObject proj =
			(GameObject)Instantiate(
				_projectile,
				gun.position,
				gun.rotation);
		Projectile p = proj.GetComponent<Projectile>();
		p.Owner = gameObject;
		p.Speed = 1000;
		p.Range = 4000f;
		proj.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * p.Speed, ForceMode.VelocityChange);
		_nextGun = (_nextGun + 1) % _guns.Length;
		_shotCooldown = 1f / _shotsPerSecond;
	}
}
