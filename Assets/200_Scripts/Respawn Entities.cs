using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnEntities : MonoBehaviour
{
    PlayerController pc;
    //[HideInInspector]
    //public Vector2 startPos;
    //bool isThereEntity;
    //private EntityType whatEntity;
    //void Start()
    //{
    //    startPos = this.gameObject.transform.position;
    //}
    //private void Update()
    //{
    //    isThereEntity = FindObjectOfType<RespawnEntities>(true);
    //}
    private void Update()
    {
        if (pc.power != 1) this.gameObject.SetActive(true);
    }
    
}

//public enum EntityType
//{
//    Fire,
//    Ice,
//    Electric
//}
