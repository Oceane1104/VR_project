//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using UnityEngine;
//using Debug = UnityEngine.Debug;

//public class stay_on_target : MonoBehaviour
//{
//    public float in_targ = 3 / 10;

//    public void stay_there(Rigidbody rb)
//    {
//        Vector3 new_pos = new(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z + in_targ);
//        rb.transform.position = new_pos;
//        rb.isKinematic = true;
//    }
//}
