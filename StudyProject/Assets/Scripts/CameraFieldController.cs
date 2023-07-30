using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFieldController : MonoBehaviour
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    [SerializeField] private Transform _topLeft;
    [SerializeField] private Transform _bottomRight;
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    // ---------- Public関数 ----------
    public Transform GetTopLeft() { return _topLeft; }
    public Transform GetBottomRight() { return _bottomRight; }
    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
}
