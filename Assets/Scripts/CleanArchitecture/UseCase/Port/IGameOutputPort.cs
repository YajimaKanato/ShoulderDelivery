namespace ShoulderDelivery.UseCase
{
    /// <summary>ゲームの状態を出力する機能を持つインターフェース</summary>
    public interface IGameOutputPort
    {
        /// <summary>ゲームの現在の状況を出力するメソッド</summary>
        /// <param name="output">ゲームの現在の状況</param>
        void ShowHud(GameHudOutput output);

        /// <summary>ゲームの結果を出力するメソッド</summary>
        /// <param name="output">ゲームの結果</param>
        void ShowResult(GameResultOutput output);
    }
}
