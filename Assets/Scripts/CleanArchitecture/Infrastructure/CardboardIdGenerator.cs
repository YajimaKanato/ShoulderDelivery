using ShoulderDelivery.Entity;
using ShoulderDelivery.UseCase;

namespace ShoulderDelivery.Infrastructure
{
    /// <summary>段ボールのIDを生成するクラス</summary>
    public class CardboardIdGenerator : ICardboardIdGenerator
    {
        int _nextId;

        public CardboardId GenerateId()
        {
            _nextId++;
            return new CardboardId(_nextId);
        }
    }
}
