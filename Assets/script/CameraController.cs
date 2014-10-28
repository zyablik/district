using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Transform player;

	public float smooth = 1.5f;
	public float minAngle = 135f;
	public float maxAngle = 220f;

	// Use this for initialization
	void FixedUpdate() {
		// Create a vector from the camera towards the player.
		Vector3 relPlayerPosition = player.position - transform.position;

		// Create a rotation based on the relative position of the player being the forward vector.
		Quaternion lookAtRotation = Quaternion.LookRotation(relPlayerPosition);
		
		// Lerp the camera's rotation between it's current rotation and the rotation that looks at the player.
		Quaternion qua = Quaternion.Lerp(transform.rotation, lookAtRotation, smooth * Time.deltaTime);
		Quaternion fixedqua = Quaternion.identity;
		fixedqua.eulerAngles = 
			new Vector3(qua.eulerAngles.x, Mathf.Clamp(qua.eulerAngles.y, minAngle, maxAngle), qua.eulerAngles.z);
		transform.rotation = fixedqua;
//		Debug.Log(" transform.rotation = " + transform.rotation.eulerAngles);
	}
}
