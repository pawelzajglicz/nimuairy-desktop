using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlashEnemies : Skill
{

    [SerializeField] EnemySlasher enemySlasher;

    private void Awake()
    {
        activationKey = KeyCode.G;
    }

    protected override void ActivateSkill()
    {
        Instantiate(enemySlasher, Camera.main.transform.position, Quaternion.identity);
    }

}