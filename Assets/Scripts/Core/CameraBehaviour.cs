using com.ootii.Cameras;
using UnityEngine;

namespace RPG.Core
{
    public class CameraBehaviour : MonoBehaviour
    {
        [Header("Modifiers")]
        [SerializeField] private Transform follow;

        void Update()
        {
            MouseZoom();
        }

        //Far or near to the player
        public void MouseZoom()
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                GetComponent<YawPitchMotor>().mDistance--;

                if (GetComponent<YawPitchMotor>().mDistance <= 5)
                {
                    GetComponent<YawPitchMotor>().mDistance = 5;
                }
            }

            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                GetComponent<YawPitchMotor>().mDistance++;

                if (GetComponent<YawPitchMotor>().mDistance >= 20)
                {
                    GetComponent<YawPitchMotor>().mDistance = 20;
                }
            }
        }
    }
}