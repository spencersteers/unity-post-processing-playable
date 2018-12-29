using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering.PostProcessing;

namespace SpencerSteers.PostProcessingPlayable
{
    [TrackColor(0.98f, 0.27f, 0.42f)]
    [TrackClipType(typeof(PostProcessClip))]
    public class PostProcessTrack : TrackAsset
    {
        public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
        {
            var scriptPlayable = ScriptPlayable<PostProcessMixerBehaviour>.Create(graph, inputCount);
            foreach (var c in GetClips())
            {
                PostProcessClip clip = (PostProcessClip)c.asset;
                if (clip.postProcessProfile == null)
                {
                    c.displayName = "No Profile Assigned!";
                }
                else
                {
                    c.displayName = clip.postProcessProfile.name;
                }

                // Get TimelineClip to Behavior so that real start / end times can be used
                if (clip != null)
                    clip.timelineClip = c;
            }

            return scriptPlayable;
        }
    }
}