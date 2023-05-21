using System;
using UnityEngine;

/*public class Climber : MonoBehaviour
{
	public CharacterController characterController;
	public float gravity = 9.8f;

	private Hand currentHand = null;
	private Vector3 movement = Vector3.zero;


	private void Awake()
	{
		characterController = GetComponent<CharacterController>();
	}

	private void Update()
	{
		CalculateMvt();

	}


	private void CalculateMovement(Transform climbPoint, float climbSpeed)
	{
		Vector3 movement = Vector3.zero;

		if (currentHand)
		{
			movement = currentHand.Delta * sensitivity
		}

		if (movement == Vector3.zero)
		{
			movement.y -= gravity * Time.deltaTime;
		}
		Vector3 targetPosition = climbPoint.position;
		targetPosition.y += climbSpeed * Time.deltaTime;

		characterController.Move(targetPosition - transform.position);

		
	}

	public void SetHand(Hand hand)
	{
		if (currentHand != null)
		{
			currentHand.ReleaseClimbPoint();
		}

		currentHand = hand;
	}

	public void ClearHand()
	{
		currentHand = null;
	}

	public void Climb(Transform climbPoint, float climbSpeed)
	{
		Vector3 targetPosition = climbPoint.position;
		targetPosition.y += climbSpeed * Time.deltaTime;

		characterController.Move(targetPosition - transform.position);
	}
}*/








public class Climber: MonoBehaviour
{
	float t = 0f;
	float interpolationDuration = 2f;
	public float movementSpeed = 1f;
	public float gravity = 45.0f;
	public float sensitivity = 45.0f;

	private Hand currentHand = null;
	private CharacterController controller = null;

	private void Awake()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
		
		CalculateMovement();

	}

	private void CalculateMovement()
	{
		Vector3 movement = Vector3.zero;

		if (currentHand)
		{
			GameObject currentpoint = currentHand.GetCurrentPoint();
			Vector3 targetPosition = currentpoint.transform.position;

			//movement += currentHand.Delta * sensitivity;
			movement.x = (targetPosition.x-transform.position.x);
			movement.y = (targetPosition.y-transform.position.y);
			//movement.z = (targetPosition.z - transform.position.z);
			Debug.LogWarning("Movementy:  " + movement.y);
			controller.Move(movement*Time.deltaTime*movementSpeed);
			//Debug.LogWarning("Point: "+point);
			Debug.LogWarning("controllery:  " + controller.transform.position.y);

			//if(currentpoint.name=="ClimbPoint1 (6))-> turn on gravity -> put the player at the top of the wall
			//topPosition.y=4 topPosition.z= controller.Move((topPosition - transform.position) * movementSpeed * Time.deltaTime);
			//controller.Move(Vector3.down * gravity * Time.deltaTime);
		}

	}

	public void SetHand(Hand hand)
	{
		if (currentHand)
		{
			currentHand.ReleasePoint();
		}

		currentHand = hand;
	}

	public void ClearHand()
	{
		currentHand = null;
	}

}
