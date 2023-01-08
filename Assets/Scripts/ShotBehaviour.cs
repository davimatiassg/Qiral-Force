using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{

    public string target = "Enemy";
    public float dmg = 1;
    public float speed = 20f;
    public float durationTime = 1f;//in seconds
    public Vector3 dir;

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == target)
        {
            Vector2 knbDir = (collision.gameObject.transform.position - transform.position).normalized;
            float knbStrenght = 0f; //Knockback n ficou legal sla

            if (target == "Player") {collision.gameObject.GetComponent<GamePlayer>().takeDmg(dmg,Vector2.zero);}
            if (target == "Enemy")
            {
                collision.gameObject.GetComponent<Behaviour>().takeDmg(dmg,knbDir*knbStrenght);
                GameObject.FindGameObjectWithTag("EnemyAudioPlayer").GetComponent<enemyAudioScript>().playAudio(
                collision.gameObject.GetComponent<Behaviour>().tipo.ToLower() + "Dmg");
            }
            Destroy(this.gameObject);
        }

    }

    // Start is called before the first frame update
    public virtual void Start()
    {
        transform.eulerAngles = Vector3.forward*Vector2.SignedAngle(Vector2.up, dir);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        transform.eulerAngles = Vector3.MoveTowards(transform.eulerAngles, Vector3.forward*Vector2.SignedAngle(Vector2.up, dir), Time.deltaTime*speed);
        durationTime -= Time.deltaTime;
        if (durationTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
