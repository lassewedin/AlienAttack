using UnityEngine;
using System.Collections;

public class Rocket : MonoBehaviour {


	public AudioClip boom;
	private ParticleSystem explosionFX;

	void Awake() {
		explosionFX = GameObject.FindGameObjectWithTag("ExplosionFX").GetComponent<ParticleSystem>();
	}

	void Start () 
	{
		Destroy(gameObject, 4);
	}
	

	void OnExplode()
	{
		explosionFX.transform.position = transform.position;
		explosionFX.Play ();
		AudioSource.PlayClipAtPoint(boom, transform.position);
	}
	
	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "Enemy")
		{
			col.gameObject.GetComponent<Enemy>().Hurt();
			OnExplode();
			Destroy (gameObject);
		}
		else if(col.tag == "EnemyUfo")
		{
			Rigidbody2D rockedBody = GetComponent<Rigidbody2D>();
			rockedBody.gravityScale = 1;
		}
		else if(col.tag == "EnemyUfoGen" )
		{
			col.gameObject.GetComponent<GeneratorHealth>().Hurt();
			OnExplode();
			Destroy (gameObject);
		}
		else if(col.tag == "BombPickup")
		{
			col.gameObject.GetComponent<Bomb>().Explode();
			Destroy (col.transform.root.gameObject);
			Destroy (gameObject);
		}
		else if(col.gameObject.tag != "Player" && col.gameObject.tag != "EnemyUfo")
		{
			OnExplode();
			Destroy (gameObject);
		}
	}
}
