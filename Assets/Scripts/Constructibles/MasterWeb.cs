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

	void Start()
	{
		int k = MasterWeb.instance.transform.childCount;
		for(int i = 0; i < k; i++)
		{
			Construction so = MasterWeb.instance.transform.GetChild(i).gameObject.GetComponent<Construction>();
			updateMap(so);
			so.AddConnection(MasterWeb.instance.transform.GetChild((i+k+1)%k).gameObject.GetComponent<Construction>());
			MasterWeb.instance.transform.GetChild((i+k+1)%k).gameObject.GetComponent<Construction>().AddConnection(so);
			List<Construction> conn = new List<Construction>();
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
