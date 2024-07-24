using System;
using UnityEngine;

namespace FunnyBlox
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField] private VariableJoystick joystick;

        private void Update()
        {
            if (joystick.Vertical != 0f || joystick.Horizontal != 0f)
            {
                EventsService.PlayerTurn(joystick.Vertical, joystick.Horizontal);
            }
        }
    }
}