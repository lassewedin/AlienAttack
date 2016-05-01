using UnityEngine;
using System.Collections;

public class OnScreen : MonoBehaviour {

	public new Camera camera;

	void Update () {
		Vector3 center = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth * 0.5f, camera.pixelHeight * 0.5f, camera.nearClipPlane));
		float aspect = camera.pixelWidth / camera.pixelHeight;

		float screenHeight = camera.orthographicSize * 2f;
		float screenWidth = screenHeight * aspect;

		float barHeightInPixles = 100f;
		float barHeightInUnits = screenHeight * ( barHeightInPixles / camera.pixelHeight);

		float barWidthInPixels = barHeightInPixles;
		float barWidthInUnits = screenWidth * ( barWidthInPixels / camera.pixelWidth);

		float unitsPerPixel = barHeightInUnits / barHeightInPixles;

		transform.position = new Vector3(center.x - screenWidth * 0.5f + 40f * unitsPerPixel, center.y, center.z);
		transform.localScale = new Vector3(barHeightInUnits, barWidthInUnits, 1f);
	}
}
