using UnityEngine;

public class movement : MonoBehaviour
{
    Quaternion a;
    Vector3 cc;
    float sensitivity = 3f;
    float slowSpeed = 10f;
    float normalSpeed = 25f;
    float sprintSpeed = 50f;
    float currentSpeed;
    Rigidbody rb;

    private void Start()
    {
        a = gameObject.transform.rotation;
        rb = gameObject.GetComponent<Rigidbody>();
        cc = gameObject.transform.position;
    }
    void Update()
    {
        if (Input.GetMouseButton(1)) //if we are holding right click
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Movement();
            Rotation();
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gameObject.transform.position = cc;
            gameObject.transform.rotation = a;
            rb.isKinematic = true;
            rb.isKinematic = false;
            Debug.Log("Reset cam position should fix it rn");
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {

        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Selectable")
        {
            gameObject.transform.position = cc;

        }
    }
    public void Rotation()
    {
        Vector3 mouseInput = new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        transform.Rotate(mouseInput * sensitivity);
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(eulerRotation.x, eulerRotation.y, 0);
    }

    public void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftAlt))
        {
            currentSpeed = slowSpeed;
        }
        else
        {
            currentSpeed = normalSpeed;
        }

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.Q))
        {
            input.y -= 0.75f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            input.y += 0.75f;
        }

        Rigidbody s = GetComponent<Rigidbody>();
        Vector3 newPosition = transform.position + transform.TransformDirection(input) * currentSpeed * Time.deltaTime;
        s.MovePosition(newPosition);
    }
}
