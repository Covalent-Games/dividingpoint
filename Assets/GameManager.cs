using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	private void Update() {

		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
