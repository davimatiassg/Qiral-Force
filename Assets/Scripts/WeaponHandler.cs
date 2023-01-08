using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class WeaponHandler : MonoBehaviour
{
    public string weaponName;
    public Collider2D col;
    public float range;
    public float dmg;
    public float knb;
    public float cd;
    public float arc;
    public float acel;
    public float angle;
    public Sprite spr;
	public Vector2 speed;
    
    void Start()
    {
    	col = this.gameObject.GetComponent<Collider2D>();
    	col.isTrigger = true;
    }

    public void onTriggerEnter2D(Collider2D hit)
    {
    	IHitableObj hitObj = hit.gameObject.GetComponent<IHitableObj>(); 
    	if(hitObj != null)
    	{
    		hitObj.TakeDmg(dmg, knb*speed);
    	}
    }
}
