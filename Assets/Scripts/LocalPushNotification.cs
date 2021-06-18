#if UNITY_ANDROID
using Unity.Notifications.Android;
#endif
#if UNITY_IOS
using Unity.Notifications.iOS;
#endif
using System;

/// <summary>
/// ローカルプッシュ通知送信クラス
/// </summary>
public static class LocalPushNotification
{
    /// <summary>
    /// Androidで使用するプッシュ通知用のチャンネルを登録する。
    /// </summary>
    public static void RegisterChannel(string cannelId, string title, string description)
    {
#if UNITY_ANDROID
        // チャンネルの登録
        var c = new AndroidNotificationChannel()
        {
            Id = cannelId,
            Name = title,
            Importance = Importance.High,
            Description = description,
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
#endif
    }

    /// <summary>
    /// 通知をすべてクリアーします。
    /// </summary>
    public static void AllClear()
    {
#if UNITY_ANDROID
        // Androidの通知をすべて削除します。
        AndroidNotificationCenter.CancelAllScheduledNotifications();
        AndroidNotificationCenter.CancelAllNotifications();
#endif
#if UNITY_IOS
        // iOSの通知をすべて削除します。
        iOSNotificationCenter.RemoveAllScheduledNotifications();
        iOSNotificationCenter.RemoveAllDeliveredNotifications();
        // バッジを消します。
        iOSNotificationCenter.ApplicationBadge = 0;
#endif
    }

    /// <summary>
    /// プッシュ通知を登録します。
    /// </summary>
    /// <param name="title">通知のタイトル</param>
    /// <param name="message">通知メッセージ</param>
    /// <param name="badgeCount">表示するバッジ数</param>
    /// <param name="elapsedTime">何秒後に表示させるか？</param>
    /// <param name="channelId">Androidで使用するチャンネル</param>
    public static void AddSchedule(string title, string message, int badgeCount, int elapsedTime, string channelId)
    {
        AllClear();

#if UNITY_ANDROID
        SetAndroidNotification(title, message, badgeCount, elapsedTime, channelId);
#endif
#if UNITY_IOS
        SetIOSNotification(title, message, badgeCount, elapsedTime);
#endif
    }

#if UNITY_IOS
    /// <summary>
    /// 通知を登録します。(iOS)
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="badgeCount"></param>
    /// <param name="elapsedTime"></param>
    static private void SetIOSNotification(string title, string message, int badgeCount, int elapsedTime)
    {
        // 通知を作成します。
        iOSNotificationCenter.ScheduleNotification(new iOSNotification()
        {
            // ※プッシュ通知を個別に取り消しなどをする場合はこのIdentifierを使用します。
            Identifier = $"_notification_{badgeCount}",
            Title = title,
            Body = message,
            ShowInForeground = false,
            Badge = badgeCount,
            Trigger = new iOSNotificationTimeIntervalTrigger()
            {
                TimeInterval = new TimeSpan(0, 0, elapsedTime),
                Repeats = false
            }
        });
    }
#endif

#if UNITY_ANDROID
    /// <summary>
    /// 通知を登録する。(Android)
    /// </summary>
    /// <param name="title"></param>
    /// <param name="message"></param>
    /// <param name="badgeCount"></param>
    /// <param name="elapsedTime"></param>
    /// <param name="cannelId"></param>
    static private void SetAndroidNotification(string title, string message, int badgeCount, int elapsedTime, string cannelId)
    {
        // 通知を作成します。
        var notification = new AndroidNotification
        {
            Title = title,
            Text = message,
            Number = badgeCount,
            // ※ここでAndroidのアイコンを設定します。
            SmallIcon = "icon_small",
            LargeIcon = "icon_large",
            FireTime = DateTime.Now.AddSeconds(elapsedTime)
        };

        // 通知を送信します。
        AndroidNotificationCenter.SendNotification(notification, cannelId);
        // ※以下のコードを使うことで個別にプッシュ通知の制御ができます。
        //var identifier = AndroidNotificationCenter.SendNotification(notification, cannelId);
        //UnityEngine.Debug.Log($"TownSoftPush: プッシュ通知の登録完了 -> {DateTime.Now.AddSeconds(elapsedTime)}");

        //if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        //{
        //    // Replace the currently scheduled notification with a new notification.
        //    UnityEngine.Debug.Log("プッシュ通知はすでに登録済み");
        //}
        //else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Delivered)
        //{
        //    //Remove the notification from the status bar
        //    //AndroidNotificationCenter.CancelNotification(identifier);
        //    UnityEngine.Debug.Log("プッシュ通知はすでに通知済み");
        //}
        //else if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Unknown)
        //{
        //    //AndroidNotificationCenter.SendNotification(newNotification, "channel_id");
        //    UnityEngine.Debug.Log("プッシュ通知は不明な状況");
        //}
    }
#endif
}