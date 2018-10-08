using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	float chronoTest=0.5f;
	bool isLaunch=false;

	public void LaunchVelocityUp(){
	//	isLaunch=true;
		GetComponent<Rigidbody>().velocity*=1.2f;
		chronoTest=0.5f;
	}

	void Update(){
		if(!isLaunch)
			return;
		if(chronoTest>0){
			GetComponent<Rigidbody>().velocity*=1.2f;
			chronoTest-=Time.deltaTime;
		}else{
			isLaunch=false;
		}
	}
}
