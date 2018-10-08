using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	public LeftHandControls leftHand;
	public RightHandControls rightHand;
	
	void Awake () {
		leftHand.SetManager(this);
		rightHand.SetManager(this);
	}
	
	public void EnableMovement(bool val){
		leftHand.SetCanMove(val);
		rightHand.SetCanMove(val);
	}

	public void RecalibrateJoycons(){
		leftHand.getCurrentJoycon().Recenter();
		rightHand.getCurrentJoycon().Recenter();
	}

	public Quaternion GetRotation(){
		return transform.rotation;
	}

	public void RecalibrateHands(){
		leftHand.SetRotation();
		rightHand.SetRotation();
	}
}
