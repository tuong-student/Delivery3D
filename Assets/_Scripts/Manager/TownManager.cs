using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [System.Serializable]
    public class Town
    {
        public string _name;
        public List<Town> _connectedTowns = new List<Town>();
        private bool _isEmptySlot = true;
        public bool IsEmptySlot => _isEmptySlot;
        public Town(string name){this._name = name;}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"> index range [0, 2]</param>
        /// <returns></returns>
        public string GetConnectedTownName(int index)
        {
            if(this._connectedTowns.Count <= index) return " ";
            return this._connectedTowns[index]._name;
        }
        public List<Town> GetAllConnectedTown()
        {
            return _connectedTowns;
        }
        public void ConnectToSlot(Town town)
        {
            if(_connectedTowns.Count < 3)
            {
                _connectedTowns.Add(town);
            }
            if(_connectedTowns.Count == 3)
                _isEmptySlot = false;
        }
    }

    /// <summary>
    /// This class will control anything in the town like sell, buy, deliver package...
    /// </summary>
    public class TownManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private Town _currentTown;

        void Awake()
        {
            _gameManager = DependenceInjectionSceneScope.ContainerInstance.GetInstance<GameManager>();
        }
        void Start()
        {
            _gameManager.SetCurrentTown(_currentTown);
        }

        public void SetCurrentTown(Town town)
        {
            _currentTown = town;
        }
    }
}