using Codice.CM.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Background_Scroll : MonoBehaviour
    {
        private float speed;

        void Update()
        {
            this.transform.position -= new Vector3(0, Time.deltaTime * speed, 0);
        }
    }
}
