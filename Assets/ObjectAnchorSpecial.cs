using UnityEngine;

public class ObjectAnchorSpecial : ObjectAnchor
{

	public void teleport_attach_to(HandController hand_controller)
	{
		// call regular attach function
		attach_to(hand_controller);

		// also make sure object "flies to" hand
		transform.position = hand_controller.transform.position;
		Debug.LogWarningFormat("Moving object to {0}", transform.position);
	}

}
