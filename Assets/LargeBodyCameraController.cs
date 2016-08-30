using UnityEngine;

using System.Collections;

public class LargeBodyCameraController : MonoBehaviour {

	public Transform _player;

	private void Start() {

		_player = transform.parent;
		transform.parent = null;
	}

	private void Update() {

		transform.rotation = _player.rotation;
		transform.position = _player.position * 0.001f;
	}
}
