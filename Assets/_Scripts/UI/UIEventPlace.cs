using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class UIEventPlace : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _eventPlaceText;
        [SerializeField] private Button _denyBtn, _acceptBtn;

        [SerializeField] private Transform _inEventTextTransform, _outEventTextTransform, _inDenyBtnTransform, _outDenyBtnTransform, _inAcceptBtnTransform, _outAcceptBtnTransform;

        void Start()
        {
            _acceptBtn.onClick.AddListener(() => UIEvent.onEventPlaceButtonPress.Invoke(true));
            _denyBtn.onClick.AddListener(() => UIEvent.onEventPlaceButtonPress.Invoke(false));
        }

        private void SetEventText(string text)
        {
            _eventPlaceText.text = text;
        }

        private void Show()
        {

        }

        private void Hide()
        {

        }
    }
}
