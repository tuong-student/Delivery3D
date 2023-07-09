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
            UIEvent.PurgeDelegatesOf(this);
        }

        void OnDisable()
        {
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            UIEvent.onEventPlaceButtonPress.Register(OnAccept);
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
            UIEvent.PurgeDelegatesOf(this);
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

        public virtual void OnPlayerGetToEventPoint()
        {
            
        }
    }
}
