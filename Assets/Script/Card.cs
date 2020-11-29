using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Card : MonoBehaviour
{
    public string cardType; // 카드 무늬와 글자 구별
    private bool isUp = false; // 카드 앞면 상태면 true
    private Sprite backSprite; // 카드 뒷면 이미지
    [SerializeField] private Sprite frontSprite; // 카드 앞면 이미지

    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        backSprite = spriteRenderer.sprite;
    }

    private void OnMouseDown() // 카드 클릭 시
    {
        if (!isUp && gameManager.canFlip)
        {
            gameManager.CompareCards(gameObject);
            Flip();
        }
    }

    public void Flip() // 카드 뒤집기
    {
        if (!isUp)
        {
            SoundManager.Instance.PlaySound("Select");

            StartCoroutine(ToFront(0.15f));
            isUp = true;
        }
        else
        {
            StartCoroutine(ToBack(0.15f));
            isUp = false;
        }
    }

    IEnumerator ToBack(float mTime)
    {
        spriteRenderer.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
            yield return 0;
        spriteRenderer.sprite = backSprite;
        spriteRenderer.transform.DORotate(new Vector3(0, 0, 0), mTime);

    }

    IEnumerator ToFront(float mTime)
    {
        spriteRenderer.transform.DORotate(new Vector3(0, 90, 0), mTime);
        for (float i = mTime; i >= 0; i -= Time.deltaTime)
            yield return 0;
        spriteRenderer.sprite = frontSprite;
        spriteRenderer.transform.DORotate(new Vector3(0, 0, 0), mTime);
    }
}
