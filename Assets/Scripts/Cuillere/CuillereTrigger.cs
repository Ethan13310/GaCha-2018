using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuillereTrigger : MonoBehaviour {

	public RightHandActions handManager;
	public GameObject contenantCuillere;

	void Start(){
		contenantCuillere.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
        if(other.tag=="Ingredient"){
			handManager.HandleAction(other.transform.parent.gameObject);
		}else if(other.tag=="Cauldron"){
			//AJOUTER LES FONCTIONS LIEES AU CHAUDRON ICI
			handManager.HandleAction(other.transform.parent.gameObject);
		}else if(other.tag=="Mortar"){
			handManager.HandleAction(other.transform.parent.gameObject);
		}
    }

}
