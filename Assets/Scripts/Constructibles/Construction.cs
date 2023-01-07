using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Constructibles/Blank Construction")]
public class Construction : ScriptableObject//, Construction
{
    [SerializeField] public string className;
    [SerializeField] public LineRenderer line;
    [SerializeField] public Transform trs;
    [SerializeField] public Vector2 pos;
    [SerializeField] public List<Construction> conections = new List<Construction>();
    public webEffect active;


    public Vector2 getPos()
    {
        return pos;
    }

    public void setPos(Vector2 p)
    {
        pos = p;
    }

    public List<Construction> getConections()
    {
        return conections;
    }

    public void setConections(List<Construction> conn)
    {
        conections = conn;
        Vector3[] points = new Vector3[conn.Count*2];
        for(int i = 0; i < conn.Count; i++)
        {
            points[2*i] = pos;
            points[2*i + 1] =conn[i].getPos();
        }
        Debug.Log(line);
        line.SetPositions(points);
    }

    public void Start()
    {
        trs.position = pos;
        Debug.Log(line);
        Debug.Log(conections);
        /*
        line.positionCount = conections.Count*2;
        for(int i = 0; i < conections.Count; i ++)
        {
            line.SetPosition(2*i, pos);
            line.SetPosition(2*i+1, conections[i].getPos());
        }*/
    }
    
    public void FixedUpdate()
    {/*
        foreach(Construction c in conections)
        {
            RaycastHit2D[] targets = Physics2D.LinecastAll(c.getPos(), pos);
            foreach(RaycastHit2D hit in targets)
            {
                active.causeEffect(hit.transform.gameObject.GetComponent<HitableObj>());
            }
        }
        */
    }
}
