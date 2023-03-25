using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GJ
{
    public class Button : MonoBehaviour
    {
        // GameObject thisBtn;
        string name;

        void Start()
        {
            name = gameObject.name;
        }

        void Update()
        {
            this.gameObject.SendMessage(name, this);
        }
    }
}
