using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGame
{
    public class TurretInput : MonoBehaviour
    {
        [Header("Input Properties")]
        public Camera camera1;
        // Start is called before the first frame update



        private Vector3 reticlePosition;
        public Vector3 ReticlePosition
        {
            get { return reticlePosition; }
        }

        private Vector3 reticleNormal;
        public Vector3 ReticleNormal
        {
            get { return reticleNormal; }
        }

        private float forwordInput;
        public float ForwordInput
        {
            get { return forwordInput; }
        }

        private float rotationInput;
        public float RotationInput
        {
            get { return rotationInput; }
        }

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (camera1)
            {
                HandleInput();
            }
        }

        

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(reticlePosition, 0.5f);
        }

        protected virtual void HandleInput()
        {
            Ray screenRay = camera1.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(screenRay, out hit))
            {
                reticlePosition  = hit.point;
                reticleNormal = hit.normal;

            }

            forwordInput = Input.GetAxis("Vertical");
            rotationInput = Input.GetAxis("Horizontal");
        }
    }
}
