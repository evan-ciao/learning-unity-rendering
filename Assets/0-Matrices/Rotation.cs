using UnityEngine;

namespace Matrices
{
    public class Rotation : Transformation
    {
        [SerializeField] private Vector3 _rotation = Vector3.zero;

        public override Vector3 Apply(Vector3 point)
        {
            return Vector3.zero;
        }
    }
}