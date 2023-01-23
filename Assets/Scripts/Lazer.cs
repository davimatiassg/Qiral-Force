using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : Bullet
{
	[SerializeField] LineRenderer line;
	[SerializeField] float range;

	public override void Start()
	{
		base.Start();
		line.positionCount = 2;
		line.SetPosition(0, (Vector2)transform.position);
	}

	public override void Update()
    {

    	Vector2 hitPoint = line.GetPosition(0)+dir*range;
    	RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, dir ,range);
    	foreach(RaycastHit2D hit in hits)
    	{
    		
    		if(hit.transform.gameObject.tag == target)
    		{
    			hitPoint = hit.point;
    			if (target == "Player") {hit.transform.gameObject.GetComponent<GamePlayer>().takeDmg(dmg,Vector2.zero);}
	            if (target == "Enemy")
	            {
	                hit.transform.gameObject.GetComponent<Behaviour>().takeDmg(dmg,Vector2.zero);
	                GameObject.FindGameObjectWithTag("EnemyAudioPlayer").GetComponent<enemyAudioScript>().playAudio(
	                hit.transform.gameObject.GetComponent<Behaviour>().tipo.ToLower() + "Dmg");
	            }
    		}
    		else if(hit.transform.gameObject.tag == "Solid")
    		{
    			hitPoint = hit.point;
    		}
    	}
    	Debug.Log(hitPoint);
    	line.SetPosition(0, (Vector2)transform.position + (Vector2)dir*0.9f);
    	line.SetPosition(1, hitPoint);
        durationTime -= Time.deltaTime;
        if (durationTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
