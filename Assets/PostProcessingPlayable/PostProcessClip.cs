using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.Rendering.PostProcessing;

namespace PostProcessingPlayable
{
    [Serializable]
    public class PostProcessClip : PlayableAsset, ITimelineClipAsset
    {
        [HideInInspector]
        public PostProcessBehaviour template = new PostProcessBehaviour();

        public PostProcessProfile postProcessProfile;

        public ClipCaps clipCaps
        {
            get { return ClipCaps.Extrapolation | ClipCaps.Blending; }
        }

        public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<PostProcessBehaviour>.Create(graph, template);
            PostProcessBehaviour clone = playable.GetBehaviour();

            int layer = LayerMask.NameToLayer("PostProcessing");
            if (postProcessProfile != null)
            {
                PostProcessVolume volume = PostProcessManager.instance.QuickVolume(
                    layer, 
                    1, 
                    postProcessProfile.settings.ToArray()
                );   
                volume.weight = 0;
                volume.priority = 1;
                volume.isGlobal = true;
                volume.profile = postProcessProfile;
                volume.gameObject.hideFlags = HideFlags.DontSave | HideFlags.NotEditable;
                volume.gameObject.name = $"PostProcessClip.CreatePlayable: QuickVolume [Profile {postProcessProfile.name}]";
                
                clone.postProcessVolume = volume;
            }
            
            return playable;
        }
    }
}