using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InvalidPlacementArea : MonoBehaviour
{
    public BoxCollider m_Collider;


    private void OnDrawGizmos()
    {
        if (m_Collider != null)
        {
            Gizmos.color = Color.red;

            // Calculate the bounds corners of the BoxCollider
            Vector3 center = transform.position;
            Vector3 size = m_Collider.size;
            Vector3 halfSize = size / 2f;


            Vector3[] boundsCorners = new Vector3[8];
            boundsCorners[0] = center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
            boundsCorners[1] = center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
            boundsCorners[2] = center + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
            boundsCorners[3] = center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
            boundsCorners[4] = center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
            boundsCorners[5] = center + new Vector3(-halfSize.x, halfSize.y, halfSize.z);
            boundsCorners[6] = center + new Vector3(halfSize.x, halfSize.y, halfSize.z);
            boundsCorners[7] = center + new Vector3(halfSize.x, halfSize.y, -halfSize.z);

            // Draw the bounds edges using Gizmos.DrawLine
            Gizmos.DrawLine(boundsCorners[0], boundsCorners[1]);
            Gizmos.DrawLine(boundsCorners[1], boundsCorners[2]);
            Gizmos.DrawLine(boundsCorners[2], boundsCorners[3]);
            Gizmos.DrawLine(boundsCorners[3], boundsCorners[0]);

            Gizmos.DrawLine(boundsCorners[4], boundsCorners[5]);
            Gizmos.DrawLine(boundsCorners[5], boundsCorners[6]);
            Gizmos.DrawLine(boundsCorners[6], boundsCorners[7]);
            Gizmos.DrawLine(boundsCorners[7], boundsCorners[4]);

            Gizmos.DrawLine(boundsCorners[0], boundsCorners[4]);
            Gizmos.DrawLine(boundsCorners[1], boundsCorners[5]);
            Gizmos.DrawLine(boundsCorners[2], boundsCorners[6]);
            Gizmos.DrawLine(boundsCorners[3], boundsCorners[7]);
        }

    }
}
