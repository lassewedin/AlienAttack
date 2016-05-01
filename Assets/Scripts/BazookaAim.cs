using UnityEngine;
using System.Collections;

public class BazookaAim : MonoBehaviour {

	private PlayerControl playerCtrl;

	void Awake()
	{
		playerCtrl = transform.root.GetComponent<PlayerControl>();
	}

	void Update () {
		transform.localRotation = Quaternion.Euler (0f, 0f, playerCtrl.aimAngleMirror );
	}
}
