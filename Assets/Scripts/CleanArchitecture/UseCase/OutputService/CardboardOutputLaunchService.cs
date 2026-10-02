namespace ShoulderDelivery.UseCase
{
    /// <summary>段ボールを投げる時の情報を生成する機能を持つクラス</summary>
    internal static class CardboardOutputLaunchService
    {
        /// <summary>
        /// 段ボールを投げる時の情報を持つDTOを生成するメソッド
        /// </summary>
        /// <param name="input">段ボールを投げる時の情報</param>
        /// <returns>段ボールを投げる時の情報</returns>
        internal static CardboardLaunchOutput Launch(CardboardLaunchInput input)
        {
            return new CardboardLaunchOutput(input.Position, input.Direction, input.InitialSpeed);
        }
    }
}
