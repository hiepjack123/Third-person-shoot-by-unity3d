using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {
    public float destructTime = 0.03f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Initiate()//This method is call other method an do it in 0.05s
    {
        Invoke("selfDestruct", destructTime);
    }
    private void selfDestruct()
    { Destroy(gameObject); }
}
