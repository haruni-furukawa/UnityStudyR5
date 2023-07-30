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

    // ステージクリア
    public void OnComplateStage()
    {
        if (_isComplateStage) { return; }
        _isComplateStage = true;

        // TODO クリア演出
        if (StageManager.instance.IsLastStage()) 
        {
            // TODO クリア画面へ遷移
            Debug.Log("ゲームクリア");
            return;
        }
        else
        {
            // 次のステージへ
            StageManager.instance.SetNextStage();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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
        SpawnPlayer();
        _isInitialized = true;
    }

    // プレイヤーを生成
    private void CreatePlayer()
    {
        GameObject player = Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
        _playerController = player.GetComponent<PlayerController>();
    }

    // プレイヤーの出現
    private void SpawnPlayer()
    {
        SpawnController spawnController = StageManager.instance.GetCurrentSpawnController();
        _playerController.transform.position = spawnController.transform.position;
        GetPlayerController().SetFreezePosition(false);
        if (spawnController.IsCustomMoveSetting())
        {
            GetPlayerController().SetMoveSpeed(spawnController.GetMoveSpeed());
            GetPlayerController().SetJumpForce(spawnController.GetJumpForce());
        }
        _cameraController.EnableSpawnCameraBehaviour();
    }
}
