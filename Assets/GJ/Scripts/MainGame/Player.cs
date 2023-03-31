using UnityEngine;

namespace GJ
{
    public class Player : MonoBehaviour
    {
        private Animator playeranimator;                    // 플레이어 애니메이터
        protected Player_Input playerInput;                 // 플레이어를 움직이기 위한 인풋

        void Awake()
        {
            playerInput = GetComponent<Player_Input>();
        }
    }
}
