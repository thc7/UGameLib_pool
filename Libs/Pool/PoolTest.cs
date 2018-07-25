using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UGameLib;

public class PoolTest : MonoBehaviour {

    public Pools pools;
    public Transform[] poolObj;

	// Use this for initialization
	void Start () {
		
        foreach (Transform transformPool in poolObj)
        {
            pools.AddPool(transformPool);
        }
	}
	
    void OnGUI()
    {
        if (GUI.Button(new Rect(0, 0, 120, 50), " Load cube "))
        {
            pools.PoolDic["Cube"].Spawn();
        }

        if (GUI.Button(new Rect(0, 60, 120, 50), " Sphere_PoolItem "))
        {
            pools.Spawn("Sphere_PoolItem");
        }
	}
}
