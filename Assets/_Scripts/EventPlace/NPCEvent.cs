using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class NPCEvent : EventPlace
    {
        [SerializeField] private Package _currentPackage;

        private void Start() 
        {
            _currentPackage = PackageMaster.GetRandomPackage();
        } 

        private string[] _evenString = {
           "Hey, someone call you!",
           "Someone call you!",
           "It has someone there!",
           "Is his call you?"
        };
        protected override string[] EventStrings => _evenString;

        protected override void OnAccept(bool value)
        {
            if(value == false) return;

        }

        private void AskPlayer()
        {
            UIEvent.onEventPlaceSetRequest.Invoke($"Can you give this {_currentPackage._name} to {_currentPackage._destination} it only {_currentPackage._weight}kg");
            UIEvent.onEventPlaceButtonPress.Register(GivePackageToPlayer);
        }

        private void GivePackageToPlayer(bool value)
        {
            if(value == false) return;
            _currentPlayer.AddPackage(_currentPackage);
            _currentPackage = null;
        }
    }

}
