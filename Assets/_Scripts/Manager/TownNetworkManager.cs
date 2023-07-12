using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using NOOD;

namespace Game
{
    /// <summary>
    /// This class will be use for define which town is connected, this will create a map
    /// </summary>
    public class TownNetworkManager
    {
        private static List<Town> _towns = new List<Town>();
        private static List<string> _townNames;

        public static void CreateTownNetwork(int townNumber)
        {
            TextAsset _townTextAsset = Resources.Load<TextAsset>("townNames");
            _townNames = NOOD.NoodyCustomCode.GetRows(_townTextAsset).ToList();

            _towns.Add(new Town(GetRandomTownName()));
            for(int i = 1; i < townNumber; i++)
            {
                // This new town will connect with one old town
                Town emptyOldTown = GetEmptySlotTown();
                if(emptyOldTown != null)
                {
                    Town newTown = new Town(GetRandomTownName());
                    newTown.ConnectToSlot(emptyOldTown);
                    emptyOldTown.ConnectToSlot(newTown);
                    _towns.Add(newTown);
                }
                else 
                {
                    Debug.LogWarning("Something wrong no empty old town");
                }
            }

            foreach(Town town in _towns)
            {
                Debug.Log($"Town: {town._name}, [1] {town.GetConnectedTownName(0)}, [2] {town.GetConnectedTownName(1)}, [3] {town.GetConnectedTownName(2)}");
            }
        }
        
        public static Town GetEmptySlotTown()
        {
            return _towns.Where(x => x.IsEmptySlot == true).GetRandom();
        }
        public static List<Town> GetTownNetwork()
        {
            return _towns;
        }
        public static string GetRandomTownName()
        {
            string townName = _townNames[UnityEngine.Random.Range(0, _townNames.Count)];
            _townNames.Remove(townName);
            return townName;
        }
        public static Town GetTownByName(string name)
        {
            return _towns.First(x => x._name == name);
        }
        
        public void GetNextTown(string currentTown, out string town1, out string town2)
        {
            town1 = "";
            town2 = "";

        }
        public void GetNextTown(string currentTown, out string town1, out string town2, out string town3)
        {
            town1 = "";
            town2 = "";
            town3 = "";
        }
    }

}
