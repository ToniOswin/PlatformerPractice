using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 2;
    // Start is called before the first frame update
    private void OnEnable()
    {
        StartCoroutine(destroy());
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speed);
    }
   
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
