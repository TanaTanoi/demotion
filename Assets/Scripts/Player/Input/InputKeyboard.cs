using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputKeyboard : PlayerInput {

    void Start()
    {
    }

	private int inputid = -1;

	public override void turn(float rotationSpeed, float horizontalInput, float verticalInput, Transform transform)
    {
		horizontalInput = Mathf.Abs (horizontalInput) == 1 ? horizontalInput : 0;
        float y = rotationSpeed * horizontalInput;// * Time.fixedDeltaTime; // <- this make the turn speed basically 0
        transform.Rotate(new Vector3(0, y, 0), Space.World);
    }

	public void RefreshInputs(int id)
	{
		// Don't need to update anything if the id's are the same
		if (this.inputid == id) return;
		this.inputid = id;
		horizontal = string.Format("Horizontal_KB{0}", id);
		vertical = string.Format("Vertical_KB{0}", id);
		boost = string.Format("Boost_KB{0}", id);
		activate = string.Format("Activate_KB{0}", id);
	}
}
