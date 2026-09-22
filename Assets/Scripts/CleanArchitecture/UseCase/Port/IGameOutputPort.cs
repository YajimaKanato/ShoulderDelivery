namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの状態を出力する機能を持つインターフェース</summary>
    public interface IGameOutputPort
    {
        /// <summary>ゲームの現在の状況を出力するメソッド</summary>
        /// <param name="output">ゲームの現在の状況</param>
        void ShowHud(StartGameOutput output);

        /// <summary>ゲームの現在の状況を出力するメソッド</summary>
        /// <param name="output">ゲームの現在の状況</param>
        void ShowHud(GameHudOutput output);

        /// <summary>ゲームの結果を出力するメソッド</summary>
        /// <param name="output">ゲームの結果</param>
        void ShowGameClear(GameClearOutput output);

        /// <summary>ゲームの結果を出力するメソッド</summary>
        /// <param name="output">ゲームの結果</param>
        void ShowGameFailed(GameFailedOutput output);

        /// <summary>コントローラーの有効無効を切り替えるメソッド</summary>
        /// <param name="enable">コントローラーの有効無効</param>
        void ChangeControllerEnable(bool enable);

        /// <summary>段ボールを投擲した情報を出力するメソッド</summary>
        /// <param name="output">投擲結果</param>
        void ShowThrowCardboardAccepted(ThrowCardboardAcceptOutput output);

        /// <summary>段ボールを投擲した情報を出力するメソッド</summary>
        /// <param name="output">投擲結果</param>
        void ShowThrowCardboardRejected(ThrowCardboardRejectedOutput output);

        /// <summary>配達結果を出力するメソッド</summary>
        /// <param name="deliveryResult">配達結果</param>
        void ShowDeliverySucceeded(DeliverySuccessOutput deliveryResult);

        /// <summary>配達結果を出力するメソッド</summary>
        /// <param name="deliveryResult">配達結果</param>
        void ShowDeliveryFailed(DeliveryFailedOutput deliveryResult);
    }
}
