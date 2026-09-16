using ShoulderDelivery.UseCase;
using UnityEngine;

namespace ShoulderDelivery.Adapter
{
    public class GamePresenter : MonoBehaviour, IGameOutputPort
    {
        public void ChangeControllerEnable(bool enable)
        {
            throw new System.NotImplementedException();
        }

        public void ShowDeliveryResult(DeliveryResultOutput deliveryResult)
        {
            throw new System.NotImplementedException();
        }

        public void ShowHud(GameHudOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void ShowResult(GameResultOutput output)
        {
            throw new System.NotImplementedException();
        }

        public void ShowThrowCardboardOutcome(ThrowCardboardOutput output)
        {
            throw new System.NotImplementedException();
        }
    }
}
