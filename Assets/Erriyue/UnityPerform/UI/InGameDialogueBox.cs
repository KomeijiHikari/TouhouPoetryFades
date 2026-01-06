using System;
using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace RenaissanceRestart
{
    public class InGameDialogueBox : MonoBehaviour
    {
        public Transform Parent;
        public TextMeshPro Text;


        private void Awake()
        {
            Parent.localScale = new Vector3(0, 0, 1);
            Text.maxVisibleCharacters = 0;
            Text.text = "";
        }

    }
}

