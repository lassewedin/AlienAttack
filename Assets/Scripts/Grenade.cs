using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour 
{
	public GameObject explosion;		// Prefab of explosion effect.
	public float restingLimit = 10f;

	private int beenResting = 0;

	void Start () 
	{
		Destroy(gameObject, 3);
	}


	void OnExplode()
	{
		Quaternion randomRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
		Instantiate (explosion, transform.position, randomRotation);
	}

	void FixedUpdate() {
		Rigidbody2D rockedBody = GetComponent<Rigidbody2D>();
		if (rockedBody.velocity.magnitude < restingLimit)
			beenResting++;
		else
			beenResting = 0;
		if(beenResting > 10 )
			Destroy (gameObject);
	}

	void OnTriggerEnter2D (Collider2D col) 
	{
		if(col.tag == "Enemy")
		{
			col.gameObject.GetComponent<Enemy>().Hurt();
			OnExplode();
			Destroy (gameObject);

		}
		else if(col.tag == "BombPickup")
		{
			col.gameObject.GetComponent<Bomb>().Explode();
			Destroy (col.transform.root.gameObject);
			Destroy (gameObject);
		}
		else if(col.gameObject.tag != "Player" && col.gameObject.tag != "Bullet")
		{
			Rigidbody2D rockedBody = GetComponent<Rigidbody2D>();
			rockedBody.gravityScale = 1;
		}
	}
}
