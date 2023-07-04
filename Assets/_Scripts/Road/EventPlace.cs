using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class EventPlace : MonoBehaviour
    {
        protected virtual string[] EventStrings => null;
        protected Player _currentPlayer;

        protected virtual void OnEnable()
        {
            UIEvent.onEventPlaceButtonPress.Register(OnAccept);
        }

        void OnDisable()
        {
            UIEvent.PurgeDelegatesOf(this);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            Player playerTemp = other.GetComponent<Player>();
            if (playerTemp != null)
            {
                // Player is near
                _currentPlayer = playerTemp;
                playerTemp.Stop();
                playerTemp.SetEventPlace(this);
                System.Random r = new System.Random();
                UIEvent.onEventPlaceSetRequest.Invoke(EventStrings.ElementAt(r.Next(EventStrings.Count())));
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            Player playerTemp = other.GetComponent<Player>();
            if(playerTemp != null)
            {
                // Player is going out
                playerTemp.SetEventPlace(null);
            }
        }

        protected virtual void OnAccept(bool value)
        {

        }
    }
}
