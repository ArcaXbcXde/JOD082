using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance {  get { return instance; } }


    public Transform audiolistOBJ;

    GameObject[] sFXList;



    private void Awake()
    {
        instance = this;
        sFXList = new GameObject[audiolistOBJ.childCount];
        for (int i = 0; i < audiolistOBJ.childCount; i++)
        {
            sFXList[i] = audiolistOBJ.GetChild(i).gameObject;
        }
    }

    public void PlaySFX(int _index)
    {
        sFXList[_index].SendMessage("Play");
    }

    public void StopSFX(int _index)
    {
        sFXList[_index].SendMessage("Stop");
    }
}
