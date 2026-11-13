using ShoulderDelivery.Common;
using System;
using System.Linq;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class StageHourManager : MonoBehaviour
    {
        [SerializeField] InGameHudView _hudView;
        [SerializeField] Guard[] _guards;
        [SerializeField] StageHourPair[] _pairs;
        [SerializeField] Material _defaultSkybox;
        [SerializeField] Color _defaultColor;

        private void Start()
        {
            _hudView.OnStart += ChangeHour;
        }

        private void OnDestroy()
        {
            _hudView.OnStart -= ChangeHour;
        }

        void ChangeHour(StageHour stageHour)
        {
            var hour = _pairs.FirstOrDefault(s => s.StageHour == stageHour);
            var material = hour?.Skybox ?? _defaultSkybox;
            var color = hour?.GuardColor ?? _defaultColor;
            RenderSettings.skybox = material;
            foreach (var guard in _guards)
            {
                guard?.SetColor(color);
            }
        }

        [Serializable]
        class StageHourPair
        {
            [SerializeField] StageHour _stageHour;
            [SerializeField] Material _skyBox;
            [SerializeField] Color _guardColor;

            public StageHour StageHour => _stageHour;
            public Material Skybox => _skyBox;
            public Color GuardColor => _guardColor;
        }
    }
}
