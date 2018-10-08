using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedObject : MonoBehaviour {

	public Vector3 initPosition;
	Quaternion initRotation;
	float chrono=3f;

	bool isGrabbed=false;

	void Start () {
		initPosition=transform.position;
		initRotation=transform.rotation;
	}

	public void SetIsGrabbed(bool val){
		isGrabbed=val;
	}

	void Update(){
		if(!isGrabbed&&transform.position!=initPosition){
			chrono-=Time.deltaTime;
			if(chrono<=0){
				ResetPosition();
			}
		}
	}

	public void ResetPosition(){
		chrono=3f;
		transform.position=initPosition;
		transform.rotation=initRotation;
	}

	public Quaternion GetInitRotation(){
		return initRotation;
	}
}
