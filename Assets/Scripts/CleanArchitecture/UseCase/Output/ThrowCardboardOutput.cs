using ShoulderDelivery.Entity;
using ShoulderDerivery.Common;

namespace ShoulderDelivery.UseCase
{
    public readonly struct ThrowCardboardOutput
    {
        public readonly CardboardId CardboardId;
        public readonly CardboardWeight CardboardWeight;
        public readonly ThrowContext Context;
        public readonly CardboardLaunch Launch;

        public ThrowCardboardOutput(CardboardId cardboardId
            , CardboardWeight cardboardWeight
            , ThrowContext context
            , CardboardLaunch launch)
        {
            CardboardId = cardboardId;
            CardboardWeight = cardboardWeight;
            Context = context;
            Launch = launch;
        }

        /// <summary>
        /// 投擲を実行するときに必要な情報を生成するメソッド
        /// </summary>
        /// <param name="cardboardId">段ボールのID</param>
        /// <param name="cardboardWeight">段ボールの重さ</param>
        /// <param name="input">投擲実行に必要な情報</param>
        /// <returns>投擲実行に必要な情報</returns>
        internal static ThrowCardboardOutput Output(CardboardId cardboardId
            , CardboardWeight cardboardWeight
            , ThrowCardboardInput input)
        {
            return new ThrowCardboardOutput(cardboardId, cardboardWeight, input.Context, input.Launch);
        }
    }
}
