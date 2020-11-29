using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float limit = 15.0f; // 제한 시간
    private float timer; // 남은 시간
    public float count = 100.0f; // text가 0.1초에 한번씩만 변경되게 하기 위해 설정한 변수
    public bool stopTimer = false; // 타이머를 멈춰야할 때 true

    [SerializeField] private Text timeText;
    [SerializeField] private GameObject failMenu;
    [SerializeField] private Text failText;
    private GameManager gameManager;

    private void Start()
    {
        timer = limit;
        count = timer - 0.1f;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (!stopTimer)
        {
            if (timer <= count) // text가 0.1초에 한번씩만 변경되게 하기 위함
            {
                count = Mathf.Round((count - 0.1f) * 10) * 0.1f;

                if (count < 0.0f)
                {
                    SoundManager.Instance.PlaySound("Fail");

                    gameManager.canFlip = false;
                    failMenu.SetActive(true);
                    stopTimer = true;
                    
                    failText.text = "최종점수 : " + Score.score;
                    Score.save(Score.score);
                }
                else if (count <= 5.0f)
                {
                    GetComponent<Text>().color = Color.red;
                    timeText.text = "Time : " + Mathf.Round(count * 10) * 0.1f;
                }
                else
                {
                    timeText.text = "Time : " + Mathf.Round(count * 10) * 0.1f;
                }
            }
            timer -= Time.deltaTime;
        }
    }
}
