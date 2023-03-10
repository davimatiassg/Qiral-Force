using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    public string target = "Enemy";
    public float dmg = 1;
    public float spd = 20f;
    private float flightTime = 5f;//in seconds
    public Vector3 dir;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == target)
        {
            if (target == "Player") {collision.gameObject.GetComponent<GamePlayer>().takeDmg(dmg,Vector2.zero);}
            if (target == "Enemy") {collision.gameObject.GetComponent<Behaviour>().takeDmg(dmg,Vector2.zero);}
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir*spd*Time.deltaTime;
        flightTime -= Time.deltaTime;

        if (flightTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
