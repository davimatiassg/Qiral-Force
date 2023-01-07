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

    [SerializeField] SolidObject P1, P2;
    [SerializeField] Construction webP1, webP2;
    [SerializeField] Vector2 dir;
    [SerializeField] float dp1, dp2, dp3;
    [SerializeField] bool chgThread;
    [SerializeField] Vector2 vel;
    [SerializeField] float acl = 0.01f;
    [SerializeField] float snapTol = 0.01f;
    
    void Start()
    {
        webP1 = (Construction) P1.consData;
        webP2 = (Construction) P2.consData;
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
                List<Construction> webP1conections = webP1.getConections();
                webP2 = webP1;
                for(int i = 0; i < webP1conections.Count; i ++){
                    ag = Vector2.Angle(vel, webP1conections[i].getPos() - webP1.getPos());
                    if(ag < d){
                        idx = i;
                        d = ag;
                        sd = Vector2.SignedAngle(vel, webP1conections[i].getPos() - webP1.getPos());
                    }
                }
                webP1 = webP1conections[idx];
                if(dp1 + dp2 -dp3 > 5*snapTol)
                {
                    trs.position = webP2.getPos();
                }
            }
            else{
            
                webP1 = webP2;
                List<Construction> webP2conections = webP2.getConections();
                for(int i = 0; i < webP2conections.Count; i ++){
                    ag = Vector2.Angle(vel, webP2conections[i].getPos() - webP2.getPos());
                    if(ag < d){
                        idx = i;
                        d = ag;
                        sd = Vector2.SignedAngle(vel, webP2conections[i].getPos() - webP2.getPos());
                    }
                }
                vel = Vector2Extension.Rotate(vel, sd);
                webP2 = webP2conections[idx];
                if(dp1 + dp2 -dp3 > 5*snapTol)
                {
                    trs.position = webP1.getPos();
                }
            }
        }

        dir = (webP1.getPos() - webP2.getPos()).normalized;

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
        Vector2 mouse = FrameControlls.weapDir;//coletar a posição atual do mouse
        float target = Vector2.SignedAngle(LastFrameControlls.weapDir, mouse);
        weaponPivot.transform.localPosition = Vector2.SmoothDamp(weaponPivot.transform.localPosition, wp.range*mouse, ref moment, 10*Time.fixedDeltaTime);
        float a = Mathf.SmoothDampAngle(weaponPivot.localEulerAngles.z, -Vector2.SignedAngle(FrameControlls.weapDir, Vector2.up), ref angularmmt, 10*Time.fixedDeltaTime);
        weaponPivot.transform.localEulerAngles = new Vector3(0, 0, a);
        weaponPoint.transform.localEulerAngles = new Vector3(0, 0, wp.angle);
        weaponPoint.transform.localPosition = Vector2.up*wp.range;	
        LastFrameControlls = FrameControlls;

        ////////////Ações:
        //código de ataque vem aqui
        if(FrameControlls.AtkBtn)
        {
            
        }
        //código defesa vem aqui
        if(FrameControlls.DefBtn)
        {
            
        }
        //código habilidade vem aqui
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
