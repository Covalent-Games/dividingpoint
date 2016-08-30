using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour {

	public GameObject Owner;
	public float Speed;
	public Vector3 Heading;
	public int Damage;
	public bool Splash;
	public int SplashDamage;
	public float Range;

	private Vector3 _origin;

	private void Start() {

		_origin = transform.position;
	}

	private void Update() {

		if (Vector3.Distance(_origin, transform.position) > Range) {
			Disable();
		}
	}

	private void OnTriggerEnter(Collider col) {

		Invoke("Disable", 0.2f);
		Debug.Log("Shot " +col.gameObject.name);
	}

	private void Disable() {

		// TODO: Pool this...
		Destroy(gameObject);
	}
}
