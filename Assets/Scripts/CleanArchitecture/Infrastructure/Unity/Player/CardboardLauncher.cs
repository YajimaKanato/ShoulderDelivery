using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;
using System.Collections.Generic;
using UnityEngine;

namespace ShoulderDelivery.Infrastructure
{
    public class CardboardLauncher : MonoBehaviour, ICardboardLauncher
    {
        [SerializeField] CardboardView[] _cardboardViews;


        public void LaunchCardboard(CardboardId id, ThrowCardboardOutput output)
        {

        }
    }
}
