using System.Linq;

namespace ShoulderDelivery.Entity
{
    /// <summary>連続配達成功回数を管理するクラス</summary>
    public class DeliveryCombo
    {
        int[] _deliveryCombo;

        /// <summary>現在までの連続配達成功回数</summary>
        public int Combo { get; private set; }
        /// <summary>連続配達成功回数に応じたボーナススコア</summary>
        public int ComboScore => Combo < _deliveryCombo.Length ? _deliveryCombo[Combo] : _deliveryCombo.Last();

        public DeliveryCombo(int[] deliveryCombo, int combo)
        {
            _deliveryCombo = deliveryCombo;
            Combo = combo;
        }

        /// <summary>
        /// 配達結果をもとに連続配達成功回数を更新
        /// </summary>
        /// <param name="outcome">配達結果</param>
        public void Throw(ThrowOutcome outcome)
        {
            if (outcome == ThrowOutcome.Delivered)
                Combo++;
            else
                Combo = 0;
        }
    }
}
