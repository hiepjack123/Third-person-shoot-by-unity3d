using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    public GameObject player;
    public GameObject bulletPrefab;
    public Transform lauchPosition;
    private AudioSource audioSource;//Shoot sound
    #region Upgraded
    public bool isUpgraded;//Upgrade Marine's gun
    public float upgradeTime = 10f;//pgradeTime is how long the upgrade will last (in seconds).
    public float currentTime; //currentTime keeps track of how long it’s been since the gun was upgraded.
    
    #endregion
    // Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
	}
     public void fireBullet()
    {
       if(player==null)
       { return; }//when player dies we cant create bullet from his gun xD ^^
        #region Bullet Setup
        Rigidbody bullet = createBullet();
        
        bullet.GetComponent<Rigidbody>().velocity = transform.parent.forward * 100;// velocity is the vector of the rigidbody
      
        #endregion
        audioSource.PlayOneShot(SoundManager.Instance.gunFire);//Call method PlayOneShot() when Marine shoots
        if (isUpgraded) { 
            Rigidbody bullet2=createBullet();
            bullet2.velocity = (transform.right + transform.forward / 0.5f) * 100;
                Rigidbody bullet3=createBullet();
            bullet3.velocity=((transform.right*-1)+transform.forward/0.5f)*100;
            if (isUpgraded)
                audioSource.PlayOneShot(SoundManager.Instance.upgradedGunFire);
            else
                audioSource.PlayOneShot(SoundManager.Instance.gunFire);
        
        
        }
    }
    public void UpgradeGun()
    {
        isUpgraded = true;
        currentTime = 0;//if the player step into upgraded pick up
    }


            


   
     public Rigidbody createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;//create bullet
        bullet.transform.position = lauchPosition.position;//cause bullet at first in lauch position
        return bullet.GetComponent<Rigidbody>();//bullet has a rigidbody attached to it. This code make the bullet hit
    }
	// Update is called once per frame
	void Update () {
#region Shoot Action 
        if (Input.GetMouseButtonDown(0))
            if (!IsInvoking("fireBullet"))//call the method fireBullet() when we haven't fired yet
                InvokeRepeating("fireBullet", 0, 0.1f);//In this case the bullet will launch every 0.1 second
        if (Input.GetMouseButtonUp(0))
            CancelInvoke("fireBullet");//Cancel the Involve when you click fire again
#endregion
        currentTime += Time.deltaTime;
        if(currentTime>upgradeTime&&isUpgraded==true)
        { isUpgraded = false; }//After time we cannot upgrade

    }
}
