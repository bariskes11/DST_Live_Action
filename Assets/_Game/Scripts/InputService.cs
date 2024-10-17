using System;
using UnityEngine;

namespace Game.Entity.Movement
{
    public sealed class InputService : MonoBehaviour
    {
        [SerializeField] private Joystick joystick;
        
        public Vector2 MoveInput => joystick.Direction;
    }
}