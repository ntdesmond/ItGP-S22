using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody _body;
    private Vector3 _direction = Vector3.right;

    private void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _direction = _direction == Vector3.right ? Vector3.forward : Vector3.right;
        }
    }

    private void FixedUpdate()
    {
        UpdateVelocity(_direction);
    }

    private void OnDisable()
    {
        UpdateVelocity(Vector3.zero);
    }

    private void UpdateVelocity(Vector3 direction)
    {
        var newVelocity = direction * speed;
        newVelocity.y = _body.velocity.y;
        _body.velocity = newVelocity;
    }
}
