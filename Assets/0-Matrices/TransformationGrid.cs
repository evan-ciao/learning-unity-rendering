using System.Collections.Generic;
using UnityEngine;

namespace Matrices
{
    public class TransformationGrid : MonoBehaviour
    {
        public Transform Prefab;

        private int _gridResolution = 10;
        private Transform[] _grid;

        private void Awake()
        {
            _grid = new Transform[_gridResolution * _gridResolution * _gridResolution];
            
            for (int i = 0, z = 0; z < _gridResolution; z++)
            {
                for (int y = 0; y < _gridResolution; y++)
                {
                    for (int x = 0; x < _gridResolution; x++, i++)
                    {
                        _grid[i] = CreateGridPoint(x, y, z);
                    }
                }
            }
        }

        private List<Transformation> _transformations = new();
        private void Update()
        {
            // this is very useful to avoid memory overheads,
            // it just fills the list over and over again.
            GetComponents<Transformation>(_transformations);

            for (int i = 0, z = 0; z < _gridResolution; z++)
            {
                for (int y = 0; y < _gridResolution; y++)
                {
                    for (int x = 0; x < _gridResolution; x++, i++)
                    {
                        _grid[i].localPosition = TransformPoint(x, y, z);
                    }
                }
            }
        }

        private Vector3 TransformPoint(int x, int y, int z)
        {
            Vector3 coordinates = GetCoordinatesInGrid(x, y, z);

            foreach (var transformation in _transformations)
            {
                coordinates = transformation.Apply(coordinates);
            }
            
            return coordinates;
        }

        private Transform CreateGridPoint(int x, int y, int z)
        {
            Transform point = Instantiate(Prefab);
            point.localPosition = GetCoordinatesInGrid(x, y, z);

            point.GetComponent<MeshRenderer>().material.color = new Color(
                (float)x / _gridResolution,
                (float)y / _gridResolution,
                (float)z / _gridResolution
            );

            return point;
        }

        private Vector3 GetCoordinatesInGrid(int x, int y, int z)
        {
            return new Vector3(
                x - (_gridResolution - 1) * 0.5f,
                y - (_gridResolution - 1) * 0.5f,
                z - (_gridResolution - 1) * 0.5f
            );
        }
    }
}