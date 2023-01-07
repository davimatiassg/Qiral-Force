using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constructible : MonoBehaviour
{
    [SerializeField] public LineRenderer line;
	[SerializeField] public Transform trs;
    [SerializeField] public Vector2 pos;
	[SerializeField] public List<Constructible> conections;

	public webEffect active;

    void Start()
    {
        trs = this.gameObject.GetComponent<Transform>();
        trs.position = pos;
        line = this.gameObject.GetComponent<LineRenderer>();
        line.positionCount = conections.Count*2;
        for(int i = 0; i < conections.Count; i ++)
        {
            line.SetPosition(2*i, pos);
            line.SetPosition(2*i+1, conections[i].pos);
        }
    }
    /*
    void FixedUpdate()
    {
    	foreach(Constructible c in conections)
    	{
    		RaycastHit2D[] targets = Physics2D.LinecastAll(c.pos, pos);
    		foreach(RaycastHit2D hit in targets)
    		{
    			active.causeEffect(hit.transform.gameObject.GetComponent<HitableObj>());
    		}
    	}
    }

*/

}
