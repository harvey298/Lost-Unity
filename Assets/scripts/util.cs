using UnityEngine;
using System.Collections.Generic;

public class PointGenerator
{
    public GameObject player;
    public int numberOfPoints = 16;
    
    public List<Vector3> GeneratePointsAroundPlayer(float radius)
    {
        List<Vector3> points = new List<Vector3>();
        float angleIncrement = 360.0f / numberOfPoints;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * angleIncrement;
            float x = player.transform.position.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
            float z = player.transform.position.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector3 pointPosition = new Vector3(x, player.transform.position.y, z);
            points.Add(pointPosition);
        }

        return points;
    }
}
