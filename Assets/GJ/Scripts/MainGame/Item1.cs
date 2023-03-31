using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Item1 : Item
    {
        private void Start()
        {
            speed = 3.0f;
            m_Type = ITEM_TYPE.HP;
        }
    }
}
