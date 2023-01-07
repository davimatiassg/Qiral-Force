using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{
    public float hp = 50f;
    
    public void takeDmg(float dmg,Vector2 knb)
    {
        hp -= dmg;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllerScript>().updatePlayerHp(hp);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
        {
            hp = 0f;
        }
    }
}
