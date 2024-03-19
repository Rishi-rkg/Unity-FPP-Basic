using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Camera : MonoBehaviour
{
   //VARIABLES
    [SerializeField] private float MouseSensi;

   //REFERENCES
   private Transform parent;

   private void Start()
   {
    parent = transform.parent;

    UnityEngine.Cursor.lockState = CursorLockMode.Locked;

   }
   private void Update()
   {
    Rotate();
   }
   private void Rotate()
   {
        float MouseX = Input.GetAxis("Mouse X") * MouseSensi * Time.deltaTime;
        parent.Rotate(Vector3.up, MouseX);
   }

}
