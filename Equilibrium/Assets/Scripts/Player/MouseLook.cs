using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.FirstPerson
{
    [Serializable]
    public class MouseLook
    {
        public float XSensitivity = 2f;
        public float YSensitivity = 2f;
        public float XPS4Sensitivity = 20f; //sensibilità levetta destra ps4
        public float YPS4Sensitivity = 20f;
        public bool clampVerticalRotation = true;
        public float MinimumX = -90F;
        public float MaximumX = 90F;
        public bool smooth;
        public float smoothTime = 5f;
        public bool lockCursor = true;


        private Quaternion m_CharacterTargetRot;
        private Quaternion m_CameraTargetRot;
        private bool m_cursorIsLocked = true;
        private bool joystickConnected = false;

        public void Init(Transform character, Transform camera)
        {
            m_CharacterTargetRot = character.localRotation;
            m_CameraTargetRot = camera.localRotation;
            joystickConnected = isJoystickConnected();
        }

        private bool isJoystickConnected()
        {
            string[] names = Input.GetJoystickNames();
            for (int x = 0; x < names.Length; x++)
            {
                if (names[x].Length == 19) //19 è il joystick della PS4
                    return true;
            }
            return false;
        }


        public void LookRotation(Transform character, Transform camera)
        {
            float xRot = 0;
            float yRot = 0;

            if(joystickConnected)
            {
                yRot = CrossPlatformInputManager.GetAxis("PS4 Right Stick X") * XPS4Sensitivity;
                xRot = CrossPlatformInputManager.GetAxis("PS4 Right Stick Y") * YPS4Sensitivity;
            }
            else
            {
                yRot = CrossPlatformInputManager.GetAxis("Mouse X") * XSensitivity;
                xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * YSensitivity;
            }


            m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
            m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

            if(clampVerticalRotation)
                m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

            if(smooth)
            {
                character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
                    smoothTime * Time.deltaTime);
                camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
                    smoothTime * Time.deltaTime);
            }
            else
            {
                character.localRotation = m_CharacterTargetRot;
                camera.localRotation = m_CameraTargetRot;
            }

            UpdateCursorLock();
        }

        public void SetCursorLock(bool value)
        {
            lockCursor = value;
            if(!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        public void UpdateCursorLock()
        {
            //if the user set "lockCursor" we check & properly lock the cursos
            if (lockCursor)
                InternalLockUpdate();
        }

        private void InternalLockUpdate()
        {
            if(Input.GetKeyUp(KeyCode.Escape))
            {
               m_cursorIsLocked = false;
            }

            else if(Input.GetMouseButtonUp(0) || !PauseMenu.gameIsPaused)
            {
                m_cursorIsLocked = true;
            }

            if (m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!m_cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

            angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

            q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

    }
}
