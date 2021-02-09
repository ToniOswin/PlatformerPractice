using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemie1 : MonoBehaviour
{
    [SerializeField]
    Transform[] limitPositions;
    [SerializeField]
    float speed;
    [SerializeField]
    SpriteRenderer SpRenderer;

    Transform nextPos;
    int nextPosIdx;
    // Start is called before the first frame update
    void Start()
    {
        nextPos = limitPositions[0];
        foreach (Transform child in limitPositions)
        {
            child.SetParent(null, true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemie();

    }

    void moveEnemie()
    {
        if (transform.position == nextPos.position)
        {
            nextPosIdx++;
        
            if (nextPosIdx >= limitPositions.Length)
            {
                nextPosIdx = 0;
            }
            nextPos = limitPositions[nextPosIdx];

            float facingDirection = transform.position.x - nextPos.position.x;
            if(facingDirection < 0) { SpRenderer.flipX = true; }
            else if(facingDirection > 0) { SpRenderer.flipX = false; }


        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPos.position, speed * Time.deltaTime);
        }
    }
}
