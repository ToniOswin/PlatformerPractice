using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int lifes;
    public int maxLifes = 10;
    public int points;

    [SerializeField]
    GameManager gameMan;

    // Start is called before the first frame update
    void Start()
    {
        lifes = maxLifes;
        gameMan.setMaxHealth(maxLifes);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        lifes -= damage;
        gameMan.SetHealth(lifes);
    }

    public void Heal(int healing)
    {
        lifes += healing;
        gameMan.SetHealth(lifes);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("enemy"))
        {
            takeDamage(2);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            takeDamage(1);
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("Heal"))
        {
            Heal(3);
            Destroy(other.gameObject);
        }
    }
}
