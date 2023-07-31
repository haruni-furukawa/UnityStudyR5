using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotateObject : MonoBehaviour
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    [SerializeField] private float _rotationTime = 1.0f;
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    private void Start()
    {
        transform
            .DORotate(new Vector3(0.0f, 0.0f, -360.0f), _rotationTime, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
    // ---------- Public関数 ----------
    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
}
