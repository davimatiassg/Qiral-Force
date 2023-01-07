using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterWeb : MonoBehaviour
{
	public static List<Construction> map = new List<Construction>();

	public List<Construction> publicMap = new List<Construction>();

	public static MasterWeb instance;

	public int WebSize;

	public static List<Construction> findClosestConstructs(Construction target, float range)
	{
		List<Construction> l = new List<Construction>();
		foreach(Construction mapT in map)
		{
			if((target.getPos() - mapT.getPos()).magnitude < range)
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

	public static void updateMap(Construction c)
	{
		map.Add(c);
	}

	public List<Construction> getMap()
	{
		return MasterWeb.map;
	}


}
