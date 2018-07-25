using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UGameLib;

public class PoolItemTest : PoolItem {

    override public void Free()
    {
        isFree = true;

        gameObject.SetActive(!isFree);
    }

    override public void Busy()
    {
        isFree = false;

        gameObject.SetActive(!isFree);
    }

}
