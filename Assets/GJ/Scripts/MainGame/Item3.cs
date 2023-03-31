using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    [SerializeField]
    public class Item3 : Item
    {
        private void Start()
        {
            speed = 4.0f;
            m_Type = ITEM_TYPE.ATTACKPOWER;
        }
    }
}
