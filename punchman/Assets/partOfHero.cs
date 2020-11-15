using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class partOfHero : MonoBehaviour
{
    /// <summary>
    /// Это нога?
    /// </summary>
    public bool itsLeg = false;

    public bool этоТуловище = false;

    private bool isGrounded = false;

    public bool IsGrounded {
        get { return isGrounded; }
        set { isGrounded = value; }
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        if (itsLeg)
        {
            Collider2D[] coliders = Physics2D.OverlapCircleAll(transform.position, 0.8F);

            isGrounded = coliders.Where(x => x.GetComponentInChildren<floor>() != null).Count() > 0;
        }
        
    }
}
