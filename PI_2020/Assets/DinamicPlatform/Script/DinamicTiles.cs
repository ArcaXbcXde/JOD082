using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DinamicTiles : DinamicEvents
{   
   public enum TypeOfChange {Move,Scale,Color }
    [Space]
   public TypeOfChange enumTypeOfChange;



    void Awake()
    {
        PlataformOff();
    }


    public void PlataformOn()
    {
        switch (enumTypeOfChange)
        {
            case TypeOfChange.Move:
                iTweenMoveOn();
                break;
            case TypeOfChange.Scale:
                iTweenScaleOn();
                break;
            case TypeOfChange.Color:
                iTweenColorOn();
                break;
            default:
                break;
        }
    }

    public void PlataformOff()
    {
        switch (enumTypeOfChange)
        {
            case TypeOfChange.Move:
                iTweenMoveOff();
                break;
            case TypeOfChange.Scale:
                iTweenScaleOff();
                break;
            case TypeOfChange.Color:
                iTweenColorOff();
                break;
            default:
                break;
        }
        
    }
    
}
