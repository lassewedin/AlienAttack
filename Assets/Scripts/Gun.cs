using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour
{
	public PickupSpawner pickupSpawner;
	public GUITexture nukeHUD;	


	public Rigidbody2D grenade;				
	public float grenadeSpeed = 20f;			

	public Rigidbody2D rocket;				
	public float rocketSpeed = 10f;

	public float weaponRadius = 0.5f;

	private Animator anim;
	private PlayerControl playerCtrl;
	private bool hasRocket = false;

	void Awake()
	{
		anim = transform.root.gameObject.GetComponent<Animator>();
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}

	public void loadRocket() {
		hasRocket = true;
	}

	void Update ()
	{
		bool fire1 = Input.GetButtonDown ("Fire1");
		bool fire2 = Input.GetButtonDown ("Fire2") && hasRocket;

		if(fire1 ||  fire2  )
		{
			anim.SetTrigger("Shoot");
			GetComponent<AudioSource>().Play();
			Vector2 barrel = playerCtrl.aimDirection * weaponRadius;
			Vector3 point = new Vector3(transform.position.x + barrel.x, transform.position.y + barrel.y, transform.position.z);

			Rigidbody2D fireRocket;
			float fireSpeed;

			if( fire1 ) {
				fireRocket = grenade;
				fireSpeed = grenadeSpeed;
			}
			else {
				fireRocket = rocket;
				fireSpeed = rocketSpeed;
				hasRocket = false;
				pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());
			}
			Rigidbody2D bulletInstance = Instantiate(fireRocket, point, Quaternion.Euler(new Vector3(0,0, playerCtrl.aimAngle))) as Rigidbody2D;
			bulletInstance.velocity = playerCtrl.aimDirection * fireSpeed;
		}
		nukeHUD.enabled = hasRocket;
	}
}
