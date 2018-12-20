using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestPlayer : MonoBehaviour
    {

        [SerializeField]
        private float moveSpeed = 10f;

        // Update is called once per frame
        void Update()
        {
            Vector3 input = new Vector3();
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Vertical");

            input *= Time.deltaTime * moveSpeed;

            input += transform.position;

            transform.position = input;
        }
    }
}

