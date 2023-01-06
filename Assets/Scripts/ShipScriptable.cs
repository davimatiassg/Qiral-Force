using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObject/Ship")]
public class ShipScriptable : ScriptableObject
{
    public string ShipName;
    public float speed;
    public int life;
    [SerializeField] private List<Sprite> sprites;

    public void OnValidate()
    {
        sprites.Clear();
        for(int i = 1; i <= 4; i++)
        {
            string s = $"Textures/Ships/{ShipName}/{i.ToString()}";
            Debug.Log(s);
            var sp  = Resources.Load<Sprite>(s);
            sprites.Add(sp);
        }
        
    }

    public (Sprite, int) getFace(Vector2 dir)
    {
        if(dir.y < 0)
        {
            return (sprites[0], -1);
        }
        else if(dir.y > 0)
        {
            return (sprites[2], 1);
        }
        else if(dir.x > 0)
        {
            return (sprites[3], -1);
        }
        else 
        {
            return (sprites[1], -1);
        }
    }
}
