using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameObject firstCard = null; // 처음 뒤집는 카드
    private GameObject secondCard = null; // 두번째 뒤집는 카드
    public int cardsLeft; // 남은 카드 갯수
    public bool canFlip = true; // 카드를 뒤집을 수 없는 상태(다른 행동 중이거나 게임이 끝남)가 아니면 true
    [SerializeField] private int scorePerCard; // 정답 맞출 때 점수를 얼마나 줄 것인가
    private float flipDelay = 0.7f; // 뒤집은 카드 두 장을 다시 뒷면으로 뒤집을 때 딜레이. 플레이어가 기억할 시간을 주기 위함.

    [SerializeField] private Text scoreText;
    [SerializeField] private Text winText;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private Timer timer;
    private Scene scene;

    void Start()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Stage1")
        {
            Score.score = 0;
        }
        scoreText.text = "Score : " + Score.score;
        timer = FindObjectOfType<Timer>();
    }

    public void CompareCards(GameObject card) // 뒤집은 두 카드를 저장하고 비교하는 함수
    {
        if (firstCard == null) // 첫번째 카드를 저장
        {
            firstCard = card;
        }
        else // 두번째 카드를 저장하고 두 카드를 비교함.
        {
            secondCard = card;
            canFlip = false; // 비교동안에 다른 카드를 뒤집을 수 없도록 하기 위함.

            if (isSame()) // 두 카드가 같으면
            {
                SoundManager.Instance.PlaySound("Correct");
                Score.score = Score.score + scorePerCard;
                scoreText.text = "Score : " + Score.score;
                DecreaseCardCount();
                StartCoroutine(DeactivateCards());
            }
            else
            {
                SoundManager.Instance.PlaySound("Wrong");
                StartCoroutine(FlipCards());
            }
        }
    }

    IEnumerator DeactivateCards() // 정답을 맞추면 두 카드가 사라지게 함.
    {
        yield return new WaitForSeconds(flipDelay);
        firstCard.SetActive(false);
        secondCard.SetActive(false);

        firstCard = null;
        secondCard = null;
        canFlip = true;
    }

    IEnumerator FlipCards()  // 정답을 틀리면 두 카드를 다시 뒤집음.
    {
        yield return new WaitForSeconds(flipDelay);
        firstCard.GetComponent<Card>().Flip();
        secondCard.GetComponent<Card>().Flip();

        firstCard = null;
        secondCard = null;
        canFlip = true;
    }

    public void DecreaseCardCount()
    {
        cardsLeft -= 2;

        if (cardsLeft <= 0) // 카드를 다 맞춘 경우
        {
            timer.stopTimer = true;

            int leftTime = (int)timer.count;
            Score.score += leftTime; // 남은 시간에 따라 추가 점수

            SoundManager.Instance.PlaySound("Clear");

            scoreText.text = "Score : " + Score.score;
            StartCoroutine(winActive(0.5f));


            if (scene.name == "Stage1")
            {
                winText.text = "시간 보너스 점수 " + leftTime + "!\n잠시 후 다음 스테이지로 넘어갑니다";
                StartCoroutine(NewSceneLoad("Stage2"));
            }
            else if (scene.name == "Stage2")
            {
                winText.text = "시간 보너스 점수 " + leftTime + "!\n잠시 후 다음 스테이지로 넘어갑니다";
                StartCoroutine(NewSceneLoad("Stage3"));
            }
            else
            {
                winText.text = "모든 스테이지를 클리어했습니다\n최종 점수 : " + Score.score;
                Score.save(Score.score);
            }
        }
    }

    IEnumerator winActive(float time)
    {
        yield return new WaitForSeconds(time);
        winMenu.SetActive(true);
    }

    IEnumerator NewSceneLoad(string scene)
    {
        yield return new WaitForSeconds(3.0f);
        SceneManager.LoadScene(scene);
    }

    bool isSame() // 뒤집은 두 카드가 같은 카드면 true
    {
        if (firstCard.GetComponent<Card>().cardType == secondCard.GetComponent<Card>().cardType)
        {
            return true;
        }
        return false;
    }
}
