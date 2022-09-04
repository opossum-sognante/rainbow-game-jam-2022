using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMarkerHandler : MonoBehaviour
{
    private bool IsFollowing = true;

    private CharacterInfo.Players ThisPlayer;

    public CharacterInfo.Players Player;

    private SpriteRenderer SpriteRenderer;

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnStateChanged(bool isFollowing)
    {
        IsFollowing = isFollowing;
        UpdateMarker();
    }

    public void OnPlayerChanged(CharacterInfo.Players thisPlayer)
    {
        ThisPlayer = thisPlayer;
        UpdateMarker();
    }

    private void UpdateMarker()
    {
        SpriteRenderer.enabled = Player == ThisPlayer && !IsFollowing;
    }
}
