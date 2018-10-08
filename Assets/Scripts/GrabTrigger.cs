using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTrigger : MonoBehaviour {

	GameObject objectInGrabRange;
	
	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag=="Grab"){
			objectInGrabRange=other.gameObject.transform.parent.gameObject;
		}
    }

	void OnTriggerExit(Collider other) {
		if(other.gameObject.tag=="Grab"){
			objectInGrabRange=null;
		}
    }

	public GameObject GetObjectToGrab(){
		return objectInGrabRange;
	}
}
