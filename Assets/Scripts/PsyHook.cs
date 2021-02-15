using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PsyHook : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer lineRend;
    public LayerMask stickyMask;
    [SerializeField]
    float moveSpeed = 2;
    [SerializeField]
    float psyLenght = 5;
    [SerializeField]
    int maxPoints;
    Rigidbody2D rb;
    List<Vector2> points = new List<Vector2> ();

    [SerializeField]
    AudioSource _audioSource;

    public bool isAtached;
    GameObject ActualHook;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRend.positionCount = 0;
        isAtached = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {           
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePos - (Vector2)transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, psyLenght, stickyMask);
            if(hit.collider != null)
            {
                _audioSource.Play();
                Vector2 hitPoint = hit.point;
                points.Add(hitPoint);
                if(points.Count > maxPoints)
                {
                    points.RemoveAt(0);
                }
            }
        }

        if (points.Count > 0)
        {
            isAtached = true;
            Vector2 moveTo = Centroid(points.ToArray());

            rb.MovePosition(Vector2.MoveTowards(transform.position, moveTo, Time.deltaTime * moveSpeed));

            lineRend.positionCount = 0;
            lineRend.positionCount = points.Count * 2;
            for (int n = 0, j = 0; n < points.Count * 2; n += 2, j++)
            {
                lineRend.SetPosition(n, transform.position);
                lineRend.SetPosition(n + 1, points[j]);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            _audioSource.Stop();
            Detatch();
        }
    }

    void Detatch()
    {
        isAtached = false;
        lineRend.positionCount = 0;
        points.Clear();
    }
    Vector2 Centroid(Vector2[] points)
    {
        Vector2 center = Vector2.zero;
        foreach(Vector2 point in points)
        {
            center += point;
        }
        center /= points.Length;
        return center;
    }
}
