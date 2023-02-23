using Avalonia;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using NP.Avalonia.UniDock;
using NP.Avalonia.Visuals.Behaviors;
using NP.Avalonia.Visuals.Controls;
using NP.Gidon.Messages;
using NP.Utilities;
using System;
using System.Reactive.Linq;

namespace NP.Avalonia.Gidon;
public class DockItemImplantedWindowHost : Decorator
{
    #region ProcessInitInfo Styled Avalonia Property
    public MultiPlatformProcessInitInfoWithClient ProcessInitInfo
    {
        get { return GetValue(ProcessInitInfoProperty); }
        set { SetValue(ProcessInitInfoProperty, value); }
    }

    public static readonly StyledProperty<MultiPlatformProcessInitInfoWithClient> ProcessInitInfoProperty =
        AvaloniaProperty.Register<DockItemImplantedWindowHost, MultiPlatformProcessInitInfoWithClient>
        (
            nameof(ProcessInitInfo)
        );
    #endregion ProcessInitInfo Styled Avalonia Property

    #region ImplantedWindowHandle Styled Avalonia Property
    public IntPtr ImplantedWindowHandle
    {
        get { return GetValue(ImplantedWindowHandleProperty); }
        set { SetValue(ImplantedWindowHandleProperty, value); }
    }

    public static readonly StyledProperty<IntPtr> ImplantedWindowHandleProperty =
        AvaloniaProperty.Register<DockItemImplantedWindowHost, IntPtr>
        (
            nameof(ImplantedWindowHandle),
            IntPtr.Zero
        );
    #endregion ImplantedWindowHandle Styled Avalonia Property

    private DockItem? _parentDockItem;
    public DockItem? ParentDockItem 
    {
        get => _parentDockItem;
        set
        {
            if (_parentDockItem == value) 
                return;

            _parentDockItem = value;

            if (_parentDockItem != null)
            {
                _parentDockItem.DockItemDestroyedEvent += _parentDockItem_DockItemDestroyedEvent;
            }
        }
    }

    private void _parentDockItem_DockItemDestroyedEvent(DockItem dockItem)
    {
        DestroyImplantedWindow();
    }

    #region ParentWindow Property
    private Window? _parentWindow;
    public Window? ParentWindow
    {
        get
        {
            return this._parentWindow;
        }
        private set
        {
            if (this._parentWindow == value)
            {
                return;
            }

            if (_parentWindow != null)
            {
                _parentWindow.Closed -= OnParentWindowClosed;
            }

            this._parentWindow = value;


            if (_parentWindow != null)
            {
                _parentWindow.Closed += OnParentWindowClosed;
            }
        }
    }
    #endregion

    protected virtual void OnParentWindowClosed(object? sender, EventArgs e)
    {
        //DestroyImplantedWindow();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        ParentWindow = (Window)e.Root;

        DockContentPresenter dockContentPresenter = (DockContentPresenter) e.Parent;

        ParentDockItem = dockContentPresenter.OwningDockItem;

        base.OnAttachedToVisualTree(e);
    }

    public DockItemImplantedWindowHost()
    {
        this.GetObservable(ImplantedWindowHandleProperty).Subscribe(OnImplantedWindowHandleChanged);

        this.GetObservable(ProcessInitInfoProperty).Subscribe(OnProcessInitInfoChanged);
    }

    private void OnProcessInitInfoChanged(MultiPlatformProcessInitInfoWithClient? processInitInfo)
    {
        if (processInitInfo == null)
        {
            return;
        }

        var uniqueWindowHostId = processInitInfo.UniqueWindowHostId;

        if (uniqueWindowHostId == null)
        {
            "uniqueWindowHostId should not be null".ThrowProgError();
        }

        this.OnMultiPlatformProcInitInfoChangedWithExtraParams(processInitInfo, new string[] { uniqueWindowHostId! });

        processInitInfo
            .TheRelayClient!
            .ObserveTopicStream<WindowInfo>(Topic.WindowInfoTopic)
            .Where(winInfo => winInfo.UniqueWindowHostId == uniqueWindowHostId)
            .Subscribe(winInfo => this.ImplantedWindowHandle = (nint) winInfo.WindowHandle);
    }

    private void OnImplantedWindowHandleChanged(IntPtr implantedWindowHandle)
    {
        if (implantedWindowHandle != IntPtr.Zero)
        {
            this.Child = new ImplantedWindowHost(implantedWindowHandle);
        }
        else
        {
            this.Child = null;
        }
    }

    protected virtual void DestroyImplantedWindow()
    {
        if (ParentWindow != null)
        {
            this.DestroyProcess();
        }

        ParentWindow = null;
    }
}
