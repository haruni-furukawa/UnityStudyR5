using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    // ---------- プレハブ ----------
    // ---------- プロパティ ----------
    [SerializeField] private bool _isCustomMoveSetting = false;
    [SerializeField] private int _moveSpeed = 0;
    [SerializeField] private int _jumpForce = 0;
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    // ---------- Public関数 ----------
    public bool IsCustomMoveSetting() { return _isCustomMoveSetting; }
    public int GetMoveSpeed() { return  _moveSpeed; }
    public int GetJumpForce() { return _jumpForce; }
    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
}
