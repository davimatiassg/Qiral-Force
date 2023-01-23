using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenBullet : Bullet
{
	public GameObject coin;
	
    public override void OnCollisionEnter2D(Collision2D collision)
    {
    	if (collision.gameObject.tag == target)
    	{
    		Vector3 rndPos = new Vector3 (Random.Range(-2,2),Random.Range(-2,2),0);
            Instantiate(coin,transform.position + rndPos,transform.rotation);
    	}
        base.OnCollisionEnter2D(collision);
        
    }
}
