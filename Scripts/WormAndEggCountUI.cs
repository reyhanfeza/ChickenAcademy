
using UnityEngine;

using TMPro;
using System.Collections;

public class WormAndEggCountUI : MonoBehaviour
{
 
    [SerializeField] TextMeshProUGUI eggText;
    [SerializeField] TextMeshProUGUI wormText;
    [SerializeField] TextMeshProUGUI TextFeedBack;
 
    public void EggCountUI(int eggCount)
    {
        eggText.text = eggCount.ToString();
    }
    public void WromCountUI(int wormCount)
    {
        wormText.text = wormCount.ToString();
    }
    public void FeedBackFullStack(string stackName)
    {
        StartCoroutine(feedBackTimer(stackName));
    }
    IEnumerator feedBackTimer(string stackName)
    {
        TextFeedBack.gameObject.SetActive(true);
        TextFeedBack.text = "I can't stack any more " + stackName;
        yield return new WaitForSeconds(3);
        TextFeedBack.gameObject.SetActive(false);
    }

}
