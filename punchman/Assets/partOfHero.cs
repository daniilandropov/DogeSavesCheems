using Assets.scripts;
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

    public int AttackPower = 0;

    public int ReciveGamageToHero;

    public character owner;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemyPart = collision.gameObject.GetComponent<partOfHero>();
        if (enemyPart)
        {
            if (enemyPart.owner == owner)
                return;

            //часть тела принимает урон от попадания только в том случае если сама в этот момент не атакует
            if(AttackPower == 0)
            {
                owner.ReciveDamage(enemyPart.AttackPower);
            }
        }
    }
}
