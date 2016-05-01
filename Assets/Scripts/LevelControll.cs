using UnityEngine;
using System.Collections;

public class LevelControll : MonoBehaviour {

	public SpawnManager spawnManager;
	public EnemyUfo enemyUfo;

	public float spawnTimeDefault = 2;
	public float[] spawnTime;

	private int fullUfoHP;

	// Use this for initialization
	void Start () {
		fullUfoHP = enemyUfo.aliveGeneratorCount ();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
		spawnManager.spawnTime = spawnTime [Mathf.Clamp (fullUfoHP - enemyUfo.aliveGeneratorCount (), 0, spawnTime.Length-1 )];
	}
}
