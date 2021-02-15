using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemieShoot : MonoBehaviour
{
    [SerializeField]
    GameObject proyectilePrefab;
    [SerializeField]
    Animator shootAnimator;
    [SerializeField]
    AudioSource shootSound;

    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

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
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }

    IEnumerator Shoot()
    {
        shootSound.Play();
        GameObject pooledProjectile = GetPooledObject();
        if (pooledProjectile != null)
        {
            pooledProjectile.SetActive(true); 
            pooledProjectile.transform.position = transform.position; 
        }
        shootAnimator.SetTrigger("Shoot");
        yield return new WaitForSeconds(2);
        StartCoroutine(Shoot());
    }
}
