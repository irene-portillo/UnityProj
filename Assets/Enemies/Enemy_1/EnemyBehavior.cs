using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // SHOOTING 
    public GameObject sporeObj;
    public int spawnRate; // seconds 
    private float timer = 0 ;
    
    // MOVEMENT 
    public PlayerMovement plrMoveScript;
    private EnemyHealth enHealthScript;
    public AnimateEnemy animEnScript;
    

    void Start()
    {
        enHealthScript = transform.GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
    }

////////////////////////////////////////////////// SHOOTING //////////////////////////////////////////////////////////////////////////
void updateTimer() // update timer + shoots 
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            shootSpore();
            timer = 0;
        }
    }

    void shootSpore()
    {
        GameObject newSpore = Instantiate(sporeObj, transform.position, transform.rotation); //create spore
        newSpore.transform.SetParent(transform);
        SporeBehavior sbScript = newSpore.GetComponent<SporeBehavior>();
        sbScript.enabled = true; // enable its script
    }

}
