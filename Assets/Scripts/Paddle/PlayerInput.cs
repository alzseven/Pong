using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace pong{
    public class PlayerInput : IPaddelInput
    {
        private string axisName;
        public float VertDir { get; private set; }

        public PlayerInput(string _axisName) => axisName = _axisName;
        public void ReadInput() => VertDir = Input.GetAxisRaw(axisName);
    }

    public interface IPaddelInput{
        void ReadInput();
        float VertDir{ get; }
    }
}