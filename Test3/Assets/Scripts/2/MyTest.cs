using System.Collections.Generic;
using UnityEngine;

public class MyTest:MonoBehaviour
{
    List<Edge> edges = new List<Edge>()
        {
            new Edge(
                new Rectangle(new Vector2(-15,15),new Vector2(2,25)),
                new Rectangle(new Vector2(-3,25),new Vector2(17,35)),
                new Vector2(-3,25),
                new Vector2(2,25)),

            new Edge(
                new Rectangle(new Vector2(-3,25),new Vector2(17,35)),
                new Rectangle(new Vector2(17,20),new Vector2(37,30)),
                new Vector2(17,25),
                new Vector2(17,30)),
            
            new Edge(
                new Rectangle(new Vector2(17,20),new Vector2(37,30)),
                new Rectangle(new Vector2(12,3),new Vector2(17,23)),
                new Vector2(17,20),
                new Vector2(17,23)),

            new Edge(
                new Rectangle(new Vector2(12,3),new Vector2(17,23)),
                new Rectangle(new Vector2(-33,3),new Vector2(12,13)),
                new Vector2(12,3),
                new Vector2(12,13)),

            new Edge(
                new Rectangle(new Vector2(-33,3),new Vector2(12,13)),
                new Rectangle(new Vector2(-30,-7),new Vector2(-28,3)),
                new Vector2(-30,3),
                new Vector2(-28,3))
        };
    private void OnDrawGizmosSelected()
    {
        List<Vector2> path = new List<Vector2>(new MyReaction().GetPath(new Vector2(-6.5f, 15), new Vector2(-29, -7), edges));
        if (path.Count ==0)return;


        Gizmos.color = Color.yellow;
        for (int i = 0; i < edges.Count; i++)
        {
            Gizmos.DrawCube(GetCentrCube(edges[i].First),
                new Vector3(edges[i].First.Max.x - edges[i].First.Min.x, edges[i].First.Max.y - edges[i].First.Min.y, 0));
        }
        Gizmos.DrawCube(GetCentrCube(edges[edges.Count - 1].Second),
                new Vector3(edges[edges.Count - 1].Second.Max.x - edges[edges.Count - 1].Second.Min.x, edges[edges.Count - 1].Second.Max.y - edges[edges.Count - 1].Second.Min.y, 0));
        Vector3 GetCentrCube(Rectangle rectangle)
        {
            Vector2 delta = (rectangle.Max - rectangle.Min) / 2;
            return new Vector3(rectangle.Min.x, rectangle.Min.y, 0) + new Vector3(delta.x, delta.y, 0);
        }

        Gizmos.color = Color.red;
        for (int i = 0; i < path.Count - 1; i++)
        {
            Gizmos.DrawLine(path[i], path[i + 1]);
        }

    }
}

