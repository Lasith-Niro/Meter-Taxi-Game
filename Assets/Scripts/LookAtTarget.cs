using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGame
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(TurretInput))]
    public class LookAtTarget : MonoBehaviour
    {
        [Header("Reticle Properties")]
        public Transform reticleTransform;

        [Header("Turret Properties")]
        public Transform turretTransform;
        public float turretLagSpeed = 1.5f;
        

        private Rigidbody rb;
        private TurretInput input;
        private Vector3 finalTurretLookDirection;
        void Start()
        {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<TurretInput>(); 
        }

        void Update()
        {

        }
        private void FixedUpdate()
        {
            HandleTurret();
            Handlereticle();
        }

        protected virtual void HandleTurret()
        {
            if (turretTransform)
            {

                //Vector3 turretVectorDirection = input.ReticlePosition - turretTransform.position;
                //turretVectorDirection.y = 0.0f;
                //finalTurretLookDirection = Vector3.Lerp(finalTurretLookDirection, turretVectorDirection, Time.deltaTime * turretLagSpeed);
                // turretTransform.rotation = Quaternion.LookRotation(finalTurretLookDirection);
                
                Vector3 lookVector = input.ReticlePosition - turretTransform.position;
                lookVector.y = 0.0f;
                Quaternion lookQuat = Quaternion.LookRotation(lookVector);
                transform.rotation = Quaternion.Slerp(Quaternion.identity, lookQuat, GameController.interpolator);
                
            }
        }
        protected virtual void Handlereticle()
        {
            if (reticleTransform)
            {
                reticleTransform.position = input.ReticlePosition;
            }
        }
    }

}