using UnityEngine;
using System.Collections;

public class GeneratorHealth : MonoBehaviour {
	
	public int HPstart = 1;
	public GameObject hundredPointsUI;
	public Score score;	

	private int HP;
	public bool dead = false;	
	private Animator anim;	
	private ParticleSystem smokeParticle;
	private ParticleSystem explosionParticle;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
	}

	void Start () {
		HP = HPstart;

		smokeParticle = transform.Find("blackSmokeParticle").GetComponent<ParticleSystem>();
		explosionParticle = transform.Find("explosionParticle").GetComponent<ParticleSystem>();
	}
	
	void FixedUpdate () {
		if(HP <= 0 && !dead)
			Death ();
	}
	
	void Update() {
		
	}
	
	
	public void Hurt() {
		HP--;
	}
	
	void Death()
	{
		
		score.score += 100;
		dead = true;
		Vector3 scorePos;
		scorePos = transform.position;
		scorePos.y += 1.5f;

		Instantiate(hundredPointsUI, scorePos, Quaternion.identity);
		
		smokeParticle.Play ();
		explosionParticle.Play();
		anim.SetBool ("Running", false);
		Collider2D[] cols = GetComponents<Collider2D>();
		foreach(Collider2D c in cols)
		{
			c.enabled = false;
		}
	}
}
