using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TweenTest : MonoBehaviour
{
    public bool isPosition = false;
    public bool isRotate = false;
    public bool isScale = false;


    [System.Serializable]
    public struct TweenRotation
    {
        public Vector3 _Vector;
        public float _duration;
        public int _vibrato;
        public float _elasticity;
    }
    [System.Serializable]
    public struct TweenPosition
    {
        public Vector3 _Vector;
        public float _duration;
        public int _vibrato;
        public float _elasticity;
    }
    [System.Serializable]
    public struct TweenScale
    {
        public Vector3 _Vector;
        public float _duration;
        public int _vibrato;
        public float _elasticity;
    }
    public TweenRotation m_tweenRotation;
    public TweenPosition m_tweenPosition;
    public TweenPosition m_tweenScale;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {

            if (isPosition)
            {
                DoPosition();
            }
            if (isRotate)
            {
                DoRotation();
            }          
            if (isScale)
            {
                DoScale();
            }
        }
    }

    void DoPosition()
    {
        transform.DOPunchPosition(m_tweenPosition._Vector,m_tweenPosition._duration,m_tweenPosition._vibrato, m_tweenPosition._elasticity);
    }
    void DoRotation()
    {
        transform.DOPunchRotation(m_tweenRotation._Vector, m_tweenRotation._duration, m_tweenRotation._vibrato, m_tweenRotation._elasticity);
    }
    void DoScale()
    {
        transform.DOPunchScale(m_tweenScale._Vector, m_tweenScale._duration, m_tweenScale._vibrato, m_tweenScale._elasticity);
    }


}
