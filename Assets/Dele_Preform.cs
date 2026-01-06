using Cysharp.Threading.Tasks;
using RenaissanceRestart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class Dele_Preform : PerformBase
{
    public override string PerformName => "asd";

    protected override async UniTask Run()
    {
        await Waitable(() => {

            return true;
        } );

        bool b =await  Run_();
    }
    async UniTask<bool> Run_()
    {
        return true;
    }

}

