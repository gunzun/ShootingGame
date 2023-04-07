using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace GJ
{
    public class UI_Score : MonoBehaviour
    {
        TextMeshProUGUI TmPro;

        void Start()
        {
            TmPro = GetComponent<TextMeshProUGUI>();
        }
        void Update()
        {
            TmPro.text = Player_Stat.Instance.CurrentScore.ToString();
        }
    }
}
