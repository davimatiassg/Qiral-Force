using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterWeb : MonoBehaviour
{
	public static List<Constructible> map = new List<Constructible>();

	public List<Constructible> publicMap = new List<Constructible>();

	public static MasterWeb instance;

	public static List<Constructible> findClosestConstructs(Constructible target, float range)
	{
		List<Constructible> l = new List<Constructible>();
		foreach(Constructible mapT in map)
		{
			if((target.pos - mapT.pos).magnitude < range)
			{
				l.Add(mapT);
			}
		}
		return l;
	}

	void Awake()
	{
		MasterWeb.map = publicMap;
		if(instance)
		{
			Destroy(this.gameObject);
		}
		else
		{
			instance = this;
		}
	}

	public static void updateMap(Constructible c)
	{
		map.Add(c);
	}

	public List<Constructible> getMap()
	{
		return MasterWeb.map;
	}
}
