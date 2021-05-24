using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int lifes;
    const int maxLifes = 10;
    int points;

    [SerializeField]
    GameManager gameMan;
    [SerializeField]
    int level;
    [SerializeField]
    string nextLevel;

    [SerializeField]
    Transform checkGroundPos;
    [SerializeField]
    float checkGroundRadius;
    public LayerMask StepInEnemie;
    bool isOnEnemie;


    [SerializeField]
    SpriteRenderer playerRender;
    [SerializeField]
    TextMeshProUGUI pointsText;
    [SerializeField]
    AudioClip enemieDeath;
    [SerializeField]
    AudioClip hitSound;
    [SerializeField]
    AudioClip healSound;
    [SerializeField]
    AudioClip pointSound;
    [SerializeField]
    AudioSource _audioSource;


    void Start()
    {
        lifes = maxLifes;
        Coin.pointCount = PlayerPrefs.GetInt("Points", 0);
        pointsText.text = "X " + Coin.pointCount;
        gameMan.setMaxHealth(maxLifes);
    }

    void Update()
    {
        isOnEnemie = Physics2D.OverlapCircle(checkGroundPos.position, checkGroundRadius, StepInEnemie);
    }

    public void takeDamage(int damage)
    {
        _audioSource.clip = hitSound;
        _audioSource.Play();
        StartCoroutine(ChangeColor(Color.red));
        lifes -= damage;
        gameMan.SetHealth(lifes);
        if(lifes <= 0)
        {
            gameMan.Die();
        }
    }

    public void Heal()
    {
        _audioSource.clip = healSound;
        _audioSource.Play();
        StartCoroutine(ChangeColor(Color.green));
        lifes = maxLifes;
        gameMan.SetHealth(lifes);
    }
    public void GetPoints()
    {
        _audioSource.clip = pointSound;
        _audioSource.Play();
        Coin coin = new Coin();
        pointsText.text = "X " + Coin.pointCount;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("enemy") && !isOnEnemie)
        {
            takeDamage(2);
        }
        else if(other.gameObject.CompareTag("enemy") && isOnEnemie)
        {
            other.gameObject.GetComponent<Enemy>().Die(_audioSource);
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            takeDamage(1);
            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.CompareTag("Heal"))
        {
            Heal();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Goal") && isOnEnemie)
        {
            
           UnlockLevel(level);
            PlayerPrefs.SetInt("Points", Coin.pointCount);
            if (level != 3)
            {
                gameMan.NextLevel(nextLevel);
            }
            else if (level == 3)
            {
                gameMan.EndGame();
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Point"))
        {
            GetPoints();
            Destroy(other.gameObject);
        }
        else if(other.gameObject.CompareTag("StickyCol"))
        {
            other.gameObject.transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("StickyCol"))
        {
            other.gameObject.transform.parent.gameObject.GetComponent<CircleCollider2D>().enabled = true; ;
        }
    }

    IEnumerator ChangeColor(Color _color)
    {
        playerRender.color = _color;
        yield return new WaitForSeconds(0.5f);
        playerRender.color = Color.white;
    }

    void UnlockLevel(int level)
    {
        if(PlayerPrefs.GetInt("LevelsPassed")  == level-1 )
        {
            PlayerPrefs.SetInt("LevelsPassed", level);
        }
    }
}
