using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace UGameLib
{
    public interface IPoolItem
    {
        bool IsFree();
        void Free();
        void Busy();
    }

    [Serializable]
    public class Pool : MonoBehaviour
    {

        public string poolName;

        public int initCount;

        public Transform template;

        List<Transform> freeList = new List<Transform>();

        LinkedList<PoolItem> busyList = new LinkedList<PoolItem>();

        LinkedListNode<PoolItem> curListNode;
        LinkedListNode<PoolItem> curListNodeNext;

        public void Init()
        {
            Transform objTransform;
            for (int i = 0; i < initCount; i++)
            {
                objTransform = Instantiate(template, Vector3.zero, Quaternion.identity) as Transform;
                Free(objTransform);
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        public Transform Spawn(Vector3 pos = new Vector3(), Quaternion rot = new Quaternion(), Transform parent = null, bool worldPositionStays = false)
        {

            Transform objTransform;

            if (freeList.Count > 0)
            {
                objTransform = freeList[freeList.Count - 1];

                freeList.RemoveAt(freeList.Count - 1);
            }
            else
            {

                objTransform = Instantiate(template, Vector3.zero, Quaternion.identity) as Transform;
            }

            if (parent)
            {
                //objTransform.transform.parent = parent;
                objTransform.transform.SetParent(parent, worldPositionStays);
            }

            objTransform.position = pos;
            objTransform.rotation = rot;

            objTransform.gameObject.SetActive(true);

            PoolItem poolItem = objTransform.GetComponent<PoolItem>();

            if (poolItem)
            {
                busyList.AddFirst(poolItem);

                poolItem.Busy();
            }

            return objTransform;
        }

        public void Free(Transform objTransform)
        {

            objTransform.gameObject.SetActive(false);
            freeList.Add(objTransform);

        }

        bool CheckFree(Transform objTransform)
        {

            PoolItem poolItem = objTransform.GetComponent<PoolItem>();

            if (poolItem)
            {
                return poolItem.IsFree();
            }

            return !objTransform.gameObject.activeSelf;
        }

        private void Update()
        {
            if (curListNode == null)
            {

                curListNode = busyList.First;

            }
            else
            {

                if (curListNode.Value.isFree)
                {

                    freeList.Add(curListNode.Value.transform);

                    curListNodeNext = curListNode.Next;

                    busyList.Remove(curListNode);

                    curListNode = curListNodeNext;

                    curListNodeNext = null;

                }
                else
                {

                    curListNode = curListNode.Next;
                }
            }

        }
    }

}