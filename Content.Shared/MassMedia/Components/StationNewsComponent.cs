﻿using Content.Shared.MassMedia.Systems;

namespace Content.Shared.MassMedia.Components;

[RegisterComponent]
public sealed partial class StationNewsComponent : Component
{
    [DataField("articles")]
    public List<NewsArticle> Articles = new();
}
