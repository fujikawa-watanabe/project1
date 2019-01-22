using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamascript : MonoBehaviour {
    
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        Invoke("kesu", 1.5f);
    }
    void kesu()
    {
        Destroy(gameObject);
    }
}
