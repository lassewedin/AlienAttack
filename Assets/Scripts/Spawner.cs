using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject[] spawnObject;

	public void Spawn () {
		int enemyIndex = Random.Range (0, spawnObject.Length);
		Instantiate (spawnObject[enemyIndex], transform.position, transform.rotation);

		foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>()) {
			p.Play ();
		}
	}
}
