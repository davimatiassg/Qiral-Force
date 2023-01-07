using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{

    public GameObject coin;
    public Transform player;
    public Transform core;
    public string tipo;
    public float hp = 10;
    public float spd = 1.5f;
    public float dmg = 1;
    public float atkRange = 3;
    public int coins = 1;


    [SerializeField] private Vector3 momentum = new Vector3(0,0,0);
    private float atrito = 1f;

    float sign(float v)
    {
        if (v == 0)
        {
            return 0;
        }
        else {
            return Mathf.Abs(v)/v;
        }
    }

    void dropCoins(int qnt)
    {
        Instantiate(coin,transform.position,transform.rotation);
    }

    ///////Movements

    void JaninhaMov()
    {
        transform.position = Vector3.MoveTowards(transform.position, core.position, spd*Time.deltaTime);
    }

    public float aaa = 20f;

    void AbelhaMov()
    {
        //atrito = 10f;
        if (Random.Range(0, 35) == 0)
        {
            Vector3 after = Vector3.MoveTowards(transform.position, core.position, spd);
            Vector2 speed = new Vector2 (transform.position.x - after.x, transform.position.y - after.y);
            speed = Vector2.Perpendicular(speed)*Random.Range(0.4f,1f);
            
            if (Random.Range(0f,1f) > 0.5f)
            {
                speed *= -1;
            }

            momentum = new Vector3 (speed.x,speed.y,0)*aaa;
        }   
        else {
            transform.position = Vector3.MoveTowards(transform.position, core.position, spd*Time.deltaTime);
        }
    }

    private float jumpRange = 4f;
    private float jumpStrenght = 12f;
    private float jumpCooldown = 3; //em segundos
    private float rJmp = 0;
    void GafanhotoMov()
    {
        
        //if there is something in the way between me and the player, then jump
        RaycastHit2D hitted = Physics2D.Linecast(transform.position,core.position);
        if (hitted && Vector3.Distance(transform.position,player.position) <= jumpRange)
        {
            if (rJmp <= 0)
            {
                momentum = (core.position - transform.position).normalized*jumpStrenght;
                rJmp = jumpCooldown;
            }
        }
        else { //else, just keep moving forward
            transform.position = Vector3.MoveTowards(transform.position, core.position, spd*Time.deltaTime);
        }

        if (rJmp > 0) {rJmp -= Time.deltaTime;}
        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            dropCoins(coins);
            Destroy(this.gameObject);
        }

        if (Vector3.Distance(transform.position,core.position) <= 10)
        {
            //core.takeDmg()
            //Destroy(this.gameObject);
        }

        if (tipo == "Janinha")
        {
            JaninhaMov();
            if (Vector3.Distance(transform.position,player.position) >= atkRange)
            {
                //deal damage
            }
        }

        if (tipo == "Abelha")
        {
            if (Vector3.Distance(transform.position,player.position) >= atkRange)
            {
                AbelhaMov();
            }
            else {
                //deal damage
            }
        }

        if (tipo == "Gafanhoto")
        {
            GafanhotoMov();
        }

        transform.position += momentum * Time.deltaTime;

        Vector3 atr = new Vector3(momentum.x,momentum.y,0)*Time.deltaTime*atrito;
        momentum -= atr;

    }
}
