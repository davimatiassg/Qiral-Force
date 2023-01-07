using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[CreateAssetMenu(menuName = "ScriptableObject/Constructibles/SolidObject (Prefab)")]
public class SolidObject : MonoBehaviour
{
	public Construction baseData;
    public Construction consData;
    [SerializeField] public Vector2 pos;
    [SerializeField] public List<Construction> conections = new List<Construction>();


    void Awake()
    {
    	consData = (Construction)ScriptableObject.CreateInstance(baseData.className);
    	consData.trs = this.gameObject.GetComponent<Transform>();
        pos = consData.trs.position;
        consData.line = this.gameObject.GetComponent<LineRenderer>();
        consData.setPos(pos);
        
    }
    void Start()
    {
    	MasterWeb.defaultMap(this.gameObject.name);
        
        consData.Start();
        
    }
    
    void FixedUpdate()
    {
       consData.FixedUpdate();
    }
}
