using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HorizontalMove: MonoBehaviour
{
    [SerializeField] private bool isRight;
    private void Start()
    {
        if (isRight) {
            RightActiveObstacle();
        }
        else {
            LeftActiveObstacle();
        }
    }
    void RightActiveObstacle() {
        
        transform.DOLocalMoveX(-4f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }
    void LeftActiveObstacle()
    {
        transform.DOLocalMoveX(4f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

}
