using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DEBUG_OptionsInterface : MonoBehaviour
{
    public bool enableAutoCompleteEmailCheat = false;

    private void Start()
    {
        Options.SetAutoCompleteEmail(enableAutoCompleteEmailCheat);
    }
}