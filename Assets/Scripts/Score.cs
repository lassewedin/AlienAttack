using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour
{
	public int score = 0;					// The player's score.


	private PlayerControl playerControl;	// Reference to the player control script.
	private int previousScore = 0;			// The score in the previous frame.


	void Awake ()
	{
		playerControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
	}


	void Update ()
	{
		// Set the score text.
		GetComponent<GUIText>().text = "Score: " + score;

		// If the score has changed...
		if(previousScore != score)
			// ... play a taunt.
			if(playerControl != null )
				playerControl.StartCoroutine(playerControl.Taunt());

		// Set the previous score to this frame's score.
		previousScore = score;
	}

}
