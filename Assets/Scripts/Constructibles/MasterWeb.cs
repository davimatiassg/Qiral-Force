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

	public static void defaultMap(string n)
	{
		int k = MasterWeb.instance.transform.childCount;
		if(n == MasterWeb.instance.transform.GetChild(k-1).gameObject.name)
		{
			for(int i = 0; i < k; i++)
			{
				SolidObject so = MasterWeb.instance.transform.GetChild(i).gameObject.GetComponent<SolidObject>();
				updateMap(so.consData);
				so.conections.Add(MasterWeb.instance.transform.GetChild((i+k+1)%k).gameObject.GetComponent<SolidObject>().consData);
				so.conections.Add(MasterWeb.instance.transform.GetChild((i+k-1)%k).gameObject.GetComponent<SolidObject>().consData);
				List<Construction> conn = new List<Construction>();
		        if(so.conections.Count > 0)
		        {
		            for(int j = 0; j < so.conections.Count; j++)
		            {
		                conn.Add((Construction)so.conections[j]);
		            }
		            so.consData.setConections(conn);
		        }
			}
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
