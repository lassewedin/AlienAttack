using UnityEngine;
using System.Collections;

public class Remover : MonoBehaviour
{
	public GameObject splash;
	public PickupSpawner pickupSpawner;

	void OnTriggerEnter2D (Collider2D col)
	{
		// If the player hits the trigger...
		if (col.gameObject.tag == "Player") {
			// .. stop the camera tracking the player
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<CameraFollow> ().enabled = false;
			Instantiate (splash, col.transform.position, transform.rotation);
			Destroy (col.gameObject);
			GameObject.FindGameObjectWithTag ("GameOverText").GetComponent<GUIText> ().enabled = true;
			StartCoroutine ("ReloadGame");
		} else if (col.gameObject.tag == "Pickup") {
			Destroy (col.gameObject);
			pickupSpawner.StartCoroutine (pickupSpawner.DeliverPickup ());
		} else if (col.gameObject.tag == "Enemy") {
			// ... instantiate the splash where the enemy falls in.
			Instantiate (splash, col.transform.position, transform.rotation);
			Destroy (col.gameObject);
		} else {
			Destroy (col.gameObject);
		}

	}

	IEnumerator ReloadGame ()
	{			
		// ... pause briefly
		yield return new WaitForSeconds (2);
		// ... and then reload the level.
		Application.LoadLevel (Application.loadedLevel);
	}
}
