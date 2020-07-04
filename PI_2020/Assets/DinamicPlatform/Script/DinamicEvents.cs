using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DinamicEvents : MonoBehaviour
{
    #region class
        [System.Serializable]
        public class DinamicColor
        {
            public Color colorOn;
            public Color colorOff;
            public float _time;
        }

        [System.Serializable]
        public class DinamicMove
        {
            public Vector3 positionOn;
            public Vector3 positionOff;
            public float _time;
        }

        [System.Serializable]
        public class DinamicScale
        {
            public Vector3 scaleOn;
            public Vector3 scaleOff;
            public float _time;
        }

        public DinamicColor dinamicColor;
        public DinamicMove dinamicMove;
        public DinamicScale dinamicScale;
    #endregion

    [Space]
    public GameObject _obj;

    Vector3 posOn;
    Vector3 posOff;
    private void Awake()
    {
       

       
    }
    

    public void iTweenScaleOn()
    {
        iTween.ScaleTo(_obj, dinamicScale.scaleOn, dinamicScale._time);

    }
    public void iTweenScaleOff()
    {
        iTween.ScaleTo(_obj, dinamicScale.scaleOff, dinamicScale._time);

    }


    public void iTweenColorOn()
    {
        iTween.ColorTo(_obj, dinamicColor.colorOn, dinamicColor._time);
    }
    public void iTweenColorOff()
    {
        iTween.ColorTo(_obj, dinamicColor.colorOff, dinamicColor._time);
    }


    public void iTweenMoveOn()
    {
        posOn = transform.position + dinamicMove.positionOn;
        iTween.MoveTo(_obj, posOn, dinamicMove._time);
    }
    public void iTweenMoveOff()
    {
        posOff = transform.position + dinamicMove.positionOff;
        iTween.MoveTo(_obj, posOff, dinamicMove._time);
    }
}
