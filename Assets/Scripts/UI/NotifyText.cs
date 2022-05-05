using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class NotifyText : MonoBehaviour
{
    [SerializeField] GameObject txtObj;
    [SerializeField] float displayTime;
    [SerializeField] float yDistance;
    Sequence sequence;
    Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        txtObj.SetActive(false);
        startPosition = transform.position;
        
    }

    public void Display()
    {
        if (sequence != null)
        {
            sequence.Kill();
        }
        sequence = DOTween.Sequence();
        sequence.OnStart(() =>
        {
            txtObj.SetActive(true);
        }).Append(txtObj.GetComponent<RectTransform>().DOLocalMoveY(yDistance, displayTime).SetEase(Ease.OutCubic))
        .OnComplete(() =>
        {
            txtObj.SetActive(false);
            txtObj.GetComponent<RectTransform>().position = startPosition;
        });
    }
}
