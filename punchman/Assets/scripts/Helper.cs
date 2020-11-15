using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.scripts
{
    internal static class Helper
    {
        public static float GetAnimLength(string name, GameObject gameObject)
        {
            var anim = gameObject.GetComponent<Animator>();
            var ac = anim.runtimeAnimatorController;
            return ac.animationClips.FirstOrDefault(t => t.name == name).length;
        }
    }
}
