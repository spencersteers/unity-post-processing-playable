using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering.PostProcessing;

namespace SpencerSteers.PostProcessingPlayable
{
    public class PostProcessMixerBehaviour : PlayableBehaviour
    {
        public Dictionary<int, PostProcessVolume> profileVolumes;

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            int inputCount = playable.GetInputCount();
            for (int i = 0; i < inputCount; i++)
            {
                ScriptPlayable<PostProcessBehaviour> playableInput = (ScriptPlayable<PostProcessBehaviour>)playable.GetInput(i);
                PostProcessBehaviour input = playableInput.GetBehaviour();

                if (input.postProcessProfile == null)
                    continue;

                float normalizedTime = (float)(playableInput.GetTime() / input.timelineClipDuration);
                float weightCurve = Mathf.Clamp01(input.weightCurve.Evaluate(normalizedTime));
                float inputWeight = playable.GetInputWeight(i);
                input.postProcessVolume.weight = inputWeight * weightCurve;

                if (inputWeight > 0 && input.isDebug)
                {
                    DebugUtils.GroupLog(
                        "PostProcessMixerBehaviour",
                        "input.timelineClipDuration", input.timelineClipDuration,
                        nameof(normalizedTime), normalizedTime,
                        nameof(weightCurve), weightCurve,
                        nameof(inputWeight), inputWeight
                    );
                }
            }
        }
    }
}
