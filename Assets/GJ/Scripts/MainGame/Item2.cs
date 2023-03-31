using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Item2 : Item
    {
        private void Start()
        {
            speed = 4.0f;
            m_Type = ITEM_TYPE.ATTACKSPEED;
        }
    }
}
