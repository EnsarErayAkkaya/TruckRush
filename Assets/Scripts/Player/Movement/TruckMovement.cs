using Project.GameSystems;
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
        private bool canMove;
        public bool CanMove => canMove;
        private void Start()
        {
            GameManager.instance.preGameStart += Move;
        }
        private void Update()
        {
            SetMoveVector();
        }
        private void FixedUpdate()
        {
            if (canMove)
            {
                rb.MovePosition(rb.position + moveVector * Time.fixedDeltaTime);
                Rotate();
            }
        }
        private void SetMoveVector()
        {
            moveVector = transform.forward * mainSpeed;
            moveVector.y = 0;
            rotateVector.y = swerveInput.swerveVector.x;
            rotateVector.x = 0;
            rotateVector.z = 0;
        }
        private void Rotate()
        {
            Quaternion deltaRotation = Quaternion.Euler(rotateVector * rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        public void Move() => canMove = true;
        public void Stop() => canMove = false;

    }
}