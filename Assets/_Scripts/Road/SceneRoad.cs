using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Game
{
    public enum RoadDirection
    {
        Left,
        Middle,
        Right,
    }
    public class SceneRoad : MonoBehaviour
    {
        [ShowInInspector] private Dictionary<RoadDirection, Road> _roadDic = new Dictionary<RoadDirection, Road>();

        public Road ChooseRoad(RoadDirection roadDirection)
        {
            return _roadDic[roadDirection];
        }
    }

}
