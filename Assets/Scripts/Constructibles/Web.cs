using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Constructibles/WebTrail")]
public class Web : Construction
{

    public static Web createWebPoint(Construction pad1, Construction pad2, Vector2 point)
    {
        Web w = new Web();
        w.pos = point;

        List<Construction> pad1conections = pad1.getConections();
        List<Construction> pad2conections = pad2.getConections();
        for(int i = 0; i < pad1conections.Count; i++)
        {
            if(pad1conections[i].getPos() == pad2.getPos())
            {
                pad1conections[i].setPos(point);
                pad1conections[i].setConections(w.conections);
                
                break;
            }
        }
        for(int i = 0; i < pad2conections.Count; i++)
        {
            if(pad2conections[i].getPos() == pad1.getPos())
            {
                pad2conections[i].setPos(point);
                pad2conections[i].setConections(w.conections);
            
                break;
            }
        }
        w.conections.Add(pad1);
        w.conections.Add(pad2);
        MasterWeb.updateMap(w);
        return w;
    }

    public void traceWeb(Vector2 st, Vector2 dir, float range)
    {
        Vector2 fn = st + dir*range;
        Web stPoint = this;
        bool foundWeb = false;

        foreach(Construction cons in MasterWeb.map)
        {
            List<Construction> consConections = cons.getConections();
            foreach(Construction targ in consConections) //1 = cons, 2 = targ, 3 = st, 4 = fn
            {
                float det1 = (cons.getPos().x - st.x)*(st.y - fn.y) - (cons.getPos().y - st.y)*(st.x - fn.x);
                float det2 = (cons.getPos().x - st.x)*(cons.getPos().y - targ.getPos().y) - (cons.getPos().y - st.y)*(st.x - targ.getPos().x);
                float det3 = (cons.getPos().x - targ.getPos().x)*(st.y - fn.y) - (cons.getPos().y - targ.getPos().y)*(st.x - fn.x);

                if(det1 < det3 && det2 < det3 && det3 != 0)
                {
                    Vector2 tpos = new Vector2(st.x - (det2/det3)*(fn.x-st.x), st.y - (det2/det3)*(fn.y-st.y));
                    Web w = createWebPoint(cons, targ, tpos + dir*0.1f);
                    w.conections.Add(stPoint);
                    stPoint.conections.Add(w);
                    stPoint = w;
                    foundWeb = true;
                    MasterWeb.updateMap(w);
                }
            }
        }
        if(!foundWeb)
        {
            RaycastHit2D Hitted = Physics2D.Raycast(st, dir, range);
            if(Hitted)
            {
                Web w = new Web();
                w.pos = Hitted.point;
                w.conections.Add(this);
                this.conections.Add(w);
                MasterWeb.updateMap(w);
            }
        }
        
    }


    
}
