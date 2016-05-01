using UnityEngine;
using System.Collections;

public class PickupSpawner : MonoBehaviour
{
	public PlayerHealth playerHealth;
	public GameObject healthDrop;
	public GameObject bombDrop;
	public GameObject rocketDrop;
	public GameObject[] dropPosition;

	public float generalDeliveryTime = 10f;		// time in between deliveries
	public float rocketDeliveryTime = 30f;		// after this amount of time, since a rocket delivery, there will be a new one
	public float highHealthThreshold = 75f;		// The health of the player, above which only bomb crates will be delivered.
	public float lowHealthThreshold = 25f;		// The health of the player, below which only health crates will be delivered.

	private float lastRocketTime;

	void Awake ()
	{
		// Setting up the reference.
		//playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

	}


	void Start ()
	{
		// Start the first delivery.
		StartCoroutine(DeliverPickup());
		lastRocketTime = Time.time;
	}


	public IEnumerator DeliverPickup()
	{
		
		// Wait for the delivery delay.
		yield return new WaitForSeconds(generalDeliveryTime);

		Transform dropPos = dropPosition[Random.Range(0, dropPosition.Length)].transform;

		float timeSinceNuke = Time.time - lastRocketTime;
		if (timeSinceNuke > rocketDeliveryTime ) {
			lastRocketTime = Time.time;
			Instantiate(rocketDrop, dropPos.position, Quaternion.identity);
		}
		else if (playerHealth.health >= highHealthThreshold) {
			Instantiate (bombDrop, dropPos.position, Quaternion.identity);
		}
		else if(playerHealth.health <= lowHealthThreshold)
			Instantiate(healthDrop, dropPos.position, Quaternion.identity);
		else
		{
			// ... instantiate a random pickup at the drop position.
			int pickupIndex = Random.Range(0, 1);
			Instantiate(pickupIndex == 0 ? healthDrop : bombDrop, dropPos.position, Quaternion.identity);
		}
	}
}
