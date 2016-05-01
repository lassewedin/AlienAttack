using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{	
	public float health = 100f;					// The player's health.
	public float maxHealth = 100f;
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public AudioClip[] ouchClips;				// Array of clips to play when the player is damaged.
	public float hurtForce = 10f;				// The force with which the player is pushed when hurt.
	public float damageAmount = 10f;			// The amount of damage to take when enemies touch the player
	public GameObject body;
	public SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.

	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private PlayerControl playerControl;		// Reference to the PlayerControl script.
	private Animator anim;						// Reference to the Animator on the player
	private float timeRed = 0.5f;
	private float playerRed = 0f;

	void Awake ()
	{
		health = maxHealth;
		playerControl = GetComponent<PlayerControl> ();
		anim = GetComponent<Animator> ();
		healthScale = healthBar.transform.localScale;
	}

	public void Update ()
	{
		playerRed -= Time.deltaTime;
		body.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f - (playerRed / timeRed), 1f - (playerRed / timeRed), 1f);

	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if (col.gameObject.tag == "Enemy" || col.gameObject.tag == "EnemyUfo") {
			if (Time.time > lastHitTime + repeatDamagePeriod) {

				if (health > 0f) {
					TakeDamage (col.transform); 
					lastHitTime = Time.time; 
				} else {

					Collider2D[] cols = GetComponents<Collider2D> ();
					foreach (Collider2D c in cols) {
						c.isTrigger = true;
					}

					SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer> ();
					foreach (SpriteRenderer s in spr) {
						s.sortingLayerName = "UI";
					}

					GetComponent<PlayerControl> ().enabled = false;

					GetComponentInChildren<Gun> ().enabled = false;

					anim.SetTrigger ("Die");
				}
			}
		}
	}

	void TakeDamage (Transform enemy)
	{
		playerControl.jump = false;
		Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
		GetComponent<Rigidbody2D>().AddForce (hurtVector * hurtForce);
		health -= damageAmount;
		UpdateHealthBar ();
		playerRed = timeRed;
		int i = Random.Range (0, ouchClips.Length);
		AudioSource.PlayClipAtPoint (ouchClips [i], transform.position);
	}

	public void UpdateHealthBar ()
	{
		healthBar.material.color = Color.Lerp (Color.green, Color.red, 1 - health * 0.01f);
		healthBar.transform.localScale = new Vector3 (healthScale.x * health * 0.01f, 1, 1);
	}
}
