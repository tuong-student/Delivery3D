using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Game
{
    public class UIEventPlace : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _eventPlaceText;
        [SerializeField] private Button _denyBtn, _acceptBtn;
        [SerializeField] private GameObject _eventPlacePanel;

        [SerializeField] private Transform _inEventTextTransform, _outEventTextTransform, _inDenyBtnTransform, _outDenyBtnTransform, _inAcceptBtnTransform, _outAcceptBtnTransform;
        private bool isShowed = false;

        void Start()
        {
            UIEvent.onEventPlaceSetRequest.Register(SetEventText);
            UIEvent.onEventPlaceButtonOnOffRequest.Register(SetActive);
            _acceptBtn.onClick.AddListener(() =>
            {
                UIEvent.onEventPlaceButtonPress.Invoke(true);
                Hide();
            });
            _denyBtn.onClick.AddListener(() =>
            {
                UIEvent.onEventPlaceButtonPress.Invoke(false);
                Hide();
            });
        }
        void OnDisable()
        {
            UIEvent.PurgeDelegatesOf(this);
        }

        private void SetEventText(string text)
        {
            if(isShowed == false)
                Show();
            _eventPlaceText.text = text;
        }

        private void SetActive(bool isActive)
        {
            if(isActive)
                Show();
            else
                Hide();
        }
        private void Show()
        {
            DOTween.Kill(_eventPlacePanel);
            DOTween.Kill(_denyBtn);
            DOTween.Kill(_acceptBtn);

            if(isShowed == true) return;
            isShowed = true;
            _eventPlacePanel.transform.DOMove(_inEventTextTransform.position, 0.5f).SetEase(Ease.OutBounce);
            _denyBtn.transform.DOMove(_inDenyBtnTransform.position, 0.5f).SetEase(Ease.OutBounce);
            _acceptBtn.transform.DOMove(_inAcceptBtnTransform.position, 0.5f).SetEase(Ease.OutBounce);
        }
        private void Hide()
        {
            _eventPlacePanel.transform.DOMove(_outEventTextTransform.position, 0.5f).SetEase(Ease.OutBounce);
            _denyBtn.transform.DOMove(_outDenyBtnTransform.position, 0.5f).SetEase(Ease.OutBounce);
            _acceptBtn.transform.DOMove(_outAcceptBtnTransform.position, 0.5f).SetEase(Ease.OutBounce);
            isShowed = false;
        }
    }
}
