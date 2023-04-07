using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Snowball : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {
            
        }

        /// <summary>
        /// ´«µ¢ÀÌ°¡ À§·Î ±¼·¯°£´Ù
        /// </summary>

        private void SnowballRolling()
        {
            transform.position += Vector3.up * Player_Stat.Instance.AttackPower * 3.0f * Time.deltaTime;
        }

    }
}
