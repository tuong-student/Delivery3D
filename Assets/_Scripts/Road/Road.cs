using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private SceneRoad _sceneRoad;
        [SerializeField] private List<Transform> _roadPoints = new List<Transform>();
        private int _currentPointIndex = -1;
        public int CurrentPointIndex => _currentPointIndex;
        [SerializeField] private bool _showDebugRoad;
        [SerializeField] private Portal _portal;

        void OnDrawGizmos()
        {
            ShowDebugRoad();
        }

        [Button]
        [PropertyOrder(-1)]
        [GUIColor("@Color." + nameof(Color.cyan))]
        private void CreateNewPoint()
        {
            GameObject point = new GameObject("Point" + _roadPoints.Count);
            point.transform.SetParent(this.transform);
            _roadPoints.Add(point.transform);
        }

        private void ShowDebugRoad()
        {
            if(_showDebugRoad)
            {
                for(int i = 0; i < _roadPoints.Count - 1; i++)
                {
                    Gizmos.DrawLine(_roadPoints[i].position, _roadPoints[i + 1].position);
                }
            }
        }

        public void SetNextTown(Town town)
        {
            _portal.SetTown(town);
        }

        public bool TryToGetNextPointPosition(out Vector3 nextPointPosition)
        {
            if(IsLastPoint()) 
            {
                nextPointPosition = Vector3.zero;
                return false;
            }
            else
            {
                _currentPointIndex++;
                nextPointPosition = _roadPoints[_currentPointIndex].position;
                return true;
            }
        }
        public Vector3 GetNextPointPosition()
        {
            _currentPointIndex++;
            return _roadPoints[_currentPointIndex].position;
        }
        public Vector3 GetPoint(int index)
        {
            return _roadPoints[index].position;
        }

        public bool IsLastPoint()
        {
            if(_currentPointIndex == _roadPoints.Count - 1) return true;
            else return false;
        }
    }
}
