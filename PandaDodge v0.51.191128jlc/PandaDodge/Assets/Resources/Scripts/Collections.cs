 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 
 public class ImageShow : MonoBehaviour {
 
     public bool isImgOn;
     public Image img;
 
     void Start () {
 
         img.enabled = true;
         isImgOn = true;
     }
 
     void Update () {
     
         if (Input.GetKeyDown ("i")) {
 
             if (isImgOn == true) {
 
                 img.enabled = false;
                 isImgOn = false;
             }
 
             else {
 
                 img.enabled = true;
                 isImgOn = true;
             }
         }
     }
 }