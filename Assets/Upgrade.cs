using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour {
    public Gun gun;
	// Use this for initialization
	void Start () {
		
	}
    void OnTriggerEnter(Collider col)

    {
        gun.UpgradeGun();
       
        Destroy(gameObject);
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpPickup);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
