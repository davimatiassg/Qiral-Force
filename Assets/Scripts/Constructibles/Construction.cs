using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Construction : MonoBehaviour//, Construction
{
    [SerializeField] public Transform trs;
    [SerializeField] public Vector2 pos;
    [SerializeField] public List<Construction> connections = new List<Construction>();
    
    public Vector2 getPos()
    {
        return pos;
    }

    public void setPos(Vector2 p)
    {
        pos = p;
        trs.position = p;
    }

    public List<Construction> getConnections()
    {
        return connections;
    }

    public virtual void AddConnection(Construction newSubstrate)
    {
    	connections.Add(newSubstrate);
    }

    public virtual void setConnections(List<Construction> conn)
    {
        connections = conn;
    }

    public virtual void tieUpWith(Construction substrate)
    {
    	AddConnection(substrate);
        substrate.AddConnection(this);
    }
    public virtual void Start()
    {
    	trs = this.gameObject.GetComponent<Transform>();
        pos = trs.position;
    }
}
