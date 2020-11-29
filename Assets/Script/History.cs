using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class History : MonoBehaviour
{
    private int highest = 0;
    [SerializeField] private RectTransform content = null;
    [SerializeField] private Transform SpawnPoint = null;
    [SerializeField] private GameObject item = null;
    [SerializeField] private Text highestText;

    // Start is called before the first frame update
    void Start()
    {
        int historyScore = 0;
        int num = Score.historynum;

        content.sizeDelta = new Vector2(0, num * 100);

        for (int i = num; i > 0; i--)
        {
            historyScore = PlayerPrefs.GetInt(i.ToString(), 0);
            if (historyScore > highest)
                highest = historyScore;

            Vector3 pos = new Vector3(0, 100 * i - 50 * num - 50, SpawnPoint.position.z);
            GameObject SpawnedItem = Instantiate(item, pos, SpawnPoint.rotation);
            SpawnedItem.transform.SetParent(SpawnPoint, false);
            HistoryItem itemDetails = SpawnedItem.GetComponent<HistoryItem>();
            itemDetails.num.text = i.ToString();
            itemDetails.score.text = historyScore.ToString();
        }
        highestText.text = "최고 점수 : " + highest;
    }
}
