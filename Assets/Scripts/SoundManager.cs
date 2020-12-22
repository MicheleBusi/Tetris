using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] BoardEventChannel boardEventChannel = default;

    [Header("Audio sources")]
    [SerializeField] List<AudioSource> deleteRowCombos = default;
    [SerializeField] AudioSource solidifyPiece = default;
    [SerializeField] AudioSource rotatePiece = default;
    [SerializeField] AudioSource movePiece = default;
    [SerializeField] AudioSource levelUp = default;

    private void Awake()
    {
        boardEventChannel.OnLevelUp += OnLevelUp;
        boardEventChannel.OnMovePieceHorizontally += OnMoveHorizontally;
        boardEventChannel.OnRotatePiece += OnRotatePiece;
        boardEventChannel.OnRowDeleted += OnRowDeleted;
        boardEventChannel.OnSolidfyPiece += OnSolidifyPiece;
    }

    private void OnLevelUp(int level)
    {
        levelUp.Play();
    }

    public void OnRowDeleted(int comboIndex)
    {
        deleteRowCombos[comboIndex - 1].Play();
    }

    public void OnSolidifyPiece()
    {
        solidifyPiece.Play();
    }

    public void OnRotatePiece()
    {
        rotatePiece.Play();
    }

    public void OnMoveHorizontally(int deltaX)
    {
        movePiece.Play();
    }
}
