
using System;
using UnityEngine;

public class SelfDeffender : MonoBehaviour
{

    [SerializeField] float probabilityOfSelfDeffence = 0.1f;

    [SerializeField] GameObject selfDeffenceVfx;
    [SerializeField] float durationOfselfDeffence = 1.5f;

    [SerializeField] AudioClip selfDeffenceSound;
    [SerializeField] float selfDeffenceSoundVolume = 0.5f;

    private FieldDefenderAttacking fieldDefenderAttacking;
    [SerializeField] float attackMultiplier = 5f;

    private void Start()
    {
        fieldDefenderAttacking = FindObjectOfType<FieldDefenderAttacking>();
    }

    internal bool TrySelfDeffence(GameObject attacker)
    {
        float generatedValue = UnityEngine.Random.Range(0, 1f);

        if (generatedValue > probabilityOfSelfDeffence)
        {
            return false;
        }
        else
        {
            SelfDeffence(attacker);
            return true;
        }
    }

    private void SelfDeffence(GameObject attacker)
    {
        DealDamage(attacker);
        MakeAttack(attacker);
    }

    private void DealDamage(GameObject attacker)
    {
        Health health = attacker.GetComponent<Health>();
        if (health)
        {
            health.DealDamage(fieldDefenderAttacking.slowAttack.attackPower * attackMultiplier);
        }
    }

    private void MakeAttack(GameObject attacker)
    {
        GameObject vfx = Instantiate(selfDeffenceVfx, attacker.transform.position, Quaternion.identity);
        //Destroy(vfx, durationOfselfDeffence);
        AudioSource.PlayClipAtPoint(selfDeffenceSound, Camera.main.transform.position, selfDeffenceSoundVolume);
    }
}
