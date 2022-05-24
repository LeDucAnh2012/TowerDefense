using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense.GameController
{
    public class GameHandler : MonoBehaviour
    {

        [SerializeField] private Camera cameraFollow;

        private Vector3 origin;

        private Vector3 diffirence;

        private Vector3 cameraFollowPosition;

        private Vector3 resetCam;

        [SerializeField] private float zoomAmount = 0.5f;

        [SerializeField] private float moveAmount = 10;

        private bool drag = false;

        void Start()
        {
            cameraFollow = GetComponent<Camera>();
            cameraFollowPosition = cameraFollow.transform.position;
            resetCam = Camera.main.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            MoveCamByKeyboard();
        }
        private void LateUpdate()
        {
            MoveCamByTouchScreen();
        }
        [SerializeField] float slowMoveCam = 0.2f;
        private void MoveCamByTouchScreen()
        {
            if (Input.GetMouseButton(0))
            {
                diffirence = ((Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position);
                if (!drag)
                {
                    drag = true;
                    origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                }
            }
            else
            {
                drag = false;
            }
            if (drag)
            {
                Vector3 _posCamChange = (origin - diffirence);
                Camera.main.transform.position = new Vector3(_posCamChange.x, _posCamChange.y, -10);
            }
            if (Input.GetMouseButton(1))
            {
                Camera.main.transform.position = resetCam;
            }
        }
        private void MoveCamByKeyboard()
        {
            if (Input.GetKey(KeyCode.W))
            {
                cameraFollowPosition.y += moveAmount * Time.deltaTime;
                cameraFollow.transform.position = cameraFollowPosition;
            }
            if (Input.GetKey(KeyCode.S))
            {
                cameraFollowPosition.y -= moveAmount * Time.deltaTime;
                cameraFollow.transform.position = cameraFollowPosition;
            }
            if (Input.GetKey(KeyCode.A))
            {
                cameraFollowPosition.x -= moveAmount * Time.deltaTime;
                cameraFollow.transform.position = cameraFollowPosition;
            }
            if (Input.GetKey(KeyCode.D))
            {
                cameraFollowPosition.x += moveAmount * Time.deltaTime;
                cameraFollow.transform.position = cameraFollowPosition;
            }
            //zoom
            if (Input.GetKey(KeyCode.KeypadPlus))
            {
                cameraFollow.orthographicSize += zoomAmount * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.KeypadMinus))
            {
                cameraFollow.orthographicSize -= zoomAmount * Time.deltaTime;
            }
        }
    }
}
