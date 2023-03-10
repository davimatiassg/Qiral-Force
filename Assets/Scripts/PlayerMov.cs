using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float speed;
    public int life = 100;

    private Transform trs;
    private SpriteRenderer spr;
    private TrailRenderer trail;
    private Construct cons;

    public GameObject weaponPoint;
    public Transform weaponPivot;
    private WeaponHandler wp;

    public WeaponScriptable Weapon;
	public ShipScriptable Ship;

    public bool isAtk = false;
    public float atkCD = 0;
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


    [SerializeField] Web webP1, webP2;
    [SerializeField] Vector2 dir;
    [SerializeField] float dp1, dp2, dp3;
    [SerializeField] bool chgThread;
    [SerializeField] Vector2 vel;
    [SerializeField] float acl = 0.01f;
    [SerializeField] float snapTol = 0.01f;
    
    void Start()
    {
    	wp = weaponPoint.GetComponent<WeaponHandler>();
        trs = this.gameObject.GetComponent<Transform>();
        spr = this.gameObject.GetComponent<SpriteRenderer>();
        trail = this.gameObject.GetComponent<TrailRenderer>();
        cons = this.gameObject.GetComponent<Construct>();
        changeWeapon();
        changeShip();
    }

    void FixedUpdate()
    {

        
            
        
        
    	FrameControlls = P_Controll.PassInput();
        ////////////Movendo jogador
        
        
        dp1 = Vector2.Distance(trs.position, webP1.pos);
        dp2 = Vector2.Distance(trs.position, webP2.pos);
        dp3 = Vector2.Distance(webP1.pos, webP2.pos);
        if(dp1 + dp2 -dp3 > snapTol)
        {
            int idx = 0;
            float d = 180;
            float sd = 180;
            Vector2 ndir;
            float ag = 0;
            if(dp1<dp2){
                
                webP2 = webP1;
                for(int i = 0; i < webP1.conections.Count; i ++){
                    ag = Vector2.Angle(vel, webP1.conections[i].pos - webP1.pos);
                    if(ag < d){
                        idx = i;
                        d = ag;
                        sd = Vector2.SignedAngle(vel, webP1.conections[i].pos - webP1.pos);
                    }
                }
                webP1 = (Web)webP1.conections[idx];
                if(dp1 + dp2 -dp3 > 5*snapTol)
                {
                    trs.position = webP2.pos;
                }
            }
            else{
            
                webP1 = webP2;
                for(int i = 0; i < webP2.conections.Count; i ++){
                    ag = Vector2.Angle(vel, webP2.conections[i].pos - webP2.pos);
                    if(ag < d){
                        idx = i;
                        d = ag;
                        sd = Vector2.SignedAngle(vel, webP2.conections[i].pos - webP2.pos);
                    }
                }
                vel = Vector2Extension.Rotate(vel, sd);
                webP2 = (Web)webP2.conections[idx];
                if(dp1 + dp2 -dp3 > 5*snapTol)
                {
                    trs.position = webP1.pos;
                }
            }
        }

        dir = (webP1.pos - webP2.pos).normalized;

        vel = Vector3.Project(vel, dir);

        vel = Vector2.ClampMagnitude(vel+(Vector2)Vector3.Project(FrameControlls.movDir, dir)*acl -vel.normalized*acl/5, speed);

        trs.Translate(vel);

        //Trocar Sprites
        if(FrameControlls.movDir != LastFrameControlls.movDir && FrameControlls.movDir != Vector2.zero){
            var (s, o) = Ship.getFace(FrameControlls.movDir);
            spr.sprite = s;
            trail.sortingOrder = o;
        }

        ////////////Movendo arma:
        Vector2 mouse = FrameControlls.weapDir;//coletar a posi????o atual do mouse
        float target = Vector2.SignedAngle(LastFrameControlls.weapDir, mouse);
        weaponPivot.transform.localPosition = Vector2.SmoothDamp(weaponPivot.transform.localPosition, wp.range*mouse, ref moment, 10*Time.fixedDeltaTime);
        float a = Mathf.SmoothDampAngle(weaponPivot.localEulerAngles.z, -Vector2.SignedAngle(FrameControlls.weapDir, Vector2.up), ref angularmmt, 10*Time.fixedDeltaTime);
        weaponPivot.transform.localEulerAngles = new Vector3(0, 0, a);
        weaponPoint.transform.localEulerAngles = new Vector3(0, 0, wp.angle);
        weaponPoint.transform.localPosition = Vector2.up*wp.range;	
        LastFrameControlls = FrameControlls;

        ////////////A????es:
        //c??digo de ataque vem aqui
        if(FrameControlls.AtkBtn)
        {
            
        }
        //c??digo defesa vem aqui
        if(FrameControlls.DefBtn)
        {
            
        }
        //c??digo habilidade vem aqui
        if(FrameControlls.SklBtn)
        {
            Debug.Log("hellow");
            cons.webLine(webP1, webP2, trs.position, mouse, 50f);

        }
    }
    public void changeWeapon()
    {
    	changeWeapon(Weapon);
    }
    public void changeWeapon(WeaponScriptable w)
    {
    	Weapon = w;
    	wp.range = Weapon.range;
    	wp.knb = Weapon.knb;
    	wp.dmg = Weapon.dmg;
    	wp.spr = Weapon.spr;
    	wp.arc = Weapon.arc;
    	wp.cd = Weapon.cd;
    	wp.acel = Weapon.acel;
    	wp.angle = Weapon.angle;
    	weaponPoint.GetComponent<SpriteRenderer>().sprite = wp.spr;
    	weaponPoint.GetComponent<PolygonCollider2D>().pathCount = wp.spr.GetPhysicsShapeCount();
    	for(int i = 0; i < wp.spr.GetPhysicsShapeCount(); i++)
    	{
    		List<Vector2> points = new List<Vector2>();
    		wp.spr.GetPhysicsShape(0, points);
    		weaponPoint.GetComponent<PolygonCollider2D>().SetPath(i, points);
    	}
    }

    public void changeShip()
    {
    	changeWeapon(Ship);
    }
    public void changeWeapon(ShipScriptable s)
    {
    	Ship = s;
    	life = Ship.life;
    	speed = Ship.speed;
    	var (ss, o) = Ship.getFace(FrameControlls.movDir);
    		spr.sprite= ss;
    		trail.sortingOrder = o;
    }

    
}
