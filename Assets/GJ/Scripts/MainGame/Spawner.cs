using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public abstract class Spawner : MonoBehaviour
    {
        private float randomSpanwPosMin;        // ���� ���� ��ġ �ּڰ�
        private float randomSpanwPosMax;        // ���� ���� ��ġ �ִ�

        protected float count;                  // ��ü�� �����Ǳ������ ī��Ʈ

        #region #Property
        protected float RandomSpanwPosMin { get => transform.GetChild(0).position.x; }
        protected float RandomSpanwPosMax { get => transform.GetChild(1).position.x; }
        #endregion

        #region # Spawner���� ��ü�� �����ϴ� �Լ��� �����ϵ��� �����Ѵ�.
        abstract protected void Generator();
        abstract protected void RandomizeType();
        #endregion

        /// <summary>
        /// ����ȭ �� ��ü�� X ���� �Ķ���ͷ� �ش�.
        /// </summary>
        protected float SpawnPositionRandomization()
        {
            float xPosValue;
            xPosValue = Random.Range(RandomSpanwPosMin, RandomSpanwPosMax);
            return xPosValue;
        }

        private void OnTriggerEnter(Collider other)
        {
            // �Ѿ��� �����ʺ��� ���� ���� �ʵ��� �ı�
            if (other.tag == "Bullet")
            {
                Destroy(other.gameObject);
            }
        }
    }
}