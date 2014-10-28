using UnityEngine;
using System.Collections;

public class HumanAnimator : MonoBehaviour {
	public float deadZone = 5f;             // The number of degrees for which the rotation isn't controlled by Mecanim.
	public float speedDampTime = 0.1f;				// Damping time for the Speed parameter.
	public float angularSpeedDampTime = 0.7f;		// Damping time for the AngularSpeed parameter
	public float angleResponseTime = 0.6f;			// Response time for turning an angle into angularSpeed.

	private NavMeshAgent nav;               // Reference to the nav mesh agent.
	private Animator anim;                  // Reference to the Animator.
	
	void Awake() {
		nav = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();

		// Making sure the rotation is controlled by Mecanim.
		nav.updateRotation = false;

		// We need to convert the angle for the deadzone from degrees to radians.
		deadZone *= Mathf.Deg2Rad;
	}
		
	void Update() {
		// Create the parameters to pass to the helper function.
		float speed;
		float angle;
		
		// Otherwise the speed is a projection of desired velocity on to the forward vector...
		speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
		
		// ... and the angle is the angle between forward and the desired velocity.
		angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
		
		// If the angle is within the deadZone...
		if(Mathf.Abs(angle) < deadZone) {
			// ... set the direction to be along the desired direction and set the angle to be zero.
			transform.LookAt(transform.position + nav.desiredVelocity);
			angle = 0f;
		}

		// Angular speed is the number of degrees per second.
		float angularSpeed = angle / angleResponseTime;
		
		// Set the mecanim parameters and apply the appropriate damping to them.
		anim.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
		anim.SetFloat("AngularSpeed", angularSpeed, angularSpeedDampTime, Time.deltaTime);
	}

	void OnAnimatorMove() {
		// Set the NavMeshAgent's velocity to the change in position since the last frame, by the time it took for the last frame.
		nav.velocity = anim.deltaPosition / Time.deltaTime;

		// The gameobject's rotation is driven by the animation's rotation.
		transform.rotation = anim.rootRotation;
	}

	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector) {
		// If the vector the angle is being calculated to is 0...
		if(toVector == Vector3.zero) // ... the angle between them is 0.
			return 0f;
		
		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
		float angle = Vector3.Angle(fromVector, toVector);
		
		// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
		Vector3 normal = Vector3.Cross(fromVector, toVector);
		
		// The dot product of the normal with the upVector will be positive if they point in the same direction.
		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
		
		// We need to convert the angle we've found from degrees to radians.
		angle *= Mathf.Deg2Rad;
		
		return angle;
	}
}
