using System;
using System.Collections.Generic;
using UnityEngine;

public class MyReaction : IPathFinder
{
    public IEnumerable<Vector2> GetPath(Vector2 A, Vector2 C, IEnumerable<Edge> edges)
    {

        List<Vector2> path = new List<Vector2>() { A };
        List<Edge> myEdges = new List<Edge>(edges);
        if (!IsPointOnTheField(A)|| !IsPointOnTheField(C)|| IsTheFieldDamaged()) return new List<Vector2>();

        for (int i = SearchForTheNumberOfTheStartingField(); i<myEdges.Count;i++)
        {
            path.AddRange(NewGetNextPoints(path[path.Count-1], myEdges[i]));
            if(CheckingBoundaries(C,myEdges[i].Second)) break;
        }
        path.Add(C);
        return path;

        List<Vector2> NewGetNextPoints(Vector2 oldPoint, Edge edge)
        {
            List<Vector2> result = new List<Vector2>();
            Vector2 edgeCentr = edge.Start + (edge.End - edge.Start) / 2;
            if ((int)oldPoint.x != (int)edgeCentr.x && (int)oldPoint.y != (int)edgeCentr.y)
            {
                return new List<Vector2>(){edgeCentr};
            }
            else
            {
                Vector2 rectangleCentr = edge.First.Min + (edge.First.Max - edge.First.Min) / 2;
                return new List<Vector2>() { rectangleCentr,edgeCentr };
            }
        }
        bool IsPointOnTheField(Vector2 point)
        {
            for (int i = 0; i < myEdges.Count; i++)
            {
                if (CheckingBoundaries(point, myEdges[i].First)) return true;
            }
            if (CheckingBoundaries(point, myEdges[myEdges.Count-1].Second)) return true;
            return false;
        }
        int SearchForTheNumberOfTheStartingField()
        {
            for (int i = 0; i < myEdges.Count; i++)
            {
                if (CheckingBoundaries(A, myEdges[i].First)) return i;
            }
            if (CheckingBoundaries(A, myEdges[myEdges.Count-1].Second)) return myEdges.Count;
            return myEdges.Count;
        }
        bool CheckingBoundaries(Vector2 point,Rectangle rectangle)
        {
            if (point.x >= rectangle.Min.x && point.x <= rectangle.Max.x
                    && point.y >= rectangle.Min.y && point.y <= rectangle.Max.y) return true;
            return false;
        }
        bool IsTheFieldDamaged()
        {
            for (int i = 0; i < myEdges.Count; i++)
            {
                if (IsPointsAreMixedUp(myEdges[i].Start, myEdges[i].End)
                    || IsPointsAreMixedUp(myEdges[i].First.Min, myEdges[i].First.Max)
                    || IsPointsAreMixedUp(myEdges[i].Second.Min, myEdges[i].Second.Max)) return true;
                Vector2 edgeCentr = myEdges[i].Start + (myEdges[i].End - myEdges[i].Start) / 2;
                if (!CheckingBoundaries(edgeCentr, myEdges[i].First)|| !CheckingBoundaries(edgeCentr, myEdges[i].Second)) return true;
            }
            return false;
            bool IsPointsAreMixedUp(Vector2 point1, Vector2 point2)
            {
                if (point1.x > point2.x || point1.y > point2.y) return true;
                return false;
            }
        }

    }
}

