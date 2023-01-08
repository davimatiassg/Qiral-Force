using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebShot : Web, ISolidObj
{
    [SerializeField] public float speed;
    [SerializeField] public float range;
    [SerializeField] public Vector2 dir;
    [SerializeField] public Vector2 startPoint;
    
    [SerializeField] public Construction Svtx1, Svtx2;

    private Collider2D col;

    public override void Start()
    {
        base.Start();
        col = this.gameObject.GetComponent<Collider2D>();
        startPoint = base.trs.position;
        line.positionCount = 2;
        line.SetPosition(0, startPoint);
        line.SetPosition(1, startPoint);

    }

    public Web SpawnWebThread()
    {
        Web substrate = Construct.webPoint(startPoint).GetComponent<Web>();
        Svtx1.connections.Remove(Svtx2);
        Svtx2.connections.Remove(Svtx1);
        Svtx1.tieUpWith(substrate);
        Svtx2.tieUpWith(substrate);
        return substrate;
    }
     void OnTriggerEnter2D(Collider2D hit)
    {
        Debug.Log("yahoo?Trigger");
        if(hit.gameObject.tag == "Solid")
        {
            Web substrate = SpawnWebThread();
            Construct.webLine(substrate, pos-dir/500);
            Destroy(this.gameObject);
        }
        
    }

    void ISolidObj.touchLine(Construction Evtx1, Construction Evtx2)
    {
        Debug.Log("yahoo?Line");

        Web substrate = SpawnWebThread();
        if((Evtx1 != Svtx1 && Evtx1 != Svtx2) ||(Evtx2 != Svtx1 && Evtx2 != Svtx2))
        {
            Evtx1.connections.Remove(Evtx2);
            Evtx2.connections.Remove(Evtx1);    
            
            List<Construction> newConnections = new List<Construction>(); 
            newConnections.Add(Evtx1);
            newConnections.Add(Evtx2);
            newConnections.Add(substrate);
            Construct.webLine(newConnections, pos-dir);
            
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(substrate.trs.gameObject);
        }
    }

    public override void FixedUpdate()
    {
        base.trs.Translate(speed*Time.fixedDeltaTime*dir);
        pos = trs.position;
        base.line.SetPosition(1, pos);
        if((pos - startPoint).magnitude >= range)
        {
            Destroy(this.gameObject);
        }
    }
    
}
