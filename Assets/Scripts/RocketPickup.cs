using UnityEngine;
using System.Collections;

public class RocketPickup : MonoBehaviour {
	public AudioClip pickupClip;		// Sound for when the bomb crate is picked up.
	
	
	private Animator anim;				// Reference to the animator component.
	private bool landed = false;		// Whether or not the crate has landed yet.
	
	
	void Awake()
	{
		anim = transform.root.GetComponent<Animator>();
	}
	

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(pickupClip, transform.position);
			Transform gun = other.transform.Find("Gun");
			Gun gunScript = gun.GetComponent<Gun>();
			gunScript.loadRocket();
			Destroy(transform.root.gameObject);
		}
		else if(other.tag == "ground" && !landed)
		{
			anim.SetTrigger("Land");
			transform.parent = null;
			gameObject.AddComponent<Rigidbody2D>();
			landed = true;		
		}
	}
}
