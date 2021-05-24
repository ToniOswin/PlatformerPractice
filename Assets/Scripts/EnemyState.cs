using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    [SerializeField]
    enemieShoot shootS;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance <= 5)
        {
            shootS.enabled = true;
        }
        else
        {
            shootS.enabled = false;
        }
    }
}
