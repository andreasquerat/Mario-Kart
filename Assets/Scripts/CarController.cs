using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody _rb;
    private float _speed = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var x = Mathf.Clamp(transform.eulerAngles.x, -45, 45);
        transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.MovePosition(transform.position+transform.forward*_speed*Time.deltaTime);
          //  _rb.linearVelocity = transform.forward*_speed;
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {

        }

  
    }
}
