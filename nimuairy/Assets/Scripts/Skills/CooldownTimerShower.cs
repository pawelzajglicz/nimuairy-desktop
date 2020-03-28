using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTimerShower : MonoBehaviour
{
    [SerializeField] public float cooldown;    
    [SerializeField] public float timeSkillUsed;

    [SerializeField] PercentageBar cooldownBar;

    [SerializeField] Skill skill;

    private void Start()
    {
        skill = GetComponent<Skill>();     
    }

    private void Update()
    {
        cooldownBar.SetPercentage(skill.cooldownRemaining * 100 / skill.cooldown);
    }
}
