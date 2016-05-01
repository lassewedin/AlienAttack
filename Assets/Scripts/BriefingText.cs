using UnityEngine;
using System.Collections;

public class BriefingText : MonoBehaviour {

	public float textTime = 2f;

	void Update () {
		if( Time.timeSinceLevelLoad > textTime )
			GetComponent<GUIText>().enabled = false;
	}
}
