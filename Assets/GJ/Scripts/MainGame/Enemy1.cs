using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace GJ
{
    public class Enemy1 : Enemy
    {
        private void Start()
        {
            m_type = ENEMY_TYPE.ENEMY1;
            EnemySpeed = 3.0f;
            EnemyAttSpeed = 1.5f;
            EnemyAttPower = 1;
            EnemyHp = 4;
            enemyOriginHp = EnemyHp;                        // ���� ������� ���� �⺻ ü��


        delayValeue = (EnemySpeed / EnemyHp) * 0.8f;        // �����̰� �ʱ�ȭ
        }
    }
}

