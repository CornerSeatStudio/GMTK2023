using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InvalidPlacementArea : MonoBehaviour
{
    private BoxCollider m_Collider;
    public bool NoPinsOnly;
    private void Awake()
    {
        m_Collider = GetComponent<BoxCollider>();
    }

    private void OnDrawGizmos()
    {
        //if (m_collider != null)
        //{
        //    gizmos.color = color.red;

        //    // calculate the bounds corners of the boxcollider
        //    vector3 center = transform.position + m_collider.center;
        //    vector3 size = m_collider.size;
        //    vector3 halfsize = size / 2f;


        //    vector3[] boundscorners = new vector3[8];
        //    boundscorners[0] = center + new vector3(-halfsize.x, -halfsize.y, -halfsize.z);
        //    boundscorners[1] = center + new vector3(-halfsize.x, -halfsize.y, halfsize.z);
        //    boundscorners[2] = center + new vector3(halfsize.x, -halfsize.y, halfsize.z);
        //    boundscorners[3] = center + new vector3(halfsize.x, -halfsize.y, -halfsize.z);
        //    boundscorners[4] = center + new vector3(-halfsize.x, halfsize.y, -halfsize.z);
        //    boundscorners[5] = center + new vector3(-halfsize.x, halfsize.y, halfsize.z);
        //    boundscorners[6] = center + new vector3(halfsize.x, halfsize.y, halfsize.z);
        //    boundscorners[7] = center + new vector3(halfsize.x, halfsize.y, -halfsize.z);

        //    // draw the bounds edges using gizmos.drawline
        //    gizmos.drawline(boundscorners[0], boundscorners[1]);
        //    gizmos.drawline(boundscorners[1], boundscorners[2]);
        //    gizmos.drawline(boundscorners[2], boundscorners[3]);
        //    gizmos.drawline(boundscorners[3], boundscorners[0]);

        //    gizmos.drawline(boundscorners[4], boundscorners[5]);
        //    gizmos.drawline(boundscorners[5], boundscorners[6]);
        //    gizmos.drawline(boundscorners[6], boundscorners[7]);
        //    gizmos.drawline(boundscorners[7], boundscorners[4]);

        //    gizmos.drawline(boundscorners[0], boundscorners[4]);
        //    gizmos.drawline(boundscorners[1], boundscorners[5]);
        //    gizmos.drawline(boundscorners[2], boundscorners[6]);
        //    gizmos.drawline(boundscorners[3], boundscorners[7]);
        //}

    }
}
