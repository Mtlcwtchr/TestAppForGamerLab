using System;
using UnityEngine;

namespace Task3.Scripts
{
    public interface ITarget
    {
        event Action<Vector3> OnPositionChanged;
        Vector3 Position { get; }
    }
}