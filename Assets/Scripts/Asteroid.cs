using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Asteroid")]
public class Asteroid : ScriptableObject
{
    public List<Sprite> sprites = new List<Sprite>();
    public Sprite sprite;

    public Sprite getSprite()
    {
        return sprites[Mathf.RoundToInt(Random.value*(sprites.Count -1))];
    }
   
}
