using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class WeaponHandler : MonoBehaviour
{
    public SpriteRenderer spr;
    public Transform trs;
    public WeaponScriptable Weapon;

    public float Cd;
    
    public void updateWeapon()
    {
        spr.sprite = Weapon.spr;
    }

    public void Shot(Vector2 dir)
    {
        if(Cd <= 0)
        {
            GameObject g = Instantiate(Weapon.Shot, trs.position, new Quaternion(0,0,0,0));
            g.transform.eulerAngles = Vector3.forward*Vector2.SignedAngle(Vector2.up, dir);
            g.GetComponent<ShotBehaviour>().dir = dir.normalized;
            Cd = Weapon.Cd;
        }
    }

    void Update()
    {
        Cd -= Time.deltaTime;
    }

}
