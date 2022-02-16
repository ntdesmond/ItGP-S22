using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody _body;
    private int _direction;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _direction = 1 - _direction;
        }
    }

    private void FixedUpdate()
    {
        var newDirection = (_direction == 0 ? Vector3.right : Vector3.forward) * speed;
        newDirection.y = _body.velocity.y;
        _body.velocity = newDirection;
    }

    private void OnDisable()
    {
        var newDirection = Vector3.zero;
        newDirection.y = _body.velocity.y;
        _body.velocity = newDirection;
    }
}
