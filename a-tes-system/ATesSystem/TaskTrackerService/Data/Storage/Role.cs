﻿using System.Text.Json.Serialization;

namespace TaskTrackerService.Data.Storage;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Role
{
    Manager,
    Admin,
    Worker
}
