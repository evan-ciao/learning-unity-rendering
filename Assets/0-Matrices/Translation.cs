using UnityEngine;

namespace Matrices
{
    public class Translation : Transformation
    {
        [SerializeField] private Vector3 _offset;

        public override Vector3 Apply(Vector3 point)
        {
            return point + _offset;
        }
    }
}