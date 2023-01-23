using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMov : MonoBehaviour
{
    [SerializeField] public GameObject GunHolster;
    public float speed;
    public int life = 100;

    private Transform trs;
    private SpriteRenderer spr;
    private TrailRenderer trail;

    public GameObject weaponPoint;
    public Transform weaponPivot;
    private WeaponHandler wp;

    public WeaponScriptable Weapon;
	public SpiderScriptable Spider;

    public bool isAtk = false;
    public Vector2 moment = Vector2.zero;
    public Vector2 pivotBack = Vector2.zero;
    public float angularmmt = 0;
    public float angularmmt2 = 0;

    Vector2 lstmouse = Vector2.up;

    //Controll Stuff
    (Vector2 movDir, Vector3 weapDir, bool AtkBtn, bool DefBtn, bool SklBtn, bool Pause) FrameControlls;
    (Vector2 movDir, Vector3 weapDir, bool AtkBtn, bool DefBtn, bool SklBtn, bool Pause) LastFrameControlls;
    Vector2 atkCharge = Vector2.zero;
    float atkTolerance = 0.15f;

    [SerializeField] Vector2 WeaponPTg = Vector2.zero;
    [SerializeField] float WeaponATg = 0;

    [SerializeField] Construction webP1, webP2;
    [SerializeField] Vector2 dir;
    [SerializeField] float dp1, dp2, dp3;
    [SerializeField] bool chgThread;
    [SerializeField] Vector2 vel;
    [SerializeField] float acl = 0.01f;
    [SerializeField] float snapTol = 0.01f;

    private bool started = true;

    private float webCd = 0.5f;
    

    void FixedUpdate()
    {   
        if(started)
        {
            wp = weaponPoint.GetComponent<WeaponHandler>();
            trs = this.gameObject.GetComponent<Transform>();
            spr = this.gameObject.GetComponent<SpriteRenderer>();
            trail = this.gameObject.GetComponent<TrailRenderer>();
            updateWeapon();
            updateSpider();
            started = false;
        }
    	FrameControlls = P_Controll.PassInput();
        ////////////Movendo jogador
        
        
        dp1 = Vector2.Distance(trs.position, webP1.getPos());
        dp2 = Vector2.Distance(trs.position, webP2.getPos());
        dp3 = Vector2.Distance(webP1.getPos(), webP2.getPos());
        if(dp1 + dp2 -dp3 > snapTol)
        {
            int idx = 0;
            float d = 180;
            float sd = 180;
            Vector2 ndir;
            float ag = 0;
            if(dp1<dp2){
                List<Construction> webP1connections = webP1.getConnections();
                webP2 = webP1;
                for(int i = 0; i < webP1connections.Count; i ++){
                    ndir = webP1connections[i].getPos() - webP1.getPos();
                    ag = Vector2.Angle(FrameControlls.movDir+vel.normalized/5,ndir);
                    if(ag < d){
                        idx = i;
                        d = ag;
                        sd = Vector2.SignedAngle(vel, ndir);
                    }
                }
                vel = Vector2Extension.Rotate(vel, sd*0.7f);
                webP1 = webP1connections[idx];
                if(dp1 + dp2 -dp3 > 5*snapTol)
                {
                    trs.position = webP2.getPos();
                }
            }
            else{
            
                webP1 = webP2;
                List<Construction> webP2connections = webP2.getConnections();
                for(int i = 0; i < webP2connections.Count; i ++){
                    ndir = webP2connections[i].getPos() - webP2.getPos();
                    ag = Vector2.Angle(FrameControlls.movDir+vel.normalized/5, ndir);
                    if(ag < d){
                        idx = i;
                        d = ag;
                        sd = Vector2.SignedAngle(vel, ndir);
                    }
                }

                vel = Vector2Extension.Rotate(vel, sd*0.7f);
                webP2 = webP2connections[idx];
                if(dp1 + dp2 -dp3 > 5*snapTol)
                {
                    trs.position = webP1.getPos();
                }
            }
        }


        
        dir = (webP1.getPos() - webP2.getPos()).normalized;

        vel = Vector3.Project(vel, dir);

        vel = Vector2.ClampMagnitude(vel+(Vector2)Vector3.Project(FrameControlls.movDir, dir)*acl -vel.normalized*acl/5, speed);

        trs.Translate(vel*Time.fixedDeltaTime);

        //Trocar Sprites
        if(FrameControlls.movDir != LastFrameControlls.movDir && FrameControlls.movDir != Vector2.zero){
            var (s, o) = Spider.getFace(FrameControlls.movDir);
            spr.sprite = s;
            trail.sortingOrder = o;
        }

        ////////////Movendo arma:
        Vector2 mouse = FrameControlls.weapDir;//coletar a posição atual do mouse
        float target = Vector2.SignedAngle(LastFrameControlls.weapDir, mouse);
        weaponPivot.transform.localPosition = Vector2.SmoothDamp(weaponPivot.transform.localPosition, 0.9f*mouse, ref moment, 10*Time.fixedDeltaTime);
        float a = Mathf.SmoothDampAngle(weaponPivot.localEulerAngles.z, -Vector2.SignedAngle(FrameControlls.weapDir, Vector2.up), ref angularmmt, 10*Time.fixedDeltaTime);
        weaponPivot.transform.localEulerAngles = new Vector3(0, 0, a);
        weaponPoint.transform.localEulerAngles = new Vector3(0, 0, -0.5f);
        weaponPoint.transform.localPosition = Vector2.up*0.9f;	
        
        if(webCd < 1f)
        {
            webCd += Time.fixedDeltaTime;
        }
        ////////////Ações:
        //código de ataque vem aqui
        if(FrameControlls.AtkBtn)
        {
            wp.Shot(weaponPivot.transform.up);
        }
        //código defesa vem aqui
        if(FrameControlls.DefBtn)
        {
            
        }
        //código habilidade vem aqui
        if(FrameControlls.SklBtn && webCd >= 1f)
        {
            webCd = 0f;
            Debug.Log("hellow");
            Construct.webShot(trs.position + weaponPivot.transform.up, weaponPivot.transform.up, 50f, 30f, webP1, webP2);

        }


        LastFrameControlls = FrameControlls;
    }
    public void updateWeapon()
    {
    	changeWeapon(Weapon);
    }
    public void changeWeapon(WeaponScriptable w)
    {
        wp.Weapon = w;
        GunHolster.transform.GetComponent<UnityEngine.UI.Image>().sprite = w.spr;
    	wp.updateWeapon();
    }

    public void updateSpider()
    {
    	changeSpider(Spider);
    }
    public void changeSpider(SpiderScriptable s)
    {
    	Spider = s;
    	life = Spider.life;
    	speed = Spider.speed;
    	var (ss, o) = Spider.getFace(FrameControlls.movDir);
    		spr.sprite= ss;
    		trail.sortingOrder = o;
    }

    
}
