﻿using Content.Client.Message;
using Content.Client.Stylesheets;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.CustomControls;
using Robust.Client.UserInterface.XAML;
using Robust.Shared.Utility;

namespace Content.Client.MassMedia.Ui;

[GenerateTypedNameReferences]
public sealed partial class ArticleEditorPanel : Control
{
    public event Action? PublishButtonPressed;

    private bool _preview = false;

    public ArticleEditorPanel()
    {
        RobustXamlLoader.Load(this);

        ButtonPublish.StyleClasses.Add(StyleBase.ButtonOpenLeft);
        ButtonPublish.StyleClasses.Add(StyleNano.StyleClassButtonColorGreen);

        ContentField.GetChild(0).Margin = new Thickness(9, 3);
        // Customize scrollbar width and margin. This is not possible in xaml
        var scrollbar = ContentField.GetChild(1);
        scrollbar.SetWidth = 6f;
        scrollbar.Margin = new Thickness(9, 0, 2 , 0);

        RichTextInfoLabel.TooltipSupplier = sender =>
        {
            var label = new RichTextLabel();
            label.SetMarkup(Loc.GetString("news-write-ui-richtext-tooltip"));

            var tooltip = new Tooltip();
            tooltip.GetChild(0).Children.Clear();
            tooltip.GetChild(0).Children.Add(label);

            return tooltip;
        };

        ButtonPreview.OnPressed += OnPreview;
        ButtonCancel.OnPressed += OnCancel;
        ButtonPublish.OnPressed += OnPublish;
    }

    private void OnPreview(BaseButton.ButtonEventArgs eventArgs)
    {
        _preview = !_preview;

        TextEditPanel.Visible = !_preview;
        PreviewPanel.Visible = _preview;
        PreviewLabel.SetMarkup(Rope.Collapse(ContentField.TextRope));
    }

    private void OnCancel(BaseButton.ButtonEventArgs eventArgs)
    {
        Reset();
        Visible = false;
    }

    private void OnPublish(BaseButton.ButtonEventArgs eventArgs)
    {
        PublishButtonPressed?.Invoke();
        Reset();
        Visible = false;
    }

    private void Reset()
    {
        _preview = false;
        TextEditPanel.Visible = true;
        PreviewPanel.Visible = false;
        PreviewLabel.SetMarkup("");
        TitleField.Text = "";
        ContentField.TextRope = Rope.Leaf.Empty;
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        if (!disposing)
            return;

        ButtonPreview.OnPressed -= OnPreview;
        ButtonCancel.OnPressed -= OnCancel;
        ButtonPublish.OnPressed -= OnPublish;
    }
}
