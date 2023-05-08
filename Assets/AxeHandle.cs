using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// some calculations so can grasp axe anywhere along handle

public class AxeHandle : MonoBehaviour
{
    Vector3 end;
    Vector3 start;
    Vector3 center;
    Vector3 downDir;

    public float teleport_radius = 1.5f;
    public float grab_radius = 0.2f;
    
    // calculate and return values for useful locations
    // ant these to be recalculated each time so that it updates
    // but not every frame so no time is wasted

    public Vector3 getEnd() { return transform.GetChild(0).position; }

    public Vector3 getCenter() { return transform.position; }

    public Vector3 getDownDir() { return (getEnd() - getCenter()); } // direction from 'center' through axe to end

    public Vector3 getStart() { return (getCenter() - getDownDir()); } // closest spot to handle

    // set variables above
    void setVary()
    {
        end = getEnd(); center = getCenter();
        downDir = getDownDir(); start = getStart();
    }

    // return t which solves equation defined wrt line L(t) = point + t*dir 
    // st || L(t) - sphere_center||^2 = grab_radius^2
    // returning float.MaxValue => complex numbers which we're not accepting
    // return value is smaller value than t2 in case there are two answers
    float intersect(Vector3 dir, Vector3 point, Vector3 sphere_center, out float t2)
    {
        t2 = float.MaxValue; // if stays this way => only one sol'n

        Vector3 diff = point - sphere_center;
        float a = Vector3.Dot(dir, dir);
        float b = 2 * Vector3.Dot(diff, dir);
        float c = Vector3.Dot(diff, diff) - grab_radius * grab_radius;

        float delta = b * b - 4 * a * c;

        if (delta == 0) return -b / (2 * a);

        if (delta < 0) return float.MaxValue;

        t2 = (-b + delta) / (2 * a);

        return (-b - delta) / (2 * a);
    }

    // check if either L(t1) or L(t2) is valid point on handle
    // L1b, L2b true iff t1, t2 on handle, respectively
    void onHandle(float t1, float t2, out bool L1b, out bool L2b)
    {
        Vector3 dir = Vector3.Normalize(downDir);
        Vector3 L1 = center + t1 * dir;
        L1b = false;
        
        Vector3 end_to_L1 = L1 - end; // want this & dir to point in opposite directions
        Vector3 L1_to_start = start - L1; // want this & dir to point in same direction

        if (dir == Vector3.Normalize(L1_to_start) || dir != Vector3.Normalize(end_to_L1))
        {
            Debug.LogWarningFormat("point L1 {0} is on the handle!", L1);
            L1b = true;
        }

        // now do the same for the second point, but only if not maxvalue
        L2b = false;
        if (t2 == float.MaxValue) return;
        Vector3 L2 = center + t2 * dir;

        Vector3 end_to_L2 = L2 - end; // want this & dir to point in opposite directions
        Vector3 L2_to_start = start - L2; // want this & dir to point in same direction

        if (dir == Vector3.Normalize(L2_to_start) || dir != Vector3.Normalize(end_to_L2))
        {
            Debug.LogWarningFormat("point L2 {0} is on the handle!", L2);
            L2b = true;
        }
    }


    // Function: bool getDistance(Vector3 posn, out float dist) where posn is the hand controller position,
    //				dist is the object_distance to be modified, and the function returns
    //				a bool which is true if the object is "close enough" to be grasped
    //              extra: teleport is true if using teleport grab, but the default is false and                    
    // Idea: 
    public bool getDistance(Vector3 posn, out float object_distance, bool teleport = false)
    {
        setVary(); // set vary bc will be using these now
        // pretty simple if using teleport grab since already a lot of "padding"
        object_distance = Vector3.Distance(posn, transform.position);
        if (teleport) return (object_distance < teleport_radius);

        // normal grab; check if withing grasping radius of ANY part of handle
        // radius => SPHERE; see if line defining handle intersects it at any point
        Debug.Log("Get Distance via special method");

        Vector3 dir = Vector3.Normalize(downDir);
        float t2 = 0;
        float t1 = intersect(dir, center, posn, out t2);

        bool L1b; bool L2b;
        onHandle(t1, t2, out L1b, out L2b);

        // finally, return & modify object distance as see fit
        if (!L1b && !L2b) return false;

        if (L1b)
        {
            Vector3 L1 = center + t1 * dir;
            float L1_dist = Vector3.Distance(posn, L1);
            if (!L2b)
            {
                object_distance = L1_dist;
                return true;
            }
            Vector3 L2 = center + t2 * dir;
            float L2_dist = Vector3.Distance(posn, L2);
            object_distance = L1_dist < L2_dist? L1_dist : L2_dist; // hold shortest distance
            return true;
        }

        object_distance = Vector3.Distance(posn, center + t2 * dir);
        return true;
    }
}
