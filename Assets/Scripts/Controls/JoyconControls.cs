using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoyconControls : MonoBehaviour {

    protected CharacterManager manager;

	protected List<Joycon> joycons;

    protected Joycon currentJoycon;

    // Values made available via Unity
    protected float[] stick;
    protected Vector3 gyro;
    protected Vector3 accel;
    public int jc_ind = 0;
    protected Quaternion orientation;

    protected bool canMove=true;

    public Vector3 eulerAngle;

    protected Vector3 initPosition;

    public GameObject endCourse;

    protected bool isTranslateArm=false;

    protected virtual void Start ()
    {
        initPosition=transform.position;
        gyro = new Vector3(0, 0, 0);
        accel = new Vector3(0, 0, 0);
        // get the public Joycon array attached to the JoyconManager in scene
        joycons = JoyconManager.Instance.j;
		if (joycons.Count < jc_ind+1){
			Destroy(gameObject);
		}
	}

    protected void UpdateHandRotation()
    {
        // make sure the Joycon only gets checked if attached
		if (joycons.Count > 0)
        {
			currentJoycon = joycons [jc_ind];

			//	j.SetRumble (160, 320, 0.2f, 200);
				// Then call SetRumble(0,0,0) when you want to turn it off.

            stick = currentJoycon.GetStick();

            // Gyro values: x, y, z axis values (in radians per second)
            gyro = currentJoycon.GetGyro();

            // Accel values:  x, y, z axis values (in Gs)
            accel = currentJoycon.GetAccel();

            orientation = currentJoycon.GetVector();

			//Vector3 v = orientation.eulerAngles;

			gameObject.transform.rotation=orientation;
            //SetRotation();

            eulerAngle=orientation.eulerAngles;
        }
    }

    public void SetRotation(){
       // gameObject.transform.rotation=currentJoycon.GetVector()*Quaternion.Inverse(transform.parent.GetComponent<CharacterManager>().GetRotation());
    // gameObject.transform.rotation= transform.parent.GetComponent<CharacterManager>().GetRotation()*Quaternion.Inverse( currentJoycon.GetVector());
        gameObject.transform.rotation= currentJoycon.GetVector()*transform.parent.GetComponent<CharacterManager>().GetRotation();
    // gameObject.transform.rotation.SetEulerRotation
    }

    public void TranslateArm(){
        if(currentJoycon.GetButtonDown(Joycon.Button.DPAD_UP)){
            isTranslateArm=true;
            transform.position=endCourse.transform.position;
        }else if(currentJoycon.GetButtonUp(Joycon.Button.DPAD_UP)){
            isTranslateArm=false;
            transform.position=initPosition;
        }
    }

    public void UpdateInitPosition(){
        if(isTranslateArm)
            return;
        initPosition=transform.position;
    }

    public Joycon getCurrentJoycon(){
        return currentJoycon;
    }

    public void SetManager(CharacterManager val){
        manager=val;
    }

    public void SetCanMove(bool val){
        canMove=val;
    }

    public bool GetCanMove(){
        return canMove;
    }
}