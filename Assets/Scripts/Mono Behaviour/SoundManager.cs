using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Game Events")]
    [SerializeField] GameEvent levelUp          = default;
    [SerializeField] GameEvent pieceRotated     = default;
    [SerializeField] GameEvent pieceMoved       = default;
    [SerializeField] GameEvent pieceSolidified  = default;
    [SerializeField] GameEvent rowDeleted       = default;


    [Header("Audio sources")]
    [SerializeField] List<AudioSource> deleteRowCombos = default;
    [SerializeField] AudioSource solidifyPiece = default;
    [SerializeField] AudioSource rotatePiece = default;
    [SerializeField] AudioSource movePiece = default;
    //[SerializeField] AudioSource levelUpAudio = default;

    private void Awake()
    {
        levelUp.RegisterListener(OnLevelUp);
        pieceRotated.RegisterListener(OnRotatePiece);
        pieceMoved.RegisterListener(OnPieceMoved);
        pieceSolidified.RegisterListener(OnPieceSolidified);
        rowDeleted.RegisterListener(OnRowDeleted);
    }

    private void OnLevelUp()
    {
        //levelUpAudio.Play();
    }

    public void OnRowDeleted()
    {
        int comboIndex = rowDeleted.sentInt;
        deleteRowCombos[comboIndex - 1].Play();
    }

    public void OnPieceSolidified()
    {
        solidifyPiece.Play();
    }

    public void OnRotatePiece()
    {
        rotatePiece.Play();
    }

    public void OnPieceMoved()
    {
        movePiece.Play();
    }
}
