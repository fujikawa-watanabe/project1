using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enegage : MonoBehaviour {
    private GameObject canvas;
    public float enemy_HP = 0;

    //敵のHPゲージを常にプレイヤーのほうへ
    private GameObject targetObject;

    // Use this for initialization
    void Start () {
        //敵のHPゲージを常にプレイヤーのほうへ
        targetObject = GameObject.Find("unitychan");
        canvas = GameObject.Find("Canvas");
    }
	
	// Update is called once per frame
	void Update () {
        //敵のHPゲージを常にプレイヤーのほうへ
        canvas.transform.LookAt(targetObject.transform);

    }
}
