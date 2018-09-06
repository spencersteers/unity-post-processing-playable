using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcessingPlayable
{
    public class PostProcessMixerBehaviour : PlayableBehaviour
    {
        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                ScriptPlayable<PostProcessBehaviour> playableInput = (ScriptPlayable<PostProcessBehaviour>)playable.GetInput(i);
                PostProcessBehaviour input = playableInput.GetBehaviour();
                
                float inputWeight = playable.GetInputWeight(i);
                input.postProcessVolume.weight = inputWeight;
           }
        }
    }
}
