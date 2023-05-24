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








public class Climber : MonoBehaviour
{
	public float movementSpeed = 1f;
	private float gravity;
	bool IsClimbing = false;
	private HandController currentHand = null;
	private CharacterController controller = null;
	public GameObject finaltarget;

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
			Debug.LogWarning("Climbing script yes");
			gravity = 0;
			IsClimbing = true;
			GameObject currentpoint = currentHand.GetCurrentPoint();
			Vector3 targetPosition = currentpoint.transform.position;

			

			//movement += currentHand.Delta * sensitivity;
			movement.x = (targetPosition.x - transform.position.x);
			movement.y = (targetPosition.y - transform.position.y);
			controller.Move(movement * Time.deltaTime);
			//movement.z = (targetPosition.z - transform.position.z);
			Debug.LogWarning("Movementy:  " + transform.position.y);

			//Debug.LogWarning("Point: "+point);
			//Debug.LogWarning("controllery:  " + controller.transform.position.y);

			if (currentpoint.name == "VirtualPoint")
			{

				//movement.x = (finaltarget.transform.position.x - transform.position.x);
				//movement.y = 2;//(finaltarget.transform.position.y - transform.position.y);
				//movement.z = (finaltarget.transform.position.z - transform.position.z);
				//Debug.LogWarning("VirtualPoint");
				//transform.position = finaltarget.transform.position;
				Vector3 elevatedPosition = transform.position + Vector3.up * 2;

				controller.Move((elevatedPosition - transform.position)*Time.deltaTime);
				if (transform.position.y >= 3)
                {
					Vector3 ForwardPosition = transform.position + Vector3.forward * 2;
					controller.Move((ForwardPosition - transform.position) * Time.deltaTime);
				}
					
				//Vector3 IntermediateTarget = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
				Debug.LogWarning("Position y"+transform.position.y);
				//transform.position = Vector3.MoveTowards(transform.position, IntermediateTarget, -100* Time.deltaTime);
				/*if (transform.position == IntermediateTarget)
                {
					Debug.LogWarning("Position reached");
					transform.position = Vector3.MoveTowards(transform.position, finaltarget.transform.position, -Time.deltaTime);
				}*/
				//transform.position = Vector3.MoveTowards(transform.position, finaltarget.transform.position,-Time.deltaTime);
				
				//Debug.LogWarning("increment " + ((finaltarget.transform.position.z - transform.position.z)*Time.deltaTime*100));
				//gravity -= 9.81 * Time.deltaTime;
				//movement.z=4*
				//controller.Move(movement* Time.deltaTime*100);
				//controller.Move(transform.forward * 4)
				//Debug.LogWarning("Movementz après:  " + transform.position.z);
				//y -= gravity * Time.deltaTime;
				//controller.Move(Physics.gravity * Time.deltaTime);
				IsClimbing = false;
				

			}
            

		}
		if (movement== Vector3.zero)
        {
			controller.Move(Physics.gravity * Time.deltaTime);
		}
	}

	public void SetHand(HandController hand)
	{
		if (currentHand)
		{
			Debug.LogWarningFormat("SetHand");
			currentHand.ReleasePoint();
		}
		Debug.LogWarningFormat("{0}",hand==null);
		currentHand = hand;
		Debug.LogWarningFormat("{0}", currentHand.name);
	}

	public void ClearHand()
	{
		Debug.LogWarningFormat("ClearHand");
		currentHand = null;
	}

	}

