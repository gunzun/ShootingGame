using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public abstract class Spawner : MonoBehaviour
    {
        private float randomSpanwPosMin;        // 랜덤 스폰 위치 최솟값
        private float randomSpanwPosMax;        // 랜덤 스폰 위치 최댓값

        protected float count;                  // 객체가 생성되기까지의 카운트

        #region #Property
        protected float RandomSpanwPosMin { get => transform.GetChild(0).position.x; }
        protected float RandomSpanwPosMax { get => transform.GetChild(1).position.x; }
        #endregion

        #region # Spawner에서 객체를 스폰하는 함수를 생성하도록 강제한다.
        abstract protected void Generator();
        abstract protected void RandomizeType();
        #endregion

        /// <summary>
        /// 랜덤화 할 객체의 X 값을 파라미터로 준다.
        /// </summary>
        protected float SpawnPositionRandomization()
        {
            float xPosValue;
            xPosValue = Random.Range(RandomSpanwPosMin, RandomSpanwPosMax);
            return xPosValue;
        }

        private void OnTriggerEnter(Collider other)
        {
            // 총알이 스포너보다 위로 가지 않도록 파괴
            if (other.tag == "Bullet")
            {
                Destroy(other.gameObject);
            }
        }
    }
}