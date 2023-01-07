using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(menuName = "ScriptableObject/Constructibles/SolidObject (Prefab)")]
public class SolidObject : MonoBehaviour
{
    public Construction consData;
    [SerializeField] public Vector2 pos;
    [SerializeField] public List<Construction> conections = new List<Construction>();


      
    void Start()
    {
        consData = (Construction)ScriptableObject.CreateInstance(consData.className);
        consData.trs = this.gameObject.GetComponent<Transform>();
        consData.line = this.gameObject.GetComponent<LineRenderer>();
        List<Construction> conn = new List<Construction>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Construction"))
        {
            SolidObject so = g.GetComponent<SolidObject>();
            if(g != this.gameObject)
            {
                conections.Add(so.consData);
            }
        }
        if(conections.Count > 0)
        {
            for(int i = 0; i < conections.Count; i++)
            {
                conn.Add((Construction)conections[i]);
            }
            consData.setConections(conn);
            consData.setPos(pos);
        }
        
        consData.Start();
        
    }
    
    void FixedUpdate()
    {
       consData.FixedUpdate();
    }
}
