using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Graphs;
using UnityEngine;

namespace GJ
{
    public class StoryText : MonoBehaviour
    {
        public float storySpeed;
        public float storySizeZ;

        private Vector2 startPos;

        void Start()
        {
            startPos = this.GetComponent<RectTransform>().anchoredPosition;
        }

        void Update()
        {
            if(this.GetComponent<RectTransform>().anchoredPosition.y < storySizeZ) 
            {
                this.GetComponent<RectTransform>().anchoredPosition += Vector2.up * Time.deltaTime * storySpeed;
            }
            else
            {
                this.GetComponent<RectTransform>().anchoredPosition = startPos;
            }
        }
    }
}
