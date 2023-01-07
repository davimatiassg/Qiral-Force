using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Web : Constructible
{
    

    public static Web createWebPoint(Constructible pad1, Constructible pad2, Vector2 point)
    {
        Web w = new Web();
        w.pos = point;
        for(int i = 0; i < pad1.conections.Count; i++)
        {
            if(pad1.conections[i].pos == pad2.pos)
            {
                pad1.conections[i].pos = point;
                pad1.conections[i].conections = w.conections;
                pad1.line.SetPosition(i, w.pos);
                break;
            }
        }
        for(int i = 0; i < pad2.conections.Count; i++)
        {
            if(pad2.conections[i].pos == pad1.pos)
            {
                pad2.conections[i].pos = point;
                pad2.conections[i].conections = w.conections;
                pad2.line.SetPosition(i, w.pos);
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
        foreach(Constructible cons in MasterWeb.map)
        {
            foreach(Constructible targ in cons.conections) //1 = cons, 2 = targ, 3 = st, 4 = fn
            {
                float det1 = (cons.pos.x - st.x)*(st.y - fn.y) - (cons.pos.y - st.y)*(st.x - fn.x);
                float det2 = (cons.pos.x - st.x)*(cons.pos.y - targ.pos.y) - (cons.pos.y - st.y)*(st.x - targ.pos.x);
                float det3 = (cons.pos.x - targ.pos.x)*(st.y - fn.y) - (cons.pos.y - targ.pos.y)*(st.x - fn.x);

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
