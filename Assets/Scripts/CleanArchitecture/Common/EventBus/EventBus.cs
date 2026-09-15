using System;
using System.Collections.Generic;
/// <summary>イベントが集約するクラス</summary>
public static class EventBus
{
    /// <summary>イベントトークンと実行するアクションの対応表</summary>
    static readonly Dictionary<Type, List<Delegate>> _subscribers = new();
    /// <summary>イベント購読者と削除するアクションの対応表</summary>
    static readonly Dictionary<object, List<IDisposable>> _ownerMap = new();

    /// <summary>
    /// イベントを発火するメソッド
    /// </summary>
    /// <typeparam name="TEvent">発火するイベントに対応するトークンのデータ型</typeparam>
    /// <param name="e">発火するイベントに対応するトークン</param>
    public static void Publish<TEvent>(TEvent e) where TEvent : struct, IToken
    {
        var type = typeof(TEvent);
        if (!_subscribers.TryGetValue(type, out var list)) return;

        var handlers = list.ToArray();
        foreach (var handler in handlers)
        {
            ((Action<TEvent>)handler)(e);
        }
    }

    /// <summary>
    /// イベントを購読するメソッド
    /// </summary>
    /// <typeparam name="TEvent">購読するイベントのに対応するトークンのデータ型</typeparam>
    /// <param name="owner">イベントの購読者</param>
    /// <param name="handler">登録するアクション</param>
    /// <returns>アクション削除処理</returns>
    public static IDisposable Subscribe<TEvent>(object owner, Action<TEvent> handler) where TEvent : struct, IToken
    {
        if (owner == null) return null;
        if (handler == null) return null;
        var type = typeof(TEvent);
        if (!_subscribers.TryGetValue(type, out var list))
        {
            list = new List<Delegate>();
            _subscribers[type] = list;
        }
        if (!_ownerMap.TryGetValue(owner, out var map))
        {
            map = new List<IDisposable>();
            _ownerMap[owner] = map;
        }

        list.Add(handler);

        Subscription subscription = null;
        subscription = new Subscription(() =>
        {
            if (_subscribers.TryGetValue(type, out var l))
            {
                l.Remove(handler);
                if (l.Count == 0)
                    _subscribers.Remove(type);
            }
            if (_ownerMap.TryGetValue(owner, out var map))
            {
                map.Remove(subscription);
                if (map.Count == 0)
                    _ownerMap.Remove(owner);
            }
        });

        map.Add(subscription);

        return subscription;
    }

    /// <summary>
    /// イベントの購読を解除するメソッド
    /// </summary>
    /// <param name="owner">イベント購読の解除者</param>
    public static void Unsubscribe(object owner)
    {
        if (owner == null) return;
        if (!_ownerMap.TryGetValue(owner, out var map)) return;
        var snapshot = new List<IDisposable>(map);
        foreach (var subscription in snapshot)
        {
            subscription.Dispose();
        }
        _ownerMap.Remove(owner);
    }

    /// <summary>
    /// すべてのイベント購読を解除するメソッド
    /// </summary>
    public static void Clear()
    {
        _subscribers.Clear();
        _ownerMap.Clear();
    }

    /// <summary>
    /// 現在のイベント購読者の数を取得するメソッド
    /// </summary>
    /// <typeparam name="TEvent">購読者数を取得するイベントに対応するトークンのデータ型</typeparam>
    /// <returns>現在のイベント購読者の数</returns>
    public static int GetSubscriberCount<TEvent>() where TEvent : struct, IToken
    {
        var type = typeof(TEvent);
        if (_subscribers.TryGetValue(type, out var list))
            return list.Count;
        return 0;
    }

    /// <summary>
    /// イベント購読を解除するクラス
    /// </summary>
    private sealed class Subscription : IDisposable
    {
        /// <summary>購読を解除するアクション</summary>
        readonly Action _dispose;
        bool _disposed;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dispose">購読解除をするアクション</param>
        public Subscription(Action dispose)
        {
            _dispose = dispose ?? throw new ArgumentNullException(nameof(dispose));
        }

        /// <summary>
        /// 購読を解除するメソッド
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _dispose();
        }
    }
}