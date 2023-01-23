using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public Transform player;
    public GameObject Controller;
    public float spd = 0.5f;
    public float acel = 3f;
    public float a_acel = 5f;
    private float rCoin = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Controller = GameObject.FindGameObjectWithTag("GameController");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Controller.GetComponent<ControllerScript>().AddCoins(1);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Coin spinning
        transform.localScale = new Vector3(Mathf.Sin(rCoin),1,1);
        if (rCoin >= Mathf.PI*2) {rCoin = 0;}
        rCoin += 30*Time.deltaTime;

        //Movement
        spd += acel*Time.deltaTime;
        transform.up = Vector3.RotateTowards(transform.up,(player.position - transform.position), spd*5, 0);
        transform.Translate(Vector2.up*spd);
        
    }
}
