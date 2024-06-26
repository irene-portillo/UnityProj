using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    //SPRITE
    private SpriteRenderer spriteRendererPlr;
    public GameObject spriteObjHolder;
    public Sprite idlePlr;
    public Sprite attackPlr;
    //LOGIC
    public float attackRate; 
    private float attackTimer = 0;
    private bool isAttacking = false;
    private GameObject enemyObject; // set through collision 
    public GameLogic gameLogicScript;
    //ANIMATION
    public Animator plrAnimator;

    void Start()
    {
        spriteRendererPlr= spriteObjHolder.GetComponent<SpriteRenderer>();
        attackTimer = attackRate;
    }

    void Update()
    {
        if (gameLogicScript.gameOver == true) { return; } // make sure game isnt over 

        if ( Input.GetKey(KeyCode.F) && playerCanAttack() ) // player attempts to attack 
        {
            attack();
        }
        updateTimer();
        animatePlr();
    }

    private bool playerCanAttack() 
    {
        return (enemyObject != null) && (attackTimer >= attackRate); // Check if colliding w enemy & cooldown is ok
    }

    void attack()
    {
        EnemyHealth enemyHealthScript = enemyObject.GetComponentInChildren<EnemyHealth>(); 
        if (enemyHealthScript != null) //make sure script exists
        {
            enemyHealthScript.takeDamage(5); // make enemy take damage -1hp
            attackTimer = 0; //reset timer
            resetEnemyObj();
        }
    }

    void updateTimer()
    {
        if (attackTimer < attackRate) // cooldown in process
        {
            isAttacking = true;
            attackTimer += Time.deltaTime;
        }
        else // cooldown complete
        {
            isAttacking = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            enemyObject = collision.gameObject; // colliding with enemy
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        resetEnemyObj();
    }

    public void resetEnemyObj()
    {
        enemyObject = null; 
    }

    void animatePlr() //switch attack & idle sprite
    {
        if (isAttacking)
        {
            plrAnimator.SetBool("isAttacking", true);
        }
        else
        {
            plrAnimator.SetBool("isAttacking", false);
        }
    }

}
