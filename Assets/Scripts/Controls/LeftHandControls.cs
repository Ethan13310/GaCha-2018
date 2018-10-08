using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHandControls : JoyconControls{

	State currentState;

	public GameObject raycastObject;
	public GameObject handPoint;

	List<GameObject> entityRaycasted;

	GrabTrigger grabTrigger;
	GameObject grabbedObject;

	protected override void Start(){
		base.Start();
		entityRaycasted=new List<GameObject>();
		grabTrigger=GetComponentInChildren<GrabTrigger>();
		currentState=State.grabState;
	}
	
	void Update () {
		UpdateInitPosition();
		if(canMove)
			UpdateHandRotation();
		SwitchStateControl();
		GrabObject();
		ChewObject();
		TranslateArm();
	}

	void FixedUpdate(){
		RaycastTarget();
	}

	public void RaycastTarget(){
		Debug.DrawLine(this.transform.position, raycastObject.transform.position);
		if(currentState==State.grabState)
			return;
		Ray ray = new Ray(this.transform.position, raycastObject.transform.position);
		RaycastHit hit;
		Debug.DrawLine(this.transform.position, raycastObject.transform.position);
        if (Physics.Raycast(ray, out hit))
        {
			if(hit.collider.gameObject.GetComponent<RaycastedObject>()==null)
				return;
			entityRaycasted.Add(hit.collider.gameObject);
			hit.collider.gameObject.GetComponent<RaycastedObject>().SetIsHighlight(true);
		}else{
			ResetObjectsRaycasted();
		}
	}

	public void GrabObject(){
		UpdateGrabbedObjectPosition();
		if(currentState==State.pointState)
			return;
		if(currentJoycon.GetButtonDown(Joycon.Button.SHOULDER_2)&&(grabTrigger.GetObjectToGrab()!=null)){
			//grabTrigger.GetObjectToGrab().transform.parent=this.transform;
			grabbedObject= grabTrigger.GetObjectToGrab();
			grabbedObject.transform.SetParent(this.transform.parent);
			grabbedObject.GetComponent<GrabbedObject>().SetIsGrabbed(true);
        	grabbedObject.transform.localRotation = handPoint.transform.rotation*grabbedObject.GetComponent<GrabbedObject>().GetInitRotation();
        	//We re-position the ball on our guide object 
        	grabbedObject.transform.position = handPoint.transform.position;
		}
	}

	public void ChewObject(){
		if(currentJoycon.GetButtonUp(Joycon.Button.SHOULDER_2)&&(grabbedObject!=null)){
			grabbedObject.transform.SetParent(null);
			grabbedObject.GetComponent<GrabbedObject>().SetIsGrabbed(false);
			//grabbedObject.GetComponent<test>().LaunchVelocityUp();
			grabbedObject=null;
		}
	}

	public void UpdateGrabbedObjectPosition(){
		if(grabbedObject==null)
			return;
		//grabbedObject.transform.localRotation = grabbedObject.GetComponent<GrabbedObject>().GetInitRotation();
        Quaternion baseRotation = handPoint.transform.rotation*grabbedObject.GetComponent<GrabbedObject>().GetInitRotation();
		// var rot = Quaternion.AngleAxis(90.0f, new Vector3(0.75f, 0.0f, 0.25f));
		grabbedObject.transform.localRotation = baseRotation;
        	//We re-position the ball on our guide object 
        grabbedObject.transform.position = handPoint.transform.position;
	}

	public void ResetObjectsRaycasted(){
		foreach(GameObject raycasted in entityRaycasted){
			raycasted.GetComponent<RaycastedObject>().SetIsHighlight(false);
		}
	}

	public void SwitchStateControl(){
		if(grabbedObject!=null)
			return;
		if(currentJoycon.GetButtonDown(Joycon.Button.SHOULDER_1))
			SwitchState();
	}

	public void SwitchState(){
		if(currentState==State.pointState){
			ResetObjectsRaycasted();
			currentState=State.grabState;
		}else
			currentState=State.pointState;
	}

	public enum State{
		grabState,
		pointState
	}
}
