﻿using UnityEngine;

using System.Collections;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using System;

public class TestingSceneManager : MonoBehaviour {

	public GameObject[] AsteroidPrefabs;
	public GameObject[] PlanetPrefabs;
	public GameObject[] StarPrefabs;

	[SerializeField]
	private Transform _star;

	private void Start() {

		GenerateAsteroidCluster();
		PlaceStar();
		PlacePlanets();

	}

	private void PlacePlanets() {

		int numOfPlanets = Random.Range(0, 6);
		int numOfMoons;
		for (int i = 0; i < numOfPlanets; i++) {
			GameObject orbit = new GameObject();
			orbit.transform.SetParent(_star);
			orbit.transform.localPosition = Vector3.zero;

			GameObject planetGO = (GameObject)Instantiate(
				PlanetPrefabs[Random.Range(1, PlanetPrefabs.Length)],
				Vector3.zero,
				Quaternion.identity);

			planetGO.transform.SetParent(orbit.transform);
			


			float scaleMod = Random.Range(0.75f, 2f);

		}
	}

	private void PlaceStar() {

		GameObject starGO = (GameObject)Instantiate(StarPrefabs[Random.Range(0, StarPrefabs.Length)]);
		starGO.transform.position = new Vector3(
			Random.Range(300, 4000),
			Random.Range(300, 4000),
			Random.Range(300, 4000));
		_star = starGO.transform;
	}

	private void GenerateAsteroidCluster() {

		int clusterNumber = 0;
		float spacing = Random.Range(0.5f, 1f);
		// Determind asteroid count of new cluster;
		int clusterSize = Random.Range(25, 100);
		GameObject clusterCore = new GameObject();
		clusterCore.name = "Asteroid Cluster 0";

		// Randomly place the center of the asteroid cluster
		clusterCore.transform.position = new Vector3(Random.Range(-25, 25), Random.Range(-25, 25), Random.Range(-25, 25));
		clusterCore.transform.SetParent(transform);

		List<Collider> colliders = new List<Collider>();
		// Generate new asteroids, set the dimensions, and parent it to their cluster.
		for (int i = 0; i < clusterSize; i++) {
			int asteroidPrefabIndex = Random.Range(0, AsteroidPrefabs.Length);
			GameObject asteroidPrefab = AsteroidPrefabs[asteroidPrefabIndex];
			GameObject asteroid = (GameObject)Instantiate(asteroidPrefab);

			asteroid.transform.SetParent(clusterCore.transform);

			// Get current dimensions so we can prevent overlap
			Collider collider = asteroid.GetComponent<Collider>();
			colliders.Add(collider);
			float minDistance = -Mathf.Max(
				collider.bounds.size.x,
				collider.bounds.size.y,
				collider.bounds.size.z);
			float maxDistance = Mathf.Max(
				collider.bounds.size.x,
				collider.bounds.size.y,
				collider.bounds.size.z);

			// Set position based on size and position in custer array.
			// asteroid.transform.localPosition = new Vector3(maxDistance, maxDistance, maxDistance) * (i + 1 * spacing);
			asteroid.transform.localPosition =
				new Vector3(
					Random.Range(minDistance, maxDistance),
					Random.Range(minDistance, maxDistance),
					Random.Range(minDistance, maxDistance)) * (i + 1 * spacing);

			// Create a new scale along x, y and z.
			asteroid.transform.localScale =
				new Vector3(
					Random.Range(.65f, 1.35f),
					Random.Range(.65f, 1.35f),
					Random.Range(.65f, 1.35f)) * Random.Range(.5f, 3f);

			// If the asteroid is overlapping just remove it.
			for (int b = colliders.Count - 2; b > 0; b--) {
				if (collider.bounds.Intersects(colliders[b].bounds)) {
					Destroy(asteroid.gameObject);
					colliders.RemoveAt(colliders.Count - 1);
					//Debug.Log("asteroid_" + clusterNumber + "_" + i + " was overlapping. It has been removed");
					continue;
				}
			}
			asteroid.name = "asteroid_" + clusterNumber + "_" + i;

		}
	}
}
