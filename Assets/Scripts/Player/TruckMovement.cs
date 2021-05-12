using System.Collections;
using UnityEngine;

namespace Project.Player
{
    public class TruckMovement : MonoBehaviour
    {
        [SerializeField] private SwerveInput swerveInput;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float  mainSpeed;
        [SerializeField] private float rotationSpeed;

        private Vector3 moveVector;
        private Vector3 rotateVector;
        private void Update()
        {
            SetMoveVector();
        }
        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime);
            Rotate();
        }
        private void SetMoveVector()
        {
            moveVector = transform.forward * mainSpeed;
            rotateVector.y = swerveInput.swerveVector.x;
        }
        private void Rotate()
        {
            Quaternion deltaRotation = Quaternion.Euler(rotateVector * rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }
}