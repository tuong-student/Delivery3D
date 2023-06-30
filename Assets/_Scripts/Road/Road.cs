using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game
{
    public class Road : MonoBehaviour
    {
        [SerializeField] private List<Transform> roadPoints = new List<Transform>();
        private int currentPointIndex = 0;
        [SerializeField] private bool _showDebugRoad;

        void OnDrawGizmos()
        {
            ShowDebugRoad();
        }

        [Button]
        [PropertyOrder(-1)]
        [GUIColor("@Color." + nameof(Color.cyan))]
        private void CreateNewPoint()
        {
            GameObject point = new GameObject("Point" + roadPoints.Count);
            point.transform.SetParent(this.transform);
            roadPoints.Add(point.transform);
        }

        private void ShowDebugRoad()
        {
            if(_showDebugRoad)
            {
                for(int i = 0; i < roadPoints.Count - 1; i++)
                {
                    Gizmos.DrawLine(roadPoints[i].position, roadPoints[i + 1].position);
                }
            }
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
                currentPointIndex++;
                nextPointPosition = roadPoints[currentPointIndex].position;
                return true;
            }
        }

        public bool IsLastPoint()
        {
            if(currentPointIndex == roadPoints.Count - 1) return true;
            else return false;
        }
    }
}
