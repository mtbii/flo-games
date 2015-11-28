using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.FuseModel.Scripts
{
    public static class ExtensionMethods
    {
        public static SideScrollCharacterController.Direction TurnClockwise(this SideScrollCharacterController.Direction dir)
        {
            switch (dir)
            {
                case SideScrollCharacterController.Direction.Down:
                    return SideScrollCharacterController.Direction.Left;

                case SideScrollCharacterController.Direction.Right:
                    return SideScrollCharacterController.Direction.Down;

                case SideScrollCharacterController.Direction.Up:
                    return SideScrollCharacterController.Direction.Right;

                case SideScrollCharacterController.Direction.Left:
                    return SideScrollCharacterController.Direction.Up;

                default:
                    return SideScrollCharacterController.Direction.Down;
            }
        }

        public static SideScrollCharacterController.Direction TurnCounterClockwise(this SideScrollCharacterController.Direction dir)
        {
            switch (dir)
            {
                case SideScrollCharacterController.Direction.Down:
                    return SideScrollCharacterController.Direction.Right;

                case SideScrollCharacterController.Direction.Right:
                    return SideScrollCharacterController.Direction.Up;

                case SideScrollCharacterController.Direction.Up:
                    return SideScrollCharacterController.Direction.Left;

                case SideScrollCharacterController.Direction.Left:
                    return SideScrollCharacterController.Direction.Down;

                default:
                    return SideScrollCharacterController.Direction.Down;
            }
        }

        public static Vector3 ToVector3(this SideScrollCharacterController.Direction dir)
        {
            switch (dir)
            {
                case SideScrollCharacterController.Direction.Down:
                    return Vector3.down;

                case SideScrollCharacterController.Direction.Left:
                    return Vector3.left;

                case SideScrollCharacterController.Direction.Up:
                    return Vector3.up;

                case SideScrollCharacterController.Direction.Right:
                    return Vector3.right;

                default:
                    return Vector3.down;
            }
        }

        public static float ToAngle(this SideScrollCharacterController.Direction dir)
        {
            switch (dir)
            {
                case SideScrollCharacterController.Direction.Down:
                    return 0;

                case SideScrollCharacterController.Direction.Left:
                    return 270;

                case SideScrollCharacterController.Direction.Up:
                    return 180;

                case SideScrollCharacterController.Direction.Right:
                    return 90;

                default:
                    return 0;
            }
        }
    }
}
