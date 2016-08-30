using UnityEngine;

using System.Collections;
using UnityEngine.UI;
using Rewired;

public class HUDUpdater : MonoBehaviour {

	// Input stuff
	private Player _playerInput; 
	private int _playerInputID = 0;

	// Player stuff
	private Rigidbody _playerShip;
	private ShipMovement _playerShipMovement;

	// UI stuff
	[SerializeField]
	private Image _thrustBar; // Current ship thrust
	[SerializeField]
	private Slider _throttleBar; // Current throttle setting

	private void Start() {

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		_playerShip = player.GetComponent<Rigidbody>();
		_playerShipMovement = player.GetComponent<ShipMovement>();
		_playerInput = ReInput.players.GetPlayer(_playerInputID);
		enabled = true;
		GetComponent<Canvas>().enabled = true;
	}

	private void FixedUpdate() {

		float thrust = _playerShip.velocity.magnitude / _playerShipMovement.Thrust;
		_thrustBar.fillAmount = thrust;
		float throttle = _playerShipMovement.DeadZone(Mathf.Abs(_playerInput.GetAxis("Throttle")), _playerShipMovement.JoystickDeadZone);
		_throttleBar.value = throttle;
	}
}
