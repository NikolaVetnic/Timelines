﻿namespace Files.Application.Dtos;

public record FileAssetDto(
    string Id,
    string Name,
    float Size,
    string Type,
    string Owner,
    string Description,
    List<string> SharedWith);