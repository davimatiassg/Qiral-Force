using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behaviour : MonoBehaviour
{

    public GameObject coin;
    public GameObject Bullet;
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

    //Baguis da lagarta/casulo/demonio
    public GameObject Casulo;
    public GameObject Demonio;
    private static float metamorphosisTime = 5f;//em segundos
    private float growSpd = 1f/metamorphosisTime;

    //Math functions

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

    float angle(Vector2 vet)
    {
        float c = 180/Mathf.PI;
        float a = vet.y/Mathf.Sqrt(vet.x*vet.x + vet.y*vet.y);

        if (vet.x >= 0 && vet.y >= 0)
        {
            return c*Mathf.Asin(a);
        }
        if (vet.x <= 0 && vet.y >= 0)
        {
            return 90 + c*Mathf.Acos(a);
        }
        if (vet.x <= 0 && vet.y <= 0)
        {
            return 180 + c*Mathf.Asin(-a);
        }
        if (vet.x >= 0 && vet.y <= 0)
        {
            return 270 + c*Mathf.Acos(-a);
        }

        return 0f;
    }

    //Other functions

    void dropCoins(int qnt)
    {
        for (int i = 0;i < qnt;i ++)
        {
            Vector3 rndPos = new Vector3 (Random.Range(-2,2),Random.Range(-2,2),0);
            Instantiate(coin,transform.position + rndPos,transform.rotation);
        }
    }

    public void takeDmg(float dmg,Vector2 knb)
    {
        hp -= dmg;
        momentum += (Vector3) knb;
    }

    ///////Movements

    void JaninhaMov()
    {
        transform.position = Vector3.MoveTowards(transform.position, core.position, spd*Time.deltaTime);
    }

    private float zigZagStrenght = 3f;
    private int zigZagRate = 35;
    void AbelhaMov()
    {
        if (Random.Range(0, zigZagRate) == 0)
        {
            Vector3 future_pos = Vector3.MoveTowards(transform.position, core.position, spd);
            Vector2 speed = new Vector2 (transform.position.x - future_pos.x, transform.position.y - future_pos.y);
            speed = Vector2.Perpendicular(speed)*Random.Range(0.5f,1f);
            
            if (Random.Range(0,2) == 0) {speed *= -1;} //50% chance

            momentum = new Vector3 (speed.x,speed.y,0)*zigZagStrenght;
        }   
        else {
            transform.position = Vector3.MoveTowards(transform.position, core.position, spd*Time.deltaTime);
        }
    }

    private float jumpRange = 10f;
    private float jumpStrenght = 32f;
    private float jumpCooldown = 3f; //em segundos
    private float jumpFreioCooldown = 0.7f; //em segundos
    private float rJmp,rJmpFreio = 0;
    void GafanhotoMov()
    {
        //if there is something in the way between me and the player, then jump
        RaycastHit2D hitted = Physics2D.Linecast(transform.position,core.position);
        if (hitted && Vector3.Distance(transform.position,player.position) <= jumpRange)
        {
            if (rJmp <= 0)
            {
                switch(Random.Range(0, 3))
                {
                    case 0:
                        momentum = (core.position - transform.position).normalized*jumpStrenght;
                    break;

                    case 1:
                        momentum = (core.position - transform.position).normalized*jumpStrenght;
                        momentum = (Vector3) Vector2.Perpendicular(momentum);
                    break;
                    
                    case 2:
                        momentum = (core.position - transform.position).normalized*jumpStrenght;
                        momentum = (Vector3) Vector2.Perpendicular(momentum)*-1;
                    break;
                }
                //momentum = (core.position - transform.position).normalized*jumpStrenght;

                rJmp = jumpCooldown;
                rJmpFreio = jumpFreioCooldown;
            }
        }
        else { //else, just keep moving forward
            transform.position = Vector3.MoveTowards(transform.position, core.position, spd*Time.deltaTime);
        }

        if (rJmp > 0) {rJmp -= Time.deltaTime;}
        if (rJmpFreio <= 0) {atrito = 3f;} else {atrito = 1f;rJmpFreio -= Time.deltaTime;}
        
    }

    private Vector2 wanderDir = Vector2.zero;
    private int wanderRate = 100;
    void BraboletaMov()
    {
        if (Random.Range(0, wanderRate) == 0)
        {
            if (Random.Range(0, 2) == 0) //50% chance
            {
                wanderDir = new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
            }
            else {
                wanderDir = (player.position - transform.position).normalized;
            }
        }
        transform.position += (Vector3) wanderDir*Time.deltaTime*spd;
    }

    void DemonioMov()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, spd*Time.deltaTime);
    }

    //Attacks

    private float firingRate = 0.2f;//em segundos
    private float rFire = 0;
    void AbelhaFire(GameObject bullet,Vector2 pos)
    {
        if (rFire <= 0)
        {
            GameObject shot = Instantiate(bullet,transform.position,transform.rotation);
            shot.GetComponent<BulletScript>().dir = (player.position - transform.position).normalized;
            shot.GetComponent<BulletScript>().target = "Player";
            shot.GetComponent<SpriteRenderer>().color = new Color(0.5f,0f,0.5f,1f);
            rFire = firingRate;
        }
        rFire -= Time.deltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        core = GameObject.FindGameObjectWithTag("Core").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            dropCoins(coins);
            Destroy(this.gameObject);
        }

        if (Vector3.Distance(transform.position,core.position) <= 0.5f)
        {
            GameObject.FindGameObjectWithTag("Core").GetComponent<GameWeb>().takeDmg(dmg,Vector2.zero);
            Destroy(this.gameObject);
        }

        //Movement

        if (tipo == "Janinha")
        {
            JaninhaMov();
            if (Vector3.Distance(transform.position,player.position) <= atkRange)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<GamePlayer>().takeDmg(dmg*Time.deltaTime,Vector2.zero);
            }
        }

        if (tipo == "Abelha")
        {
            if (Vector3.Distance(transform.position,player.position) <= atkRange
             && Vector2.Distance(transform.position,core.position) >= Vector2.Distance(player.position,core.position))
            {
                AbelhaFire(Bullet,(player.position - transform.position).normalized);
            }
            else {
                AbelhaMov();
            }
        }

        if (tipo == "Gafanhoto")
        {
            GafanhotoMov();
        }

        if (tipo == "Besouro")
        {
            JaninhaMov(); //é o mesmo tipo de movimento q a janinha
        }

        if (tipo == "Braboleta")
        {
            BraboletaMov();
            if (Vector3.Distance(transform.position,player.position) <= atkRange)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<GamePlayer>().takeDmg(dmg*Time.deltaTime,Vector2.zero);
                Debug.Log("Dano de braboleta!!!!!!!!!!!!");
            }
        }

        if (tipo == "Lagarta")
        {
            JaninhaMov(); //é o mesmo tipo de movimento q a janinha
            if (Vector3.Distance(transform.position,player.position) <= atkRange)
            {
                Instantiate(Casulo,transform.position,transform.rotation);
                Destroy(this.gameObject);
            }
        }

        if (tipo == "Casulo")
        {
            transform.localScale = new Vector3(transform.localScale.x + growSpd*Time.deltaTime,transform.localScale.x + growSpd*Time.deltaTime,1);
            if (metamorphosisTime <= 0)
            {
                Instantiate(Demonio,transform.position,transform.rotation);
                Destroy(this.gameObject);
            }
            metamorphosisTime -= Time.deltaTime;
        }

        if (tipo == "Demonio")
        {
            DemonioMov();
            if (Vector3.Distance(transform.position,player.position) <= atkRange)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<GamePlayer>().takeDmg(dmg*Time.deltaTime,Vector2.zero);
            }
        }

        transform.position += momentum * Time.deltaTime;

        Vector3 atr = new Vector3(momentum.x,momentum.y,0)*Time.deltaTime*atrito;
        momentum -= atr;

    }
}