using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong{
    public class AIInput : IPaddelInput
    {
        public float VertDir { get; private set;}

        public void ReadInput()
        {
            VertDir = Random.Range(-1f, 1f);
        }
    }
}

