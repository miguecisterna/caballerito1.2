using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatWeapon : MonoBehaviour
{
    [SerializeField] float damagePoints=10;
    [SerializeField] TagId targetTag;

   private void OnTriggerEnter2D(Collider2D collision) 
   {


    if(targetTag.ToString().Equals(collision.gameObject.tag))
    {
    
        var combatTarget= collision.gameObject.GetComponent<ICombatTarget>();

        if(combatTarget != null)
        {
            combatTarget.TakeDamage(damagePoints);
        }









    }
   
   
   
   }












}
