using UnityEngine;

namespace GJ
{
    public class Player : MonoBehaviour
    {
        private Animator playeranimator;                    // �÷��̾� �ִϸ�����
        protected Player_Input playerInput;                 // �÷��̾ �����̱� ���� ��ǲ

        void Awake()
        {
            playerInput = GetComponent<Player_Input>();
        }
    }
}
