using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerayScript : MonoBehaviour {
   
    private GameObject Playero;
    public float rotateSpeed = 2.0f;
    private GameObject Camerao;
    // Use this for initialization
    void Start () {
        Camerao = Camera.main.gameObject;
        Playero = GameObject.Find("unitychan");

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 angle = new Vector3(Input.GetAxis("Mouse X") * rotateSpeed, Input.GetAxis("Mouse Y") * rotateSpeed, 0);

        //transform.RotateAround()をしようしてメインカメラを回転させる
        Camerao.transform.RotateAround(Playero.transform.position, Vector3.up, angle.x);
        Camerao.transform.RotateAround(Playero.transform.position, transform.right, angle.y);

    }
}
