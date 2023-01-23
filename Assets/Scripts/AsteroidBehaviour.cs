using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    public SpriteRenderer spr;
    public Asteroid ast;

    public void Start()
    {
       this.gameObject.GetComponent<SpriteRenderer>().sprite = ast.getSprite();
    }
}
