using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] List<AudioSource> deleteRowCombos = default;
    [SerializeField] AudioSource solidifyPiece = default;
    [SerializeField] AudioSource rotatePiece = default;
    [SerializeField] AudioSource movePiece = default;
    [SerializeField] AudioSource tick1 = default;
    [SerializeField] AudioSource tick2 = default;

    public void DeleteRow(int comboIndex)
    {
        deleteRowCombos[comboIndex - 1].Play();
    }

    public void SolidifyPiece()
    {
        solidifyPiece.Play();
    }

    public void RotatePiece()
    {
        rotatePiece.Play();
    }

    public void MovePiece()
    {
        movePiece.Play();
    }

    int lastTick = 1;
    public void Tick()
    {
        if (lastTick == 1)
        {
            tick2.Play();
            lastTick = 2;
        }
        else
        {
            tick1.Play();
            lastTick = 1;
        }
    }
}
