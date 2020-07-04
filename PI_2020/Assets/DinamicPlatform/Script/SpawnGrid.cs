using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGrid : MonoBehaviour
{
    public enum GridType {Quad,Hex }
    public GridType enumGridType = GridType.Quad;

    public int sizeX = 2;
    public int sizeY = 2;
    public int sizeZ = 2;
    [Space]
    public float  Xoffset = 0;
    public float  Yoffset = 0;
    public float  Zoffset = 0;
    [Space]
    public float gapX = 1;
    public float gapY = 1;
    public float gapZ = 1;
    [Space]
    public GameObject _hexTile;
    public GameObject _quadTile;

    void Awake()
    {
        CrateGrid();
    }

    //void QuadGrid()
    //{
    //    for (int i = 0; i < sizeX; i++)
    //    {
    //        for (int j = 0; j < sizeZ; j++)
    //        {

    //            Instantiate(_quadTile, new Vector3(
    //               transform.position.x + i,
    //               transform.position.y,
    //               transform.position.z + j),
    //               gameObject.transform.rotation, gameObject.transform);


    //        }
    //    }
    //}

    //void HexGrid()
    //{
    //    for (int i = 0; i < sizeX; i++)
    //    {
    //        for (int j = 0; j < sizeZ; j++)
    //        {

    //            if (i % 2 == 0)
    //            {
    //                Instantiate(_hexTile, new Vector3(
    //                (transform.position.x + i * gapX) + Xoffset,
    //                transform.position.y,
    //                (transform.position.z + j * gapZ) + Zoffset),
    //               gameObject.transform.rotation, gameObject.transform);

    //            }
    //            else
    //            {
    //                Instantiate(_hexTile, new Vector3(
    //                transform.position.x + i * gapX,
    //                transform.position.y,
    //                transform.position.z + j * gapZ),
    //               gameObject.transform.rotation, gameObject.transform);

    //            }
    //        }
    //    }
    //}

    void CrateGrid()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                for (int k = 0; k < sizeY; k++)
                {
                    switch (enumGridType)
                    {
                        case GridType.Quad:
                            Instantiate(_quadTile, GridPosition(i,k, j, true),
                       gameObject.transform.rotation, gameObject.transform);
                            break;
                        case GridType.Hex:
                            Instantiate(_hexTile, GridPosition(i,k, j, false),
                       gameObject.transform.rotation, gameObject.transform);
                            break;
                        default:
                            break;
                    }
                }
                
            }
        }
    }

    Vector3 GridPosition(int _x,int _y, int _z, bool isQuad)
    {
        float xPosition;
        float yPosition;
        float zPosition;

        Vector3 _gridPosition;
        if (isQuad)
        {
            xPosition = (transform.position.x + _x * gapX);
            yPosition = (transform.position.y + _y * gapY);
            zPosition = (transform.position.z + _z * gapZ);

            _gridPosition = new Vector3(xPosition, yPosition, zPosition);

            return _gridPosition;
        }

        else
        {
            if (_x % 2 == 0)
            {
                xPosition = (transform.position.x + _x * (gapZ * 0.8f)) + Xoffset;
                yPosition = (transform.position.y + _y * (gapZ * 0.8f)) + Yoffset;
                zPosition = (transform.position.z + _z * gapZ) + (gapZ/2);

                _gridPosition = new Vector3(xPosition, yPosition, zPosition);

                return _gridPosition;
            }
            else
            {
                xPosition = (transform.position.x + _x * (gapZ * 0.8f));
                yPosition = (transform.position.y + _y * (gapY * 0.8f));
                zPosition = (transform.position.z + _z * gapZ);

                _gridPosition = new Vector3(xPosition, yPosition, zPosition);

                return _gridPosition;
            }           
        }       
    }

    #region Gizmos

    void OnDrawGizmos()
    {
        GizmosGrid();

        /*switch (enumGridType)
        {
            case GridType.Quad:
                Gizmos.color = Color.blue;
                QuadGridGizmos();
                break;
            case GridType.Hex:
                Gizmos.color = Color.yellow;
                HexGridGizmos();
                break;
            default:
                break;
        }*/
    }
    void GizmosGrid()
    {
        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeZ; j++)
            {
                for (int k = 0; k < sizeY; k++)
                {
                    switch (enumGridType)
                    {
                        case GridType.Quad:
                            Gizmos.color = Color.blue;
                            Gizmos.DrawWireCube(GridPosition(i, k, j, true), Vector3.one / 10);

                            break;
                        case GridType.Hex:
                            Gizmos.color = Color.yellow;
                            Gizmos.DrawWireCube(GridPosition(i, k, j, false), Vector3.one / 10);

                            break;
                        default:
                            break;
                    }
                }
               
            }
        }
    }


    //void QuadGridGizmos()
    //{
    //    for (int i = 0; i < sizeX; i++)
    //    {
    //        for (int j = 0; j < sizeZ; j++)
    //        {

    //            Gizmos.DrawWireCube(GridPosition(j,i,true),
    //                Vector3.one);


    //        }
    //    }
    //}

    //void HexGridGizmos()
    //{
    //    for (int i = 0; i < sizeX; i++)
    //    {
    //        for (int j = 0; j < sizeZ; j++)
    //        {

    //            if (i % 2 ==0)
    //            {
    //                Gizmos.DrawWireCube(new Vector3(
    //                (transform.position.x + i * gapX)  + Xoffset,
    //                transform.position.y,
    //                (transform.position.z + j * gapZ) + Zoffset),
    //                Vector3.one);
    //            }
    //            else
    //            {
    //                Gizmos.DrawWireCube(new Vector3(
    //                transform.position.x + i * gapX,
    //                transform.position.y,
    //                transform.position.z + j * gapZ),
    //                Vector3.one);
    //            }
    //        }
    //    }
    //}

    #endregion
}
