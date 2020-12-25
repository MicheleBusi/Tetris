using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Lean.Transition.Extras;

public class SpawnScoreAdd : MonoBehaviour
{
    [SerializeField] GameEvent scoreChanged = default;
    [SerializeField] GameObject scoreAddPrefab = default;
    [SerializeField] RectTransform spawnHere = default;
    [SerializeField] RectTransform transitionTarget = default;

    private void Awake()
    {
        scoreChanged.RegisterListener(Spawn);
    }

    public void Spawn()
    {
        // If the score didn't actually change, don't do anything
        if (scoreChanged.sentInt == 0)
        {
            return;
        }

        var GO = Instantiate(scoreAddPrefab, spawnHere);
        var rt = GO.GetComponent<RectTransform>();
        rt.localPosition = Vector3.zero;
        rt.SetParent(transitionTarget, true);

        var text = GO.GetComponent<Text>();
        text.text = "+" + scoreChanged.sentInt.ToString();

        var animation = GO.GetComponent<LeanAnimation>();
        animation.BeginTransitions();

        Destroy(GO, 1.2f);
    }

    //void OnScoreChanged()
    //{
    //    // Instantiate Add Score prefab
    //    GameObject scoreIncreaseText = Instantiate(addScoreTextPrefab, canvas.transform, false);
    //    RectTransform rt = scoreIncreaseText.GetComponent<RectTransform>();
    //    rt.anchoredPosition = Vector3.zero;
    //    Text text = scoreIncreaseText.GetComponent<Text>();
    //    text.text = "+" + scoreChange.ToString();
    //    text.fontSize += scoreChange / 25;
    //    StartCoroutine(ScoreChangeAnimation());
    //
    //
    //    // Slide towards score
    //    rt.SetParent(this.transform, worldPositionStays: true);
    //    rt.anchoredPositionTransition(new Vector2(0, 0), .7f, LeanEase.Accelerate);
    //}
}
