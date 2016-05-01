using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public Transform groundCheck;
	public new Camera camera;
	[HideInInspector]
	public bool	facingRight = true;			// For determining which way the player is currently facing.
	
	[HideInInspector]
	public bool	jump = false;				// Condition for whether the player should jump.
	
	[HideInInspector]
	public Vector2 aimDirection;			// Hero -> Cursor, magnitude 1

	[HideInInspector]
	public float aimAngle;					//in degrees

	[HideInInspector]
	public float aimAngleMirror;			//in degrees
	
	
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 1000f;			// Amount of force added when the player jumps.
	public AudioClip[] taunts;				// Array of clips for when the player taunts.
	public float tauntProbability = 50f;	// Chance of a taunt happening.
	public float tauntDelay = 1f;			// Delay for when the taunt should happen.
	
	
	private int tauntIndex;					// The index of the taunts array indicating the most recent taunt.
	private bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.

	void Awake ()
	{
		anim = GetComponent<Animator> ();
	}
	
	void Update ()
	{
		grounded = Physics2D.Linecast (transform.position, groundCheck.position, 1 << LayerMask.NameToLayer ("Ground"));  

		if (Input.GetButtonDown ("Jump") && grounded)
			jump = true;

		UpdateAim ();

		if (aimDirection.x > 0 && !facingRight)
			Flip ();
		else if (aimDirection.x < 0 && facingRight)
			Flip ();

	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (h));
		if (h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			GetComponent<Rigidbody2D>().AddForce (Vector2.right * h * moveForce);

		if (Mathf.Abs (GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			GetComponent<Rigidbody2D>().velocity = new Vector2 (Mathf.Sign (GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if (jump) {
			anim.SetTrigger ("Jump");
			int i = Random.Range (0, jumpClips.Length);
			AudioSource.PlayClipAtPoint (jumpClips [i], transform.position);
			GetComponent<Rigidbody2D>().AddForce (new Vector2 (0f, jumpForce));
			jump = false;
		}

	}

	void UpdateAim ()
	{
		Vector3 worldMouse = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, camera.nearClipPlane));
		aimDirection = new Vector2 (worldMouse.x, worldMouse.y) - new Vector2 (transform.position.x, transform.position.y);
		aimDirection.Normalize ();

		aimAngle = FindAngle (aimDirection);
		aimAngleMirror = FindMirrorAngle (aimAngle);


	}

	float FindAngle (Vector2 direction)
	{
		float angle = Mathf.Rad2Deg * Mathf.Atan (Mathf.Abs (direction.y) / Mathf.Abs (direction.x));

		if (direction.x > 0f) {
			if (direction.y < 0f)
				return 360f - angle;
			return angle;
		}
		if (direction.y > 0f) 
			return 180f - angle;
		return 180f + angle;

	}

	float FindMirrorAngle (float angle)
	{
		if (angle < 90f || angle > 270)
			return angle;
		if (angle < 180f)
			return 180f - angle;
		return 360f - (angle - 180f);
	}

	void Flip ()
	{
		facingRight = !facingRight;

		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	
	public IEnumerator Taunt ()
	{

		float tauntChance = Random.Range (0f, 100f);
		if (tauntChance > tauntProbability) {

			yield return new WaitForSeconds (tauntDelay);

			if (!GetComponent<AudioSource>().isPlaying) {
				tauntIndex = TauntRandom ();
				GetComponent<AudioSource>().clip = taunts [tauntIndex];
				GetComponent<AudioSource>().Play ();
			}
		}
	}
	
	int TauntRandom ()
	{
		int i = Random.Range (0, taunts.Length);

		if (i == tauntIndex)
			return TauntRandom ();
		else
			return i;
	}
}
