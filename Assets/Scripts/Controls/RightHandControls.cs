using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightHandControls : JoyconControls{

	protected override void Start(){
		base.Start();
	}

	void Update () {
		UpdateInitPosition();
		if(canMove)
			UpdateHandRotation();
		TranslateArm();
		//UseCameraButton();
	}

	public void UseCameraButton(){
		 // make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
        {
			Joycon j = joycons [jc_ind];
			if (j.GetButtonDown(Joycon.Button.SHOULDER_1))
            {
				// Joycon has no magnetometer, so it cannot accurately determine its yaw value. Joycon.Recenter allows the user to reset the yaw value.
				manager.RecalibrateJoycons();
				SetCanMove(false);
			}else if(j.GetButtonUp(Joycon.Button.SHOULDER_1)){
				manager.RecalibrateJoycons();
				SetCanMove(true);
			}
		}
	}
}
