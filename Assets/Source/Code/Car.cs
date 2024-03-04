using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Car : MonoBehaviour
{

    [SerializeField] private Vector3 dir;
    [SerializeField] private bool canMove;
    [SerializeField] private float speed;
    private void OnMouseDown()
    {
        if (GameManager.Instance.canDrag)
        {
            StartMove();
        }
    }
    

    void StartMove()
    {
        if (GameManager.Instance.canDrag)
        {
            canMove = true;
            GameManager.Instance.canDrag = false;
        }
    }

    void StopMove()
    {
        canMove = false;
        GameManager.Instance.canDrag = true;
        
    }

    private void Update()
    {
        if (canMove)
        {
            transform.position += dir * (speed * Time.deltaTime);
        }
    }
    
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.GetComponent<Car>())
        {
            StopMove();
        }
        else
        {
            if (other.transform.CompareTag("wall")||other.transform.CompareTag("saw"))
            {
                StopMove();
                GameManager.Instance.GetCurLevel().RemoveObject(gameObject);
                Destroy(gameObject);
            }
        }
    }
}
