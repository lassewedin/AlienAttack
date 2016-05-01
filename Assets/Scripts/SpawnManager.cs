using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {


	public float spawnTime = 1;
	private float lastSpawn;
	public GameObject[] spawner; 

	void Start () {
		lastSpawn = Time.time;
	}

	void Update () {
		if (spawner.Length >= 1) {
			float timePassed = Time.time - lastSpawn;
			if (timePassed > spawnTime) {
				lastSpawn = Time.time;
				int index = Random.Range (0, spawner.Length);

				spawner [index].GetComponent<Spawner>().Spawn ();
			}
		}
	}


}
