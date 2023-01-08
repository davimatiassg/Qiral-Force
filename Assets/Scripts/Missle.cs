using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missle : Bullet
{
	[SerializeField] GameObject blast;

	public override void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == target)
        {
            Instantiate(blast, transform.position, new Quaternion(0,0,0,0));
            base.OnCollisionEnter2D(collision);
        }

    }
}
