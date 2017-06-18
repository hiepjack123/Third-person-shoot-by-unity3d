using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Animator arenaAnimator;
    public GameObject player;//represent Marine current location
    public GameObject[] spawnPoints;//Place where alien will spawn and chase the player
    #region declare Alien
    public GameObject alien;//Alien's prefab
    public int maxAliensOnScreen;//Max aliens at once
    public int totalAliens;//Kill all total aliens we will win the game
    public float maxSpawnTime;
    public float minSpawnTime;
    public int alienPerSpawn;//After a spawn we have how many aliens
    private int aliensOnScreen = 0;
    private float generatedSpawnTime = 0;//random spawm time for aliens
    private float currentSpawnTime = 0;
    #endregion
    #region declare Upgrade Power
    public GameObject upgradeFrefabs;
    public Gun gun;
    public float upgradeMaxSpawnTime = 7.5f;
    public  bool spawnUpgraded = false;
    public float actualUpgradeTime=0;
    public float currentUpgradeTime=0;
    #endregion
    // Use this for initialization
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)//Evething falls when Marine die ^^
        {return;}
        #region Spawn Aliens and Its behavior
        currentSpawnTime += Time.deltaTime;//add spawn time
        
        if (currentSpawnTime > generatedSpawnTime)
        {
            currentSpawnTime = 0;//Time will reset when spawn a alien to the floor
            generatedSpawnTime = Random.Range(minSpawnTime, maxSpawnTime);
            //create a random spawn time to spawn the aliens
            if (alienPerSpawn > 0 && aliensOnScreen < totalAliens)//Alien  spawn cannot be zero, and max aliens on screen dont exceed the totals aliens
            {
                List<int> previousSpawnLocation = new List<int>();//create spawn location
                if (alienPerSpawn > spawnPoints.Length)
                { alienPerSpawn = spawnPoints.Length - 1; }//This limits the number of aliens you can spawn by the number of spawn points.
                alienPerSpawn = (alienPerSpawn > totalAliens) ? alienPerSpawn - totalAliens : alienPerSpawn;//reduce spawns
                for (int i = 0; i < alienPerSpawn; i++)
                {
                    if (aliensOnScreen < maxAliensOnScreen)//
                    {
                        aliensOnScreen += 1;//count the alien spawn until it exceeds total aliens
                        int spawnPoint = -1;//first spawnPoint is not set
                        while (spawnPoint == -1)//add random spawn point
                        {
                            int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                            if (!previousSpawnLocation.Contains(randomNumber))//Loop run to find a new spawn point equal random number
                            {
                                previousSpawnLocation.Add(randomNumber);
                                spawnPoint = randomNumber;
                            }
                        }
                        GameObject spawnLocation = spawnPoints[spawnPoint];//Make a spawn location 
                        GameObject newAlien = Instantiate(alien) as GameObject;//create a new clone alien
                        newAlien.transform.position = spawnLocation.transform.position;//Alien will spawn to a spawnLocation position
                        Alien alienScript = newAlien.GetComponent<Alien>();//get component of class Alien this class have target property and agent property
                        alienScript.target = player.transform;//transform of the player Alien will move and find the location of the player
                        Vector3 targetRotation = new Vector3(player.transform.position.x, newAlien.transform.position.y, player.transform.position.z);
                        newAlien.transform.LookAt(targetRotation);//Look at Marine
                        alienScript.OnDestroy.AddListener(AlienDestroy);//Call the OnsDestroy Event and do the method AlienDestroy()
        #endregion
        #region Upgrade Power For Marine
                        currentUpgradeTime += 0.05f;
                        if (currentUpgradeTime > actualUpgradeTime)
                        {
                            currentUpgradeTime = 0;
                            actualUpgradeTime = Random.Range(upgradeMaxSpawnTime - 3f, upgradeMaxSpawnTime);//Pick a random time to generate pick up
                            actualUpgradeTime = Mathf.Round(actualUpgradeTime);
                            spawnUpgraded = false;
                            if (!spawnUpgraded)
                            {
                                int randomNumber = Random.Range(0, spawnPoints.Length - 1);
                                GameObject spawnPowerLocation = spawnPoints[randomNumber];//create spawn power location
                                GameObject upgrade = Instantiate(upgradeFrefabs) as GameObject;
                                Upgrade upgradeScript = upgrade.GetComponent<Upgrade>();//create a class upgradeScript 
                                //upgrade have method GetComponent<> 
                                upgradeScript.gun = gun;//upgradeScript have a property name gun
                                upgrade.transform.position = spawnPowerLocation.transform.position;//Spawn power pick up
                                spawnUpgraded = true;
                                SoundManager.Instance.PlayOneShot(SoundManager.Instance.powerUpAppear);
        #endregion

                            }
                            
                        }

                    }
                }
            }





        }
    }
    private void EndGame()
    {
        SoundManager.Instance.PlayOneShot(SoundManager.Instance.elevatorArrived);
        arenaAnimator.SetTrigger("PlayerWon");

    }
    public void AlienDestroy()
    {
        aliensOnScreen -= 1;
        totalAliens -= 1;
        if (totalAliens == 0)
        {
           
            Invoke("EndGame", 2.0f);
        }
       
    
    }
}
                
            


