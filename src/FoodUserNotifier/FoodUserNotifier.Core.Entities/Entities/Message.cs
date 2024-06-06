﻿namespace FoodUserNotifier.Core.Entities;

public sealed class Message
{
    public IEnumerable<Recepient> Recepients { get; set; }
    public string MessageText { get; set; }
    public int[] AttachmentIds { get; set; }
}
