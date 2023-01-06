using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMov : MonoBehaviour
{
    public float speed = 2.5f;
    public int life = 100;

    private Transform trs;
    private SpriteRenderer spr;
    private TrailRenderer trail;

    public GameObject weaponPoint;
    public Transform weaponPivot;
    private WeaponHandler wp;

    public WeaponScriptable Weapon;
	public ShipScriptable Ship;

    public bool isAtk = false;
    public float atkCD = 0;
    public float vel = 0;
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

    
    void Start()
    {
    	wp = weaponPoint.GetComponent<WeaponHandler>();
        trs = this.gameObject.GetComponent<Transform>();
        spr = this.gameObject.GetComponent<SpriteRenderer>();
        trail = this.gameObject.GetComponent<TrailRenderer>();
        changeWeapon();
        changeShip();
    }

    void FixedUpdate()
    {
    	FrameControlls = P_Controll.PassInput();

		if(FrameControlls.movDir != LastFrameControlls.movDir && FrameControlls.movDir != Vector2.zero){
			var (s, o) = Ship.getFace(FrameControlls.movDir);
    		spr.sprite= s;
    		trail.sortingOrder = o;
    	}
        trs.Translate(FrameControlls.movDir*speed);//andar marotamente
        Vector2 mouse = FrameControlls.weapDir;//coletar a posição atual do mouse
        float target = Vector2.SignedAngle(LastFrameControlls.weapDir, mouse);
        if(FrameControlls.AtkBtn && atkCD <= 0)
        {

            if(atkCharge == Vector2.zero)
            {
                atkCharge = FrameControlls.weapDir;
            }
            //weaponPoint.transform.localPosition = Vector2.up*wp.range; 
            //weaponPivot.transform.localEulerAngles = new Vector3(0, 0, Mathf.SmoothDampAngle(weaponPivot.transform.localEulerAngles.z, -Vector2.SignedAngle(FrameControlls.weapDir, Vector2.up), ref angularmmt, 50*Time.fixedDeltaTime));
            //weaponPoint.transform.localEulerAngles = Vector3.zero;

        }
        else if (LastFrameControlls.AtkBtn == true && atkCD <= 0)
        {
        	atkCD = wp.cd;
            isAtk = true;
            float a1 = Vector2.SignedAngle(atkCharge, Vector2.up);
            float a2 = Vector2.SignedAngle(FrameControlls.weapDir, Vector2.up);
            float a = Mathf.DeltaAngle(a1, a2);
            WeaponATg = -Vector2.SignedAngle(FrameControlls.weapDir, Vector2.up);
            pivotBack = weaponPivot.transform.localPosition;
            angularmmt = 0;

            //weaponPivot.transform.localEulerAngles = new Vector3(0, 0, -Vector2.SignedAngle(atkCharge, weaponPivot.transform.up) + Vector2.SignedAngle(atkCharge, Vector2.up)/10);
            atkCharge = Vector2.zero;
        }
        else if(isAtk)
        {
        	//achar o ângulo entre o mouse do ultimo frame e o atual e adicionar o momentum a esse ângulo
        	//weaponPoint.transform.localPosition = Vector2.SmoothDamp(weaponPivot.transform.localPosition, wp.range*Vector2.up, ref moment, atkCD/5);
            //Debug.Log("Ângulo: " + weaponPivot.localEulerAngles.z + "°; Alvo: " +WeaponATg+"°.");
        	float a = Mathf.SmoothDampAngle(weaponPivot.localEulerAngles.z, WeaponATg, ref angularmmt, 2*Time.fixedDeltaTime);
            weaponPivot.transform.localPosition = Vector2.SmoothDamp(weaponPivot.transform.localPosition, -pivotBack.normalized*wp.range, ref moment, 2*Time.fixedDeltaTime);
	        weaponPivot.localEulerAngles = new Vector3(0, 0, a);

	        atkCD -= Time.fixedDeltaTime;
	        if(atkCD <= 0)
	        {
	        	isAtk = false;
                pivotBack = Vector2.zero;
	        }
        }
        else
        {
        	
        	weaponPivot.transform.localPosition = Vector2.SmoothDamp(weaponPivot.transform.localPosition, wp.range*mouse, ref moment, 10*Time.fixedDeltaTime);
            float a = Mathf.SmoothDampAngle(weaponPivot.localEulerAngles.z, -Vector2.SignedAngle(FrameControlls.weapDir, Vector2.up), ref angularmmt, 10*Time.fixedDeltaTime);
            //Debug.Log("Ângulo: " + weaponPivot.localEulerAngles.z + "°; Alvo: " +(-Vector2.SignedAngle(FrameControlls.weapDir, Vector2.up))+"°.");
        	weaponPivot.transform.localEulerAngles = new Vector3(0, 0, a);
        	weaponPoint.transform.localEulerAngles = new Vector3(0, 0, wp.angle);
            weaponPoint.transform.localPosition = Vector2.up*wp.range;
            
        	
        }
        LastFrameControlls = FrameControlls;
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

    public void PrepAtk()
    {

 		
    }
    public void CancelAtk()
    {
    	atkCharge = Vector2.zero;
    }
}
