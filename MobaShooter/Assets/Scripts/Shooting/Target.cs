using UnityEngine;

public class Target : MonoBehaviour {

    //Target has 50hp
    public float health = 50f;

    //TakeDamage gets called from the gun when it hits an object
    public void TakeDamage(float damageAmmount) {

        health -= damageAmmount;
        if (health <= 0) {

            Die();

        }

    }

    void Die() {

        //Die just destroys the gameobject the script is called on
        Destroy(gameObject);

    }
}
