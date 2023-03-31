using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace GJ
{
    public class Enemy3 : Enemy
    {
        private void Start()
        {
            m_type = ENEMY_TYPE.ENEMY3;
            EnemySpeed = 6.0f;
            EnemyAttSpeed = 2.5f;
            EnemyAttPower = 1;
            EnemyHp = 3;

            enemyOriginHp = EnemyHp;                            // 적의 변경되지 않은 기본 체력
            delayValeue = (EnemySpeed / EnemyHp) * 0.9f;        // 딜레이값 초기화
        }
    }
}

