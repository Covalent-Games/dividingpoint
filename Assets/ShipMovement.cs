using UnityEngine;

using System.Collections;
using System;
using Rewired;

public class ShipMovement : MonoBehaviour {

	public float JoystickDeadZone;
	public int PlayerID = 0;

	public float Thrust {
		get {
			return _thrust;
		}
	}

	private Player _playerInput;
	private Rigidbody _rigidBody;
	[SerializeField]
	private float _thrust = 250;
	[SerializeField]
	private float _maneuverability = 250;

	
	void Awake() {

		_rigidBody = GetComponent<Rigidbody>();
		_playerInput = ReInput.players.GetPlayer(PlayerID);
	}
	
	void FixedUpdate () {

		Vector3 appliedDirectionalVelocity = Vector3.zero;
		Vector3 appliedAngularVelocity = Vector3.zero;
		_rigidBody.drag = 1f;

		// Get Pitch/Yaw/Roll
		appliedAngularVelocity.x += _playerInput.GetAxis("Pitch") * _maneuverability;
		appliedAngularVelocity.y += _playerInput.GetAxis("Yaw") * _maneuverability;
		appliedAngularVelocity.z -= _playerInput.GetAxis("Roll") * _maneuverability;

		// Get Throttle, apply braking if no throttle input detected.
		appliedDirectionalVelocity.z += _playerInput.GetAxis("Throttle") * _thrust;
		if (appliedDirectionalVelocity.z < -JoystickDeadZone || appliedDirectionalVelocity.z > JoystickDeadZone) {
			_rigidBody.drag = 0;
		} else {
			_rigidBody.drag = 1;
		}

		_rigidBody.velocity = 
			Vector3.MoveTowards(
				_rigidBody.velocity, 
				transform.TransformDirection(appliedDirectionalVelocity), 
				_thrust / _rigidBody.mass * Time.fixedDeltaTime);

		_rigidBody.AddRelativeTorque(appliedAngularVelocity);
		_rigidBody.velocity = Vector3.ClampMagnitude(_rigidBody.velocity, _thrust);
		_rigidBody.angularVelocity = Vector3.ClampMagnitude(_rigidBody.angularVelocity, _maneuverability);
		_rigidBody.angularVelocity = new Vector3(
			_rigidBody.angularVelocity.x, 
			Mathf.Clamp(_rigidBody.angularVelocity.y, -(_maneuverability / 2), _maneuverability / 2),
			_rigidBody.angularVelocity.z);
	}

	public float DeadZone(float input, float deadzone) {

		if (input > -deadzone && input < deadzone) {
			return 0f;
		} else {
			return input;
		}
	}
}
