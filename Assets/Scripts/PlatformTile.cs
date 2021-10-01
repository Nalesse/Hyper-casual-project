using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTile : MonoBehaviour
{
    #region inspector fields
    [SerializeField] private Transform endPoint;
    [SerializeField] private GameObject[] obstacles;
    #endregion
    public Transform startPoint;

    public void ActivateRandomObstacle()
    {
        int randomIndex = Random.Range(0, obstacles.Length);
        obstacles[randomIndex].SetActive(true);
    }

    public void DeactivateObsticles()
    {
        for(int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].SetActive(false);
        }
    }
}
