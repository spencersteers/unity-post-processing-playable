using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering.PostProcessing;

namespace SpencerSteers.PostProcessingPlayable
{
    [Serializable]
    public class PostProcessBehaviour : PlayableBehaviour
    {
        public AnimationCurve weightCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
        public PostProcessProfile postProcessProfile;
        public PostProcessVolume postProcessVolume;

        public bool isDebug = false;

        // Reference to the TimelineClip so that real start / end times can be used when extrapolation is enabled
        // - PostProcessBehaviour.timelineClip is set in PostProcessClip#CreatePlayable
        public TimelineClip timelineClip;
        public double timelineClipStart { get { return timelineClip.start; } }
        public double timelineClipEnd { get { return timelineClip.end; } }
        public double timelineClipDuration { get { return timelineClipEnd - timelineClipStart; } }

        public override void OnPlayableDestroy(Playable playable)
        {
            if (postProcessVolume != null)
            {
                GameObject go = postProcessVolume.gameObject;
                RuntimeUtilities.DestroyVolume(postProcessVolume, false);
                RuntimeUtilities.Destroy(go);
            }
        }
    }
}
