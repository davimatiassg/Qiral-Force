using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PewPew : MonoBehaviour
{

    public float firingRate = 0.15f;
    public float rFire = 0f;
    private bool fire = false;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)) {fire = true;}
        if (Input.GetKeyUp(KeyCode.Q)) {fire = false;}

        if (fire)
        {
            if (rFire <= 0)
            {
                GameObject shot = Instantiate(bullet,transform.position,transform.rotation);

                Vector2 mouse = new Vector2(Input.mousePosition.x - Screen.width/2, Input.mousePosition.y - Screen.height/2);
                shot.GetComponent<BulletScript>().dir = (mouse - (Vector2) transform.position).normalized;
                shot.GetComponent<BulletScript>().target = "Enemy";
                rFire = firingRate;
            }
            rFire -= Time.deltaTime;
        }
        else {
            rFire = firingRate;
        }
    }
}
