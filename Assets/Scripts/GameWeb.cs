using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWeb : MonoBehaviour
{

    public float hp = 100;

    public void takeDmg(float dmg,Vector2 knb)
    {
        hp -= dmg;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllerScript>().updateCoreHp(hp);
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
            hp = 0;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllerScript>().updateCoreHp(hp);
        }
    }
}
