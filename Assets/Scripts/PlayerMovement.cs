using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IObserver
{
    [SerializeField] private Camera mainCamera;
    public float moveSpeed = 10f;
    public Subject<bool> Subject;
    public GameObject SpawnPoint; // ეს ამკლასში არუნდა იყოს მარა at this point :D

    public Rigidbody2D rb;
    private bool _receivingInput = true;


    // Start is called before the first frame update
    void Start()
    {
        Subject.AddObserver(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_receivingInput)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        Move();
        LookAtMouse();
    }

    public IEnumerator MoveFastFor(float additionalSpeed, float timer)
    {
        var currentMoveSpeed = moveSpeed;

        moveSpeed = moveSpeed + additionalSpeed;
        yield return new WaitForSeconds(timer);
        moveSpeed = currentMoveSpeed;
    }

    private void LookAtMouse()
    {
        var mousePos = Input.mousePosition;
        mousePos = new Vector3(mousePos.x, mousePos.y, transform.position.z - mainCamera.transform.position.z);
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);
        var direction = mousePos - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        SpawnPoint.transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // transform.position += new Vector3(horizontalInput, verticalInput, 0f).normalized 
        //                       * (moveSpeed * Time.deltaTime);

        rb.velocity = new Vector2(horizontalInput, verticalInput).normalized * moveSpeed;
    }

    public void Notify<T>(T data)
    {
        bool? boolValue = data as bool?;

        if (boolValue.HasValue)
        {
            _receivingInput = boolValue.Value;
        }
    }
}