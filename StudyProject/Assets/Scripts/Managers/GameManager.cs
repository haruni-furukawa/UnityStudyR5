using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class GameManager : SingletonBehavior<GameManager>
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    [SerializeField] private CameraController _cameraController = default;
    // ---------- プレハブ ----------
    [SerializeField] private GameObject _playerPrefab = default;
    // ---------- プロパティ ----------
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------
    bool _isInitialized = false;
    bool _isComplateStage = false;
    bool _isDyingPlayer = false;
    private PlayerController _playerController = default;

    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    private void Start()
    {
        Initialize();
    }

    // ---------- Public関数 ----------

    // 初期化判定
    public bool IsInitialized() { return _isInitialized; }

    // ステージクリア判定
    public bool IsComplateStage() { return _isComplateStage; }

    // ステージクリア
    public void OnComplateStage()
    {
        if (_isComplateStage) { return; }
        _isComplateStage = true;

        FXManager.instance.PlayFireworks();
        GetPlayerController().SetFreezePosition(true);

        DOVirtual.DelayedCall(3.0f, () =>
        {
            if (StageManager.instance.IsLastStage())
            {
                // クリア画面へ遷移
                SceneManager.LoadScene("ClearScene");
                return;
            }
            else
            {
                // 次のステージへ
                StageManager.instance.SetNextStage();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        });
    }

    // プレイヤーの死亡判定
    public bool IsDyingPlayer() { return _isDyingPlayer; }

    // プレイヤーの取得
    public PlayerController GetPlayerController()
    {
        return _playerController;
    }

    // プレイヤーの死亡
    public void DiePlayer()
    {
        if (_isDyingPlayer) { return; }
        _isDyingPlayer = true;
        // 死亡アニメーション
        float delay = 1f;
        GetPlayerController().SetFreezePosition(true);
        GetPlayerController().SetFlicker(delay);

        DOVirtual.DelayedCall(delay, () =>
        {
            SpawnPlayer();
            _isDyingPlayer = false;
        });
    }

    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------

    // 初期化
    private void Initialize()
    {
        if (_isInitialized) { return; }
        StageManager.instance.CreateCurrentStage();
        CreatePlayer();
        SpawnPlayer(true);
        _isInitialized = true;
    }

    // プレイヤーを生成
    private void CreatePlayer()
    {
        GameObject player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        _playerController = player.GetComponent<PlayerController>();
    }

    // プレイヤーの出現
    private void SpawnPlayer(bool isInitialize = false)
    {
        SpawnController spawnController = StageManager.instance.GetCurrentSpawnController();
        _playerController.transform.position = spawnController.transform.position;
        GetPlayerController().SetFreezePosition(false);
        if (spawnController.IsCustomMoveSetting())
        {
            GetPlayerController().SetMoveSpeed(spawnController.GetMoveSpeed());
            GetPlayerController().SetJumpForce(spawnController.GetJumpForce());
        }

        if(!isInitialize)
        {
            _cameraController.EnableSpawnCameraBehaviour();
        }
    }
}
