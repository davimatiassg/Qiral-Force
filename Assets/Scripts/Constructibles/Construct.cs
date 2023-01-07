using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : MonoBehaviour
{
    ///Mudar para lista de ScriptableObjects depois
    public List<GameObject> constructions = new List<GameObject>();

    public List<int> idx = new List<int>();

    public void webLine(Constructible pad1, Constructible pad2, Vector2 st, Vector2 dir, float range)
    {
        GameObject newWeb = Instantiate(constructions[0], st, new Quaternion(0, 0, 0, 0), MasterWeb.instance.gameObject.transform);
        Web webAtr = Web.createWebPoint(pad1, pad2, st);
        webAtr.traceWeb(st, dir, range);
    }

}
