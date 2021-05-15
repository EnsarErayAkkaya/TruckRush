using Project.GameSystems;
using UnityEngine;

namespace Project.Player
{
    public class TruckWheelCollision : MonoBehaviour
    {
        [SerializeField] private TruckMovement truckMovement;
        [SerializeField] private LayerMask roadLayer;

        [Header("Wheel Transforms")]
        [SerializeField] private Transform frontRightWheel;
        [SerializeField] private Transform frontLeftWheel;
        [SerializeField] private Transform backRightWheel;
        [SerializeField] private Transform backLeftWheel;
        [Header("Wheel Collision particles")]
        [SerializeField] private ParticleSystem frontRightWheelParticle;
        [SerializeField] private ParticleSystem frontLeftWheelParticle;
        [SerializeField] private ParticleSystem backRightWheelParticle;
        [SerializeField] private ParticleSystem backLeftWheelParticle;

        // they are 1 wheel safe, 0 if it is not
        private byte front_right = 1;
        private byte front_left = 1;
        private byte back_right = 1;
        private byte back_left = 1;

        [SerializeField] private float maxTwoWheelOutTime;

        private float wheelOutStartTime;
        private bool twoWheelOut;
        private bool checkWheelCollision = true;

        private void Update()
        {
            if (truckMovement.CanMove && checkWheelCollision)
            {
                int sum = front_right + front_left + back_right + back_left;

                if (sum == 2 && !twoWheelOut)
                {
                    twoWheelOut = true;
                    wheelOutStartTime = Time.time;
                }
                else if (sum == 1 ||sum == 0)
                {
                    twoWheelOut = false;
                    truckMovement.Stop();
                    GameManager.instance.PlayerLost();
                }
                else
                {
                    twoWheelOut = false;
                }

                if (wheelOutStartTime + maxTwoWheelOutTime < Time.time && twoWheelOut)
                {
                    truckMovement.Stop();
                    //Debug.Log("2 Wheel out of road for too long");
                    GameManager.instance.PlayerLost();
                }
            }
        }

        private void FixedUpdate()
        {
            if (truckMovement.CanMove && checkWheelCollision)
            {
                RaycastHit hit;

                Ray ray = new Ray(frontRightWheel.position, Vector3.down);
                if (Physics.Raycast(ray, out hit, 10, roadLayer))
                {
                    front_right = 1;
                    if (frontRightWheelParticle.isPlaying)
                        frontRightWheelParticle.Stop();
                }
                else
                {
                    front_right = 0;
                    if (frontRightWheelParticle.isStopped)
                        frontRightWheelParticle.Play();
                }

                ray = new Ray(frontLeftWheel.position, Vector3.down);
                if (Physics.Raycast(ray, out hit, 10, roadLayer))
                {
                    front_left = 1;
                    if (frontLeftWheelParticle.isPlaying)
                        frontLeftWheelParticle.Stop();
                }
                else
                {
                    front_left = 0;
                    if (frontLeftWheelParticle.isStopped)
                        frontLeftWheelParticle.Play();
                }

                ray = new Ray(backRightWheel.position, Vector3.down);
                if (Physics.Raycast(ray, out hit, 10, roadLayer))
                {
                    back_right = 1;
                    if (backRightWheelParticle.isPlaying)
                        backRightWheelParticle.Stop();
                }
                else
                {
                    back_right = 0;
                    if (backRightWheelParticle.isStopped)
                        backRightWheelParticle.Play();
                }

                ray = new Ray(backLeftWheel.position, Vector3.down);
                if (Physics.Raycast(ray, out hit, 10, roadLayer))
                {
                    back_left = 1;
                    if (backLeftWheelParticle.isPlaying)
                        backLeftWheelParticle.Stop();
                }
                else
                {
                    back_left = 0;
                    if (backLeftWheelParticle.isStopped)
                        backLeftWheelParticle.Play();
                }
            }
        }
        public void DontCheckCollision() => checkWheelCollision = false;
        public void CheckCollision() => checkWheelCollision = true;
    }
}
