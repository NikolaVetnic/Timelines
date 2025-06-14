﻿using System.Text.Json.Serialization;
using BuildingBlocks.Domain.Nodes.Node.Dtos;

namespace BuildingBlocks.Domain.Reminders.Reminder.Dtos;

public class ReminderDto(
    string? id,
    string title,
    string description,
    DateTime notifyAt,
    int priority,
    string colorHex,
    NodeBaseDto node) : ReminderBaseDto(id, title, description, notifyAt, priority, colorHex)
{
    [JsonPropertyName("node")] public NodeBaseDto Node { get; set; } = node;
}
