using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyhand : MonoBehaviour {
    private Enemymotion enemymotion;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
         enemymotion = GameObject.Find("Enemy").GetComponent<Enemymotion>();
	}
    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            enemymotion.dam_flag = true;
            //Debug.Log("プレイヤーにダメージ");

        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            enemymotion.dam_flag = false;
        }
    }
}
