using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyFaint : MonoBehaviour
{
    public UnityEvent Fainted;

    public UnityEvent Awoken;

    private List<CharacterInfo> CharacterInfoInArea = new();

    public float FaintDuration = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        OnTrigger(other, CharacterInfoInArea.Add);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        OnTrigger(other, ci => CharacterInfoInArea.Remove(ci));
    }

    private void OnTrigger(Collider2D other, Action<CharacterInfo> action)
    {
        var characterInfo = other.gameObject.transform
            .parent.gameObject.GetComponent<CharacterInfo>();
        if (characterInfo != null)
        {
            action(characterInfo);
        }
    }

    public void OnPlayer1Action()
    {
        OnPlayerAction(CharacterInfo.Players.P1);
    }

    public void OnPlayer2Action()
    {
        OnPlayerAction(CharacterInfo.Players.P2);
    }

    private void OnPlayerAction(CharacterInfo.Players p)
    {
        foreach (var characterInfo in CharacterInfoInArea)
        {
            if (characterInfo.Player == p && !characterInfo.IsFollower)
            {
                if (characterInfo.Character == CHARACTER.MARIELLE)
                {
                    Faint();
                }
                break;
            }
        }
    }

    private void Faint()
    {
        Fainted.Invoke();
        StartCoroutine(ResumeFromFainted());
    }

    private IEnumerator ResumeFromFainted()
    {
        yield return new WaitForSeconds(FaintDuration);
        Awoken.Invoke();
    }
}
