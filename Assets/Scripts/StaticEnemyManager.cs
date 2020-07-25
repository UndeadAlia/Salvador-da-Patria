using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyManager : MonoBehaviour
{
    public static StaticEnemyManager instance;

    [Header("Enemy Stats")]
    //enemy health & exp system
    public int currentHP;
    public int maxHP;
    public int expToGive;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            PlayerStats.instance.AddExp(expToGive);
        }
    }
    public void HurtEnemy(int damageToGive)
    {
        currentHP -= damageToGive;

    }
}
