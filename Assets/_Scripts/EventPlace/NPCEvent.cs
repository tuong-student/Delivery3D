using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ImpossibleOdds.DependencyInjection;

namespace Game
{
    [Injectable]
    public class NPCEvent : EventPlace
    {
        [SerializeField] private Package _currentPackage;
        [SerializeField] private Transform  _eventPoint;
        [Inject] private CustomCamera _camera;

        private string[] _eventString = {
           "Hey, someone call you!",
           "Someone call you!",
           "It has someone there!",
           "Is his call you?"
        };
        protected override string[] EventStrings => _eventString;

        private void Start() 
        {
            _currentPackage = PackageMaster.GetRandomPackage();
        } 

        protected override void OnAccept(bool value)
        {
            if(value == false) return;
            _currentPlayer.Stop();
        }

        private void AskPlayer()
        {
            Vector3 cameraPosition = _currentPlayer.GetCameraBesidePosition();
            _camera.SetCameraTarget(this.transform);
            _camera.TranslateCamera(cameraPosition, AskPlayer);
            UIEvent.onEventPlaceSetRequest.Invoke($"Can you give this {_currentPackage._name} to {_currentPackage._destination} it only {_currentPackage._weight}kg");
            UIEvent.onEventPlaceButtonPress.Unregister(OnAccept);
            UIEvent.onEventPlaceButtonPress.Register(PlayerAcceptPackage);
        }

        private void PlayerAcceptPackage(bool value)
        {
            // If player press accept, give player the package
            if(value == false) return;
            GivePackageToPlayer();
            _camera.ReturnOldPosition();
        }

        private void GivePackageToPlayer()
        {
            _currentPlayer.AddPackage(_currentPackage);
            _currentPlayer.SetCanMove(true);
            UIEvent.onEventPlaceButtonOnOffRequest.Invoke(false);
        }
    }

}
