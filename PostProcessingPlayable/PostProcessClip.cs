using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering.PostProcessing;

namespace SpencerSteers.PostProcessingPlayable
{
    [Serializable]
    public class PostProcessClip : PlayableAsset, ITimelineClipAsset
    {
        [HideInInspector]
        public PostProcessBehaviour template = new PostProcessBehaviour();

        [HideInInspector]
        public TimelineClip timelineClip;

        public AnimationCurve weightCurve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
        public PostProcessProfile postProcessProfile;
        public bool debug = false;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Extrapolation | ClipCaps.Blending; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<PostProcessBehaviour>.Create(graph, template);
            PostProcessBehaviour clone = playable.GetBehaviour();
            clone.weightCurve = weightCurve;

            int layer = LayerMask.NameToLayer("Post Process Volume");
            if (postProcessProfile != null)
            {
                clone.postProcessProfile = postProcessProfile;
                PostProcessVolume volume = PostProcessManager.instance.QuickVolume(layer, 1, clone.postProcessProfile.settings.ToArray());

                volume.weight = 0;
                volume.priority = 1;
                volume.isGlobal = true;
                volume.profile = clone.postProcessProfile;

                if (debug)
                {
                    clone.isDebug = true;
                    volume.gameObject.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
                    volume.gameObject.name = "PostProcessClip.CreatePlayable: QuickVolume [Profile " + postProcessProfile.name + "]";
                }

                clone.postProcessVolume = volume;

                // Get TimelineClip to Behavior so that real start / end times can be used when extrapolation is enabled
                // - PostProcessClip.timelineClip is first set in PostProcessTrack#CreateTrackMixer
                clone.timelineClip = this.timelineClip;
            }

            return playable;
        }
    }
}