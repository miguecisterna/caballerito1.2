using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerChecker : MonoBehaviour
{

    [SerializeField] LayerMask targetLayerMask;
    
    [Range(0,360)]
    [SerializeField] float angle;
    
    private Vector2 direction;
    [SerializeField] float distance;

    public bool isTouching;
    
    void Update()
    {
        CalculateDirection();
        isTouching = Physics2D.Raycast(this.transform.position,direction,distance,targetLayerMask);
    }

    void CalculateDirection()
    {
        direction = new Vector2(Mathf.Cos(angle*Mathf.Deg2Rad),Mathf.Sin(angle*Mathf.Deg2Rad));
    }


#if UNITY_EDITOR
    private void OnDrawGizmos() 
    {
        if (isTouching)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        CalculateDirection();
        Gizmos.DrawRay(this.transform.position, direction * distance);
    }


#endif    
}
