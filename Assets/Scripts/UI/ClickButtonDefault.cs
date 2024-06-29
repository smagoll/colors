using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;

public class ClickButtonDefault : MonoBehaviour
{
    private UnityEvent endClick = new();
    
    private void Awake()
    {
        var button = GetComponent<Button>();
        endClick = button.onClick;
        button.onClick = new Button.ButtonClickedEvent();
        button.onClick.AddListener(Pressed);
    }

    private void Pressed()
    {
        AudioController.instance.PlaySFX(AudioController.instance.button);
        
        DOTween.Sequence()
            .Append(transform.DOScale(.8f, .3f))
            .Append(transform.DOScale(1f, .1f))
            .AppendCallback(() => endClick.Invoke())
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }
}