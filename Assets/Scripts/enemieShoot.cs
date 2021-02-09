using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemieShoot : MonoBehaviour
{
    [SerializeField]
    GameObject proyectilePrefab;
    [SerializeField]
    Animator shootAnimator;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;
    // Start is called before the first frame update
    void Start()
    {
        pooledObjects = new List<GameObject>();
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = (GameObject)Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
            obj.transform.SetParent(this.transform); 
        }

        StartCoroutine(Shoot());
    }
    // Update is called once per frame
    void Update()
    {
        
    }


    public GameObject GetPooledObject()
    {
        // For as many objects as are in the pooledObjects list
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            // if the pooled objects is NOT active, return that object 
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        // otherwise, return null   
        return null;
    }

    IEnumerator Shoot()
    {
        
        GameObject pooledProjectile = GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); // activate it
            pooledProjectile.transform.position = transform.position; // position it at player
        }
        shootAnimator.SetTrigger("Shoot");
        yield return new WaitForSeconds(2);
        StartCoroutine(Shoot());
    }
}
