using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastedObject : MonoBehaviour {

	Renderer material;
	Color baseColor;

	void Start(){
		material=GetComponent<Renderer>();
		baseColor=material.material.color;
	}

	public void SetIsHighlight(bool isRaycast){
		if(isRaycast)
			material.material.color = Color.green;
		else
			material.material.color = baseColor;
	}
}
