using ShoulderDelivery.Entity;

namespace ShoulderDelivery.UseCase
{
    /// <summary>投擲結果のDTOを生成する役割を持つクラス</summary>
    public static class ThrowCardboardOutputService
    {
        /// <summary>
        /// 投擲失敗を生成する
        /// </summary>
        /// <returns>投擲失敗DTO</returns>
        public static ThrowCardboardOutput Rejected()
        {
            return new ThrowCardboardOutput(false, null);
        }

        /// <summary>
        /// 投擲成功を生成する
        /// </summary>
        /// <param name="id">投擲した段ボールのID</param>
        /// <returns>投擲成功DTO</returns>
        public static ThrowCardboardOutput Accepted(CardboardId id)
        {
            return new ThrowCardboardOutput(true, id);
        }
    }
}
