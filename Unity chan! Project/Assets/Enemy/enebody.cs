using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enebody : MonoBehaviour {
    private Enemymotion enemymotion;
    private float ene_sign = 1f;
    // Use this for initialization
    void Start () {
        enemymotion = transform.root.gameObject.GetComponent<Enemymotion>();
    }
	
	// Update is called once per frame
	void Update () {
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Debug.Log("プレイヤー発見");
            //敵の状態変化(臨戦)
            BoxCollider boxcol = GetComponent<BoxCollider>();
            boxcol.size = new Vector3(30.0f, 2.0f, 60.0f);
            enemymotion.ene_flag = 3;
            enemymotion.ene_speed = 2f;
            enemymotion.ene_rotationspeed = 3f;
            enemymotion.first_flag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("プレイヤーを見失った");
            BoxCollider boxcol = GetComponent<BoxCollider>();
            //敵の状態変化（索敵）
            boxcol.size = new Vector3(20.0f, 2.0f, 40.0f);
            enemymotion.ene_flag = 1;
            enemymotion.ene_speed = 1f;
            enemymotion.ene_rotationspeed = 1f;
            enemymotion.first_flag = true;
        }
    }
}
