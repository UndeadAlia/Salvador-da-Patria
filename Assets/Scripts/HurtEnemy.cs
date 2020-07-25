using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public int damage;
    public int baseDamage;

    //public Transform hitPoint;

    public GameObject damageBurst;
    public GameObject damageNumber;

    public Transform hitpoint;
    



    // Start is called before the first frame update
    void Start()
    {
        //hitpoint = GameObject.FindGameObjectWithTag("HitPoint").transform;
        

    }

    // Update is called once per frame
    void Update()
    {
        damage = baseDamage + PlayerStats.instance.strength;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerController.instance.attacking)
        {


            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyManager>().HurtEnemy(damage);
                //StaticEnemyManager.instance.HurtEnemy(damage);
                Instantiate(damageBurst, hitpoint.position, hitpoint.rotation);

                var clone = (GameObject) Instantiate(damageNumber, hitpoint.position, Quaternion.Euler (Vector3.zero));
                clone.GetComponent<FloatingNumbers>().damageNumber = damage;
                clone.transform.position = new Vector2(hitpoint.position.x, hitpoint.position.y);

            }
            
        }
    }
}
