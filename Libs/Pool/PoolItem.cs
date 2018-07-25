using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UGameLib
{

    public class PoolItem : MonoBehaviour, IPoolItem
    {

        public float lifeTime; // Despawn delay in ms
        public float runTime;
        public bool useLifeTime;

        public bool isFree = true;

        public static void AddTo<T>(Transform tran) where T : PoolItem
        {
            if (tran && tran.GetComponent<T>() == null)
            {
                tran.gameObject.AddComponent<T>();
            }
        }

        virtual public bool IsFree()
        {
            return isFree;
        }

        virtual public void Free()
        {
            isFree = true;
        }

        virtual public void Busy()
        {
            isFree = false;
        }

        void Update()
        {
            if (useLifeTime && !isFree)
            {

                runTime += Time.deltaTime;

                if (runTime > lifeTime)
                {

                    Free();

                    runTime = 0;
                }
            }
        }
    }

}