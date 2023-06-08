﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private Camera mainCamera;
    public float moveSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        LookAtMouse();
    }

    private void LookAtMouse()
    {
        var mousePos = Input.mousePosition;
        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z - mainCamera.transform.position.z);
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);
        var direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    private void Move()
    {
        // თუ სრიალის ეფექტი გვინდა GetAxis
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        transform.position += new Vector3(horizontalInput, verticalInput, 0f).normalized 
                              * (moveSpeed * Time.deltaTime);;
    }
}
