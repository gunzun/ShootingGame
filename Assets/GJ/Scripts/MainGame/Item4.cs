using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Item4 : Item
    {
        private void Start()
        {
            speed = 2.0f;
            m_Type = ITEM_TYPE.BOMB;
        }
    }
}
