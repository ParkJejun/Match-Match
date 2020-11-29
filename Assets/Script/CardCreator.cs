using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardCreator : MonoBehaviour
{
    [SerializeField] private List<GameObject> cardsList; // 카드 52장
    private List<int> numList; // 52개의 숫자를 저장할 리스트
    private List<int> createdCards; // 게임에 사용될 카드 번호를 저장하는 리스트

    [SerializeField] private int rows = 2;
    [SerializeField] private int columns = 2;
    [SerializeField] private float startX = -1.0f;
    [SerializeField] private float startY = 1.0f;
    [SerializeField] private float xDistance = 3.0f;
    [SerializeField] private float yDistance = 4.0f;
    private GameManager gameManager;

    System.Random rand = new System.Random();

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        CreateCards();
        gameObject.SetActive(false);
    }

    void CreateCards()
    {
        int numOfCards = rows * columns;

        if (numOfCards % 2 == 0)
        {
            gameManager.cardsLeft = numOfCards;
            
            numList = new List<int>();
            createdCards = new List<int>();

            for (int i = 0; i < 52; i++) // 52개의 카드 번호 리스트를 셔플한다.
            {
                numList.Add(i);
            }
            Shuffle(numList);

            for (int i = 0; i < numOfCards / 2; i++) // 셔플된 리스트에서 사용하고자 하는 카드 갯수의 절반만큼 가져와서 2번씩 리스트에 추가한다.
            {
                createdCards.Add(numList[i]);
                createdCards.Add(numList[i]);
            }
            Shuffle(createdCards);

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Instantiate(cardsList[createdCards[i * columns + j]], new Vector3(startX + xDistance * j, startY - yDistance * i, 5.0f), Quaternion.Euler(0.0f, 0.0f, 0.0f));
                }
            }
        }
        else
        {
            Debug.Log("카드 갯수가 짝수여야 합니다.");
        }
    }

    void Shuffle<T>(IList<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int r = rand.Next(i + 1);

            T temp = list[i];
            list[i] = list[r];
            list[r] = temp;
        }
    }
}
