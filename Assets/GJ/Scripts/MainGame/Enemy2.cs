using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GJ
{
    public class Enemy2 : Enemy
    {
        protected override void Start()
        {
            m_type = ENEMY_TYPE.ENEMY2;
            EnemySpeed = 2.0f;
            EnemyAttSpeed = 2.5f;
            EnemyAttPower = 1;
            EnemyHp = 6;

            base.Start();
            delayValeue = (EnemySpeed / EnemyHp) * 0.7f;    // 딜레이값 초기화
        }
    }
}