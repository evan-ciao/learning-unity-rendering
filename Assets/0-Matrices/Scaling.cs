using UnityEngine;

namespace Matrices
{
    public class Scaling : Transformation
    {
        [SerializeField] private Vector3 _scale = Vector3.one;

        public override Vector3 Apply(Vector3 point)
        {
            return new Vector3(point.x * _scale.x, point.y * _scale.y, point.z * _scale.z);
        }
    }
}