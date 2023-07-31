using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AtoBObject : MonoBehaviour
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    [SerializeField] private Transform _aObject = default;
    [SerializeField] private Transform _bObject = default;
    [SerializeField] private float _duration = 1.0f;
    [SerializeField] private float _waitTime = 1.0f;
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    private void Start()
    {
        Transform target = transform.parent;
        Sequence seq = DOTween.Sequence();

        seq.Append(target.DOMove(_bObject.position, _duration).SetEase(Ease.Linear));

        seq.AppendInterval(_waitTime);

        seq.Append(target.DOMove(_aObject.position, _duration).SetEase(Ease.Linear));

        seq.AppendInterval(_waitTime);

        seq.SetLoops(-1);

        seq.Play();
    }
    // ---------- Public関数 ----------
    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
}
