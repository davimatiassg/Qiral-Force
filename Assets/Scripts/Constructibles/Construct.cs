using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : MonoBehaviour
{
    ///Mudar para lista de ScriptableObjects depois
    public List<GameObject> Constructables;

    public static List<GameObject> constructions;
    
    void Start()
    {
    	Construct.constructions = Constructables;
    }

    public static GameObject webPoint(Vector2 st)
    {	
    	GameObject newPoint = Instantiate(constructions[0], st, new Quaternion(0, 0, 0, 0), MasterWeb.instance.gameObject.transform);
    	MasterWeb.updateMap(newPoint.GetComponent<Construction>());
        return newPoint;
    }

    public static GameObject webLine(Construction substrate, Vector2 st)
    {
        GameObject newWeb = webPoint(st);
        newWeb.GetComponent<Web>().tieUpWith(substrate);
        return newWeb;
    }

    public static GameObject webLine(List<Construction> substrates, Vector2 st)
    {
        GameObject newWeb = webPoint(st);
        foreach(Construction substrate in substrates)
        {
        	newWeb.GetComponent<Web>().tieUpWith(substrate);
        }
        return newWeb;
    }

    public static GameObject webLine(Construction[] substrates, Vector2 st)
    {
        GameObject newWeb = webPoint(st);
        foreach(Construction substrate in substrates)
        {
        	newWeb.GetComponent<Web>().tieUpWith(substrate);
        }
        return newWeb;
    }

    public static GameObject webShot(Vector2 st, Vector2 dir, float speed, float range, Construction vtx1, Construction vtx2)
    {
    	GameObject webshot = Instantiate(constructions[1], st, new Quaternion(0, 0, 0, 0));
    	WebShot shot = webshot.GetComponent<WebShot>();
    	shot.Svtx1 = vtx1;
    	shot.Svtx2 = vtx2;//AddConnection((Construction)newP);
    	shot.dir = dir;
    	shot.speed = speed;
    	shot.range = range;
    	return webshot;
    }
}
