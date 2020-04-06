using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGame
{
    [RequireComponent(typeof(TurretInput))]

    public class TurretController : MonoBehaviour
    {

        private TurretInput input;
        private Vector3 aimVector;

        public float interpolator = -1.0f;

        public Transform gunXform;
        public Transform domeXform;


        // Start is called before the first frame update
        void Start()
        {
            input = GetComponent<TurretInput>();
        }

        // Update is called once per frame
        void Update()
        {
            
            aimVector = input.ReticlePosition - gunXform.position;

            Quaternion aimQuat = Quaternion.LookRotation(aimVector);
            gunXform.rotation = Quaternion.Slerp(Quaternion.identity, aimQuat, interpolator);

            aimVector.y = 0.0f;
            Quaternion domeQuat = Quaternion.LookRotation(aimVector);


            domeXform.rotation = Quaternion.Slerp(Quaternion.identity, domeQuat, interpolator);
        }
    }
}