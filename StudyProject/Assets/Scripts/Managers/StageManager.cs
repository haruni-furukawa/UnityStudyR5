using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : SingletonBehavior<StageManager>
{
    // ---------- 定数宣言 ----------
    // ---------- ゲームオブジェクト参照変数宣言 ----------
    // ---------- プレハブ ----------
    [SerializeField] private List<GameObject> _stagePrefabs = default;
    // ---------- プロパティ ----------
    // ---------- クラス変数宣言 ----------
    // ---------- インスタンス変数宣言 ----------

    private GameObject _stage = default;
    private SpawnController _currentSpawnController = default;
    private CameraFieldController _currentCameraFieldController = default;
    private GateController _currentGateController = default;
    // ---------- コンストラクタ・デストラクタ ----------
    // ---------- Unity組込関数 ----------
    // ---------- Public関数 ----------

    // ステージ数取得
    public int GetStageMaxCount()
    {
        return _stagePrefabs.Count;
    }

    // ステージIdを取得
    public int GetStageId()
    {
        return GameData.stageId;
    }

    // ステージIdを設定
    public void SetStageId(int stageId)
    {
        GameData.stageId = Mathf.Clamp(stageId, 0, GetStageMaxCount() - 1);
    }

    // ステージを次に進める
    public void SetNextStage()
    {
        SetStageId(GetStageId() + 1);
    }

    // ラストステージ判定
    public bool IsLastStage()
    {
        return GetStageId() == GetStageMaxCount() - 1;
    }

    // ステージ生成
    public void CreateStage(int stageId)
    {
        stageId = Mathf.Clamp(stageId, 0, GetStageMaxCount() - 1);

        // ステージの生成
        _stage = Instantiate(_stagePrefabs[stageId], Vector3.zero, Quaternion.identity);

        // 生成したオブジェクト以下にある以下のコンポーネントを保持       
        _currentSpawnController = _stage.GetComponentInChildren<SpawnController>();
        _currentCameraFieldController = _stage.GetComponentInChildren<CameraFieldController>();
        _currentGateController = _stage.GetComponentInChildren<GateController>();
    }

    // 現在のステージを生成
    public void CreateCurrentStage()
    {
        CreateStage(GetStageId());
    }

    // 現在のスポーンコントローラーを取得
    public SpawnController GetCurrentSpawnController()
    {
        return _currentSpawnController;
    }

    // 現在のカメラフィールドコントローラーを取得
    public CameraFieldController GetCurrentCameraFieldController()
    {
        return _currentCameraFieldController;
    }

    // 現在のゲートコントローラーを取得
    public GateController GetCurrentGateController()
    {
        return _currentGateController;
    }

    // ---------- Protected関数 ----------
    // ---------- Private関数 ----------
}
