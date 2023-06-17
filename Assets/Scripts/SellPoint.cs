using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellPoint : MonoBehaviour
{
    [HideInInspector] private Coroutine sellCo;
    [SerializeField][Range(0.1f, 100.0f)] private float sellIntervalInSecond = 0.1f;

    [HideInInspector] public const string MONEYPREFKEY = "MONEYPREFKEY";

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                sellCo = StartCoroutine(sell());
                break;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                StopCoroutine(sellCo);
                break;
        }
    }

    private IEnumerator sell()
    {
        yield return new WaitForEndOfFrame();

        while (CharacterController.ins.CollectedGemCount() > 0)
        {
            GemScript scr = CharacterController.ins.GetCollectedGem();
            PlayerPrefs.SetFloat(MONEYPREFKEY, PlayerPrefs.GetFloat(MONEYPREFKEY) + scr.GetPrice());
            PlayerPrefs.SetInt(scr.gemSelf.GemName, PlayerPrefs.GetInt(scr.gemSelf.GemName) + 1);
            scr.GoToPosition(this.transform.position);
            GameObject.Destroy(scr.gameObject, scr.moveTimeInSecond);

            TotalCoinController.Instance.SetCoinText();

            yield return new WaitForSeconds(sellIntervalInSecond);
        }
    }
}
