using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : Construction
{
	[SerializeField] private webEffect activeEffect;
	[SerializeField] public LineRenderer line;

    private int updateTime = 25;

	public override void AddConnection(Construction newSubstrate)
    {
    	base.AddConnection(newSubstrate);
    	line.positionCount += 2;
    	line.SetPosition(line.positionCount -2, newSubstrate.getPos());
    	line.SetPosition(line.positionCount -1, pos);
    }

    public override void setConnections(List<Construction> conn)
    {
        base.setConnections(conn);
        updateConnections(conn);
    }

    public void updateConnections(List<Construction> conn)
    {
        Vector3[] points = new Vector3[conn.Count*2];
        for(int i = 0; i < conn.Count; i++)
        {
            points[2*i] = pos;
            points[2*i + 1] = conn[i].getPos();
        }
        line.positionCount = conn.Count*2;
        line.SetPositions(points);
    }

    public override void tieUpWith(Construction substrate)
    {
    	AddConnection(substrate);
        substrate.AddConnection(this);
    }

    public virtual void FixedUpdate()
    {
    	updateConnections(connections);
        for(int i = 0; i < connections.Count; i ++)
        {	
        	RaycastHit2D[] targets = Physics2D.LinecastAll(connections[i].getPos(), pos);
            foreach(RaycastHit2D hit in targets)
            {
                
                if(activeEffect != null)
				{
	                activeEffect.causeEffect(hit.transform.gameObject.GetComponent<IHitableObj>());
	            }

	            ISolidObj Sol = hit.collider.gameObject.GetComponent<ISolidObj>();
	            if(Sol != null)
	            {
	            	Sol.touchLine((Construction)this, connections[i]);
	            }

	        }
	    }
    }

    public override void Start()
    {
    	base.Start();
    	line = this.gameObject.GetComponent<LineRenderer>();
    }


    
}
