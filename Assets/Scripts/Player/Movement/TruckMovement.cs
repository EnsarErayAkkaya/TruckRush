using Project.GameSystems;
using Project.UI;
using System.Collections;
using UnityEngine;

namespace Project.Player
{
    public class TruckMovement : MonoBehaviour
    {
        [SerializeField] private SwerveInput swerveInput;
        [SerializeField] private Rigidbody rb;
        [SerializeField] private float basicSpeed;
        [SerializeField] private float rotationSpeed;


        private Vector3 moveVector;
        private Vector3 rotateVector;
        private bool canMove;
        private GameUI gameUI;
        private float mainSpeed;
        private float speedMofifiers;

        public bool CanMove => canMove;
        private void Start()
        {
            GameManager.instance.onGameStart += Move;
            gameUI = FindObjectOfType<GameUI>();
            SetMainSpeed();
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
            if (canMove)
            {
                moveVector = transform.forward * mainSpeed;
                moveVector.y = 0;
                rotateVector.y = swerveInput.swerveVector.x;
                rotateVector.x = 0;
                rotateVector.z = 0;
                AudioManager.instance.ChangeTruckMovementSoundPitch(rotateVector.y);
                gameUI.RotateSteeringWheel(rotateVector.y);
            }
        }
        private void Rotate()
        {
            Quaternion deltaRotation = Quaternion.Euler(rotateVector * rotationSpeed * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }
        public void Move() => canMove = true;
        public void Stop() => canMove = false;
        public void IncreaseSpeed(float value) 
        {
            speedMofifiers += value;
            SetMainSpeed();
        }
        public void DecreaseSpeed(float value)
        {
            speedMofifiers -= value;
            SetMainSpeed();
        }
        public void SetSpeed(float value)
        {
            basicSpeed = value;
            SetMainSpeed();
        }
        private void SetMainSpeed() => mainSpeed = basicSpeed + speedMofifiers;

    }
}