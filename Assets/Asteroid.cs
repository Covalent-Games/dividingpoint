using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Collider))]
public class Asteroid : MonoBehaviour {

	private Vector3 _rotationVector;
	private int _rotationSpeed;

	// Use this for initialization
	void Start () {

		_rotationVector = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
		_rotationSpeed = Random.Range(1, 5);
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate(_rotationVector * _rotationSpeed * Time.deltaTime);
	}
}
