using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGameLib
{

    public class Pools : MonoBehaviour
    {

        public static Pools Default;

        public Dictionary<string, Pool> PoolDic = new Dictionary<string, Pool>();

        public List<Transform> TempList = new List<Transform>();

        private void Awake()
        {
            if (Default == null)
                Default = this;
        }

        // Use this for initialization
        void Start()
        {

            foreach (Transform tp in TempList)
            {
                AddPool(tp);
            }
        }

        public void AddPool(Transform template, int initCount = 3)
        {

            Pool pool = gameObject.AddComponent<Pool>();
            pool.template = template;
            pool.poolName = template.name;
            pool.initCount = initCount;
            pool.Init();

            PoolDic.Add(pool.poolName, pool);

            //Debug.LogWarning("AddPool -> " + template.name);
        }

        public Pool Get(string poolName)
        {

            Pool pool = null;

            PoolDic.TryGetValue(poolName, out pool);

            return pool;
        }

        public Transform Spawn(string name, Vector3 pos = new Vector3(), Quaternion rot = new Quaternion(), Transform parent = null, bool worldPositionStays = false)
        {

            return PoolDic[name].Spawn(pos, rot, parent, worldPositionStays);
        }
    }

}