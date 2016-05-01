using UnityEngine;
using System.Collections;

public class EnemyUfo : MonoBehaviour {

	public int HPstart = 5;
	public GameObject hundredPointsUI;
	public Score score;

	private GameObject[] generators;
	private int HP;
	private bool dead = false;	
					// Reference to the Score script.
	private Animator anim;	
	
	void Awake()
	{
		anim = GetComponent<Animator>();
	}
	
	// Use this for initialization
	void Start () {
		HP = HPstart;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (aliveGeneratorCount() == 0 && !dead) {
			Death ();
			StartCoroutine("ReloadGame");
		}
	}

	public int aliveGeneratorCount() {
		int aliveCount = 0;
		GeneratorHealth[] children = transform.GetComponentsInChildren<GeneratorHealth>();
		foreach(GeneratorHealth c in children) {
			if(!c.dead)
				aliveCount++;
		}
		return aliveCount;
	}
	
	
	public void Hurt()
	{

		// Reduce the number of hit points by one.
		HP--;
	}
	
	void Death()
	{
		
		// Increase the score by 100 points
		score.score += 100;
		
		// Set dead to true.
		dead = true;
		
		// Allow the enemy to rotate and spin it by adding a torque.
		GetComponent<Rigidbody2D>().isKinematic = false;
		anim.enabled = false;
		GetComponent<Rigidbody2D>().AddTorque(500);

		// Create a vector that is just above the enemy.
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;
		
		// Instantiate the 100 points prefab at this point.
		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);

		GameObject[] enemy = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach(GameObject g in enemy ) {
			Enemy e =  g.GetComponent<Enemy>();
			if(e!=null)
				e.Death();
		}
		SpawnManager esm = (SpawnManager) FindObjectOfType(typeof(SpawnManager));
		if (esm != null) {
			esm.enabled = false;
		}

		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>().enabled = false;
		GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetTrigger ("Idle");;
		GameObject.FindGameObjectWithTag("WinText").GetComponent<GUIText>().enabled = true;

	}

	IEnumerator ReloadGame()
	{			
		yield return new WaitForSeconds(10);
		Application.LoadLevel(Application.loadedLevel);
	}
}
