using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private int _jumpForce;
    private bool isMoving = false;
    private bool isJumping = false;
    private bool isFalling = false;
    void Update()
    {
        if (GameManager.instance.IsDyingPlayer()) { return; }
        float horizontal = Input.GetAxis("Horizontal");
        isMoving = horizontal != 0;
        isFalling = rb.velocity.y < -0.5f;

        if (isMoving)
        {
            Vector3 scale = gameObject.transform.localScale;
            if (horizontal < 0 && scale.x > 0 || horizontal > 0 && scale.x < 0)
            {
                scale.x *= -1;
            }
            gameObject.transform.localScale = scale;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isFalling)
        {
            Jump();
        }
        rb.velocity = new Vector2(horizontal * _moveSpeed, rb.velocity.y);
        animator.SetBool("IsMoving", isMoving);
        animator.SetBool("IsJumping", isJumping);
        animator.SetBool("IsFalling", isFalling);

        // 画面下部死亡判定
        CameraFieldController cameraFieldController = StageManager.instance.GetCurrentCameraFieldController();
        Transform bottomRight = cameraFieldController.GetBottomRight();
        if (transform.position.y <= bottomRight.position.y - 6)
        {
            GameManager.instance.DiePlayer();
        }
    }

    // 移動速度を設定
    public void SetMoveSpeed(int moveSpeed)
    {
        _moveSpeed = moveSpeed;
    }

    // ジャンプ力を設定
    public void SetJumpForce(int jumpForce)
    {
        _jumpForce = jumpForce;
    }

    // 位置固定
    public void SetFreezePosition(bool b)
    {
        rb.isKinematic = b;
        if (b)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            // ジャンプアニメーションに死亡した際に、正しくIdleモーションと遷移させるため
            animator.SetBool("IsFalling", true);
        }
    }

    // 明滅エフェクト
    public void SetFlicker(float time, float interval = 0.125f)
    {
        // デフォルトの色（通常時）を保存
        Color defaultColor = playerSpriteRenderer.color;

        // ダメージ色（少し白くする）と透明色を指定
        Color damageColor = Color.Lerp(defaultColor, Color.white, 0.5f);
        Color transparentColor = new Color(defaultColor.r, defaultColor.g, defaultColor.b, 0f);

        // シーケンスを作成
        Sequence sequence = DOTween.Sequence();

        // ループの回数を計算（繰り返す回数＝全体の時間 / (発光時間 + 透明時間)）
        int loopCount = Mathf.FloorToInt(time / (interval * 2));

        // 発光と透明を繰り返すアニメーションを追加
        sequence.Append(playerSpriteRenderer.DOColor(damageColor, interval))
                .Append(playerSpriteRenderer.DOColor(transparentColor, interval))
                .SetLoops(loopCount)
                .OnComplete(() =>
                {
                    // アニメーションが終わったら色をデフォルトに戻す
                    playerSpriteRenderer.color = defaultColor;
                });
    }

    void Jump()
    {
        isJumping = true;
        rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stage"))
        {
            isJumping = false;
        }
    }
}