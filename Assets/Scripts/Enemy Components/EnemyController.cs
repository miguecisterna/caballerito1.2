using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,ICombatTarget 
{
   
  [SerializeField] float health = 30;

  public Animator animator;
    
   
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

     public void TakeDamage(float damagePoints)
    {
        health -= damagePoints;
        animator.SetTrigger("Hurt");

        if (health <=0)
        {
          Die();
        }
        
    }

    void Die(){
      Debug.Log("Enemy Died!");
      animator.SetBool("isDead",true);
      GetComponent<Collider2D>().enabled=false;
      animator.SetTrigger("Dead");
      this.enabled=false;
    }
}
