using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.ComponentModel;

namespace GUIInjectionTest
{
    public class ProcessGUIPresenter
    {
        private Process _model;
        private int _processId;
        private string _processName;
        private bool _hasExited;

        public ProcessGUIPresenter(Process model)
        {
            _model = model;
            RefreshFromModel();
        }

        public void RefreshFromModel()
        {
            try
            {
                _processId = _model.Id;
                _processName = _model.ProcessName;
                _hasExited = _model.HasExited;
            }
            catch (Exception ex)
            {
                _processId = -1;
                _processName = ex.Message;
            }
        }


        public int Id
        {
            get 
            {
                return _processId;
            }
        }

        public string ProcessName
        {
            get
            {
                return _processName;
            }
        }

        public bool HasExited
        {
            get
            {
                return _hasExited;
            }
        }


        /*

        // Summary:
        //     Gets the base priority of the associated process.
        //
        // Returns:
        //     The base priority, which is computed from the System.Diagnostics.Process.PriorityClass
        //     of the associated process.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     the System.Diagnostics.ProcessStartInfo.UseShellExecute property to false
        //     to access this property on Windows 98 and Windows Me.
        //
        //   System.InvalidOperationException:
        //     The process has exited.-or- The process has not started, so there is no process
        //     ID.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessBasePriority")]
        public int BasePriority { get; }
        //
        // Summary:
        //     Gets or sets whether the System.Diagnostics.Process.Exited event should be
        //     raised when the process terminates.
        //
        // Returns:
        //     true if the System.Diagnostics.Process.Exited event should be raised when
        //     the associated process is terminated (through either an exit or a call to
        //     System.Diagnostics.Process.Kill()); otherwise, false. The default is false.
        [MonitoringDescription("ProcessEnableRaisingEvents")]
        [Browsable(false)]
        [DefaultValue(false)]
        public bool EnableRaisingEvents { get; set; }
        //
        // Summary:
        //     Gets the value that the associated process specified when it terminated.
        //
        // Returns:
        //     The code that the associated process specified when it terminated.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The process has not exited.-or- The process System.Diagnostics.Process.Handle
        //     is not valid.
        //
        //   System.NotSupportedException:
        //     You are trying to access the System.Diagnostics.Process.ExitCode property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        [MonitoringDescription("ProcessExitCode")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ExitCode { get; }
        //
        // Summary:
        //     Gets the time that the associated process exited.
        //
        // Returns:
        //     A System.DateTime that indicates when the associated process was terminated.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        //
        //   System.NotSupportedException:
        //     You are trying to access the System.Diagnostics.Process.ExitTime property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        [MonitoringDescription("ProcessExitTime")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime ExitTime { get; }
        //
        // Summary:
        //     Gets the native handle of the associated process.
        //
        // Returns:
        //     The handle that the operating system assigned to the associated process when
        //     the process was started. The system uses this handle to keep track of process
        //     attributes.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The process has not been started or has exited. The System.Diagnostics.Process.Handle
        //     property cannot be read because there is no process associated with this
        //     System.Diagnostics.Process instance.-or- The System.Diagnostics.Process instance
        //     has been attached to a running process but you do not have the necessary
        //     permissions to get a handle with full access rights.
        //
        //   System.NotSupportedException:
        //     You are trying to access the System.Diagnostics.Process.Handle property for
        //     a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [MonitoringDescription("ProcessHandle")]
        public IntPtr Handle { get; }
        //
        // Summary:
        //     Gets the number of handles opened by the process.
        //
        // Returns:
        //     The number of operating system handles the process has opened.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     the System.Diagnostics.ProcessStartInfo.UseShellExecute property to false
        //     to access this property on Windows 98 and Windows Me.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessHandleCount")]
        public int HandleCount { get; }
        //
        // Summary:
        //     Gets a value indicating whether the associated process has been terminated.
        //
        // Returns:
        //     true if the operating system process referenced by the System.Diagnostics.Process
        //     component has terminated; otherwise, false.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     There is no process associated with the object.
        //
        //   System.ComponentModel.Win32Exception:
        //     The exit code for the process could not be retrieved.
        //
        //   System.NotSupportedException:
        //     You are trying to access the System.Diagnostics.Process.HasExited property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        [Browsable(false)]
        [MonitoringDescription("ProcessTerminated")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool HasExited { get; }
        //
        // Summary:
        //     Gets the unique identifier for the associated process.
        //
        // Returns:
        //     The system-generated unique identifier of the process that is referenced
        //     by this System.Diagnostics.Process instance.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The process's System.Diagnostics.Process.Id property has not been set.-or-
        //     There is no process associated with this System.Diagnostics.Process object.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     the System.Diagnostics.ProcessStartInfo.UseShellExecute property to false
        //     to access this property on Windows 98 and Windows Me.
        [MonitoringDescription("ProcessId")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Id { get; }
        //
        // Summary:
        //     Gets the name of the computer the associated process is running on.
        //
        // Returns:
        //     The name of the computer that the associated process is running on.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     There is no process associated with this System.Diagnostics.Process object.
        [MonitoringDescription("ProcessMachineName")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MachineName { get; }
        //
        // Summary:
        //     Gets the main module for the associated process.
        //
        // Returns:
        //     The System.Diagnostics.ProcessModule that was used to start the process.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     You are trying to access the System.Diagnostics.Process.MainModule property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     System.Diagnostics.ProcessStartInfo.UseShellExecute to false to access this
        //     property on Windows 98 and Windows Me.
        //
        //   System.InvalidOperationException:
        //     The process System.Diagnostics.Process.Id is not available.-or- The process
        //     has exited.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [MonitoringDescription("ProcessMainModule")]
        public ProcessModule MainModule { get; }
        //
        // Summary:
        //     Gets the window handle of the main window of the associated process.
        //
        // Returns:
        //     The system-generated window handle of the main window of the associated process.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Diagnostics.Process.MainWindowHandle is not defined because the
        //     process has exited.
        //
        //   System.NotSupportedException:
        //     You are trying to access the System.Diagnostics.Process.MainWindowHandle
        //     property for a process that is running on a remote computer. This property
        //     is available only for processes that are running on the local computer.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     System.Diagnostics.ProcessStartInfo.UseShellExecute to false to access this
        //     property on Windows 98 and Windows Me.
        [MonitoringDescription("ProcessMainWindowHandle")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IntPtr MainWindowHandle { get; }
        //
        // Summary:
        //     Gets the caption of the main window of the process.
        //
        // Returns:
        //     The main window title of the process.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Diagnostics.Process.MainWindowTitle property is not defined because
        //     the process has exited.
        //
        //   System.NotSupportedException:
        //     You are trying to access the System.Diagnostics.Process.MainWindowTitle property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     System.Diagnostics.ProcessStartInfo.UseShellExecute to false to access this
        //     property on Windows 98 and Windows Me.
        [MonitoringDescription("ProcessMainWindowTitle")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string MainWindowTitle { get; }
        //
        // Summary:
        //     Gets or sets the maximum allowable working set size for the associated process.
        //
        // Returns:
        //     The maximum working set size that is allowed in memory for the process, in
        //     bytes.
        //
        // Exceptions:
        //   System.ComponentModel.Win32Exception:
        //     Working set information cannot be retrieved from the associated process resource.-or-
        //     The process identifier or process handle is zero because the process has
        //     not been started.
        //
        //   System.NotSupportedException:
        //     You are trying to access the System.Diagnostics.Process.MaxWorkingSet property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        //
        //   System.InvalidOperationException:
        //     The process System.Diagnostics.Process.Id is not available.-or- The process
        //     has exited.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessMaxWorkingSet")]
        public IntPtr MaxWorkingSet { get; set; }
        //
        // Summary:
        //     Gets or sets the minimum allowable working set size for the associated process.
        //
        // Returns:
        //     The minimum working set size that is required in memory for the process,
        //     in bytes.
        //
        // Exceptions:
        //   System.ComponentModel.Win32Exception:
        //     Working set information cannot be retrieved from the associated process resource.-or-
        //     The process identifier or process handle is zero because the process has
        //     not been started.
        //
        //   System.NotSupportedException:
        //     You are trying to access the System.Diagnostics.Process.MinWorkingSet property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        //
        //   System.InvalidOperationException:
        //     The process System.Diagnostics.Process.Id is not available.-or- The process
        //     has exited.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessMinWorkingSet")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IntPtr MinWorkingSet { get; set; }
        //
        // Summary:
        //     Gets the modules that have been loaded by the associated process.
        //
        // Returns:
        //     An array of type System.Diagnostics.ProcessModule that represents the modules
        //     that have been loaded by the associated process.
        //
        // Exceptions:
        //   System.NotSupportedException:
        //     You are attempting to access the System.Diagnostics.Process.Modules property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        //
        //   System.InvalidOperationException:
        //     The process System.Diagnostics.Process.Id is not available.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     System.Diagnostics.ProcessStartInfo.UseShellExecute to false to access this
        //     property on Windows 98 and Windows Me.
        //
        //   System.ComponentModel.Win32Exception:
        //     You are attempting to access the System.Diagnostics.Process.Modules property
        //     for either the system process or the idle process. These processes do not
        //     have modules.
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessModules")]
        public ProcessModuleCollection Modules { get; }
        //
        // Summary:
        //     Gets the nonpaged system memory size allocated to this process.
        //
        // Returns:
        //     The amount of memory, in bytes, the system has allocated for the associated
        //     process that cannot be written to the virtual memory paging file.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessNonpagedSystemMemorySize")]
        [Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.NonpagedSystemMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        public int NonpagedSystemMemorySize { get; }
        //
        // Summary:
        //     Gets the amount of nonpaged system memory allocated for the associated process.
        //
        // Returns:
        //     The amount of system memory, in bytes, allocated for the associated process
        //     that cannot be written to the virtual memory paging file.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessNonpagedSystemMemorySize")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ComVisible(false)]
        public long NonpagedSystemMemorySize64 { get; }
        //
        // Summary:
        //     Gets the paged memory size.
        //
        // Returns:
        //     The amount of memory, in bytes, allocated by the associated process that
        //     can be written to the virtual memory paging file.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessPagedMemorySize")]
        [Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PagedMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PagedMemorySize { get; }
        //
        // Summary:
        //     Gets the amount of paged memory allocated for the associated process.
        //
        // Returns:
        //     The amount of memory, in bytes, allocated in the virtual memory paging file
        //     for the associated process.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ComVisible(false)]
        [MonitoringDescription("ProcessPagedMemorySize")]
        public long PagedMemorySize64 { get; }
        //
        // Summary:
        //     Gets the paged system memory size.
        //
        // Returns:
        //     The amount of memory, in bytes, the system has allocated for the associated
        //     process that can be written to the virtual memory paging file.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessPagedSystemMemorySize")]
        [Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PagedSystemMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PagedSystemMemorySize { get; }
        //
        // Summary:
        //     Gets the amount of pageable system memory allocated for the associated process.
        //
        // Returns:
        //     The amount of system memory, in bytes, allocated for the associated process
        //     that can be written to the virtual memory paging file.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessPagedSystemMemorySize")]
        [ComVisible(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public long PagedSystemMemorySize64 { get; }
        //
        // Summary:
        //     Gets the peak paged memory size.
        //
        // Returns:
        //     The maximum amount of memory, in bytes, allocated by the associated process
        //     that could be written to the virtual memory paging file.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PeakPagedMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        [MonitoringDescription("ProcessPeakPagedMemorySize")]
        public int PeakPagedMemorySize { get; }
        //
        // Summary:
        //     Gets the maximum amount of memory in the virtual memory paging file used
        //     by the associated process.
        //
        // Returns:
        //     The maximum amount of memory, in bytes, allocated in the virtual memory paging
        //     file for the associated process since it was started.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ComVisible(false)]
        [MonitoringDescription("ProcessPeakPagedMemorySize")]
        public long PeakPagedMemorySize64 { get; }
        //
        // Summary:
        //     Gets the peak virtual memory size.
        //
        // Returns:
        //     The maximum amount of virtual memory, in bytes, that the associated process
        //     has requested.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessPeakVirtualMemorySize")]
        [Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PeakVirtualMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PeakVirtualMemorySize { get; }
        //
        // Summary:
        //     Gets the maximum amount of virtual memory used by the associated process.
        //
        // Returns:
        //     The maximum amount of virtual memory, in bytes, allocated for the associated
        //     process since it was started.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessPeakVirtualMemorySize")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ComVisible(false)]
        public long PeakVirtualMemorySize64 { get; }
        //
        // Summary:
        //     Gets the peak working set size for the associated process.
        //
        // Returns:
        //     The maximum amount of physical memory that the associated process has required
        //     all at once, in bytes.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessPeakWorkingSet")]
        [Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PeakWorkingSet64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PeakWorkingSet { get; }
        //
        // Summary:
        //     Gets the maximum amount of physical memory used by the associated process.
        //
        // Returns:
        //     The maximum amount of physical memory, in bytes, allocated for the associated
        //     process since it was started.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ComVisible(false)]
        [MonitoringDescription("ProcessPeakWorkingSet")]
        public long PeakWorkingSet64 { get; }
        //
        // Summary:
        //     Gets or sets a value indicating whether the associated process priority should
        //     temporarily be boosted by the operating system when the main window has the
        //     focus.
        //
        // Returns:
        //     true if dynamic boosting of the process priority should take place for a
        //     process when it is taken out of the wait state; otherwise, false. The default
        //     is false.
        //
        // Exceptions:
        //   System.ComponentModel.Win32Exception:
        //     Priority boost information could not be retrieved from the associated process
        //     resource.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.-or- The process identifier or process handle
        //     is zero. (The process has not been started.)
        //
        //   System.NotSupportedException:
        //     You are attempting to access the System.Diagnostics.Process.PriorityBoostEnabled
        //     property for a process that is running on a remote computer. This property
        //     is available only for processes that are running on the local computer.
        //
        //   System.InvalidOperationException:
        //     The process System.Diagnostics.Process.Id is not available.
        [MonitoringDescription("ProcessPriorityBoostEnabled")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool PriorityBoostEnabled { get; set; }
        //
        // Summary:
        //     Gets or sets the overall priority category for the associated process.
        //
        // Returns:
        //     The priority category for the associated process, from which the System.Diagnostics.Process.BasePriority
        //     of the process is calculated.
        //
        // Exceptions:
        //   System.ComponentModel.Win32Exception:
        //     Process priority information could not be set or retrieved from the associated
        //     process resource.-or- The process identifier or process handle is zero. (The
        //     process has not been started.)
        //
        //   System.NotSupportedException:
        //     You are attempting to access the System.Diagnostics.Process.PriorityClass
        //     property for a process that is running on a remote computer. This property
        //     is available only for processes that are running on the local computer.
        //
        //   System.InvalidOperationException:
        //     The process System.Diagnostics.Process.Id is not available.
        //
        //   System.PlatformNotSupportedException:
        //     You have set the System.Diagnostics.Process.PriorityClass to AboveNormal
        //     or BelowNormal when using Windows 98 or Windows Millennium Edition (Windows
        //     Me). These platforms do not support those values for the priority class.
        //
        //   System.ComponentModel.InvalidEnumArgumentException:
        //     Priority class cannot be set because it does not use a valid value, as defined
        //     in the System.Diagnostics.ProcessPriorityClass enumeration.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessPriorityClass")]
        public ProcessPriorityClass PriorityClass { get; set; }
        //
        // Summary:
        //     Gets the private memory size.
        //
        // Returns:
        //     The number of bytes allocated by the associated process that cannot be shared
        //     with other processes.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessPrivateMemorySize")]
        [Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PrivateMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int PrivateMemorySize { get; }
        //
        // Summary:
        //     Gets the amount of private memory allocated for the associated process.
        //
        // Returns:
        //     The amount of memory, in bytes, allocated for the associated process that
        //     cannot be shared with other processes.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [MonitoringDescription("ProcessPrivateMemorySize")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [ComVisible(false)]
        public long PrivateMemorySize64 { get; }
        //
        // Summary:
        //     Gets the privileged processor time for this process.
        //
        // Returns:
        //     A System.TimeSpan that indicates the amount of time that the process has
        //     spent running code inside the operating system core.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        //
        //   System.NotSupportedException:
        //     You are attempting to access the System.Diagnostics.Process.PrivilegedProcessorTime
        //     property for a process that is running on a remote computer. This property
        //     is available only for processes that are running on the local computer.
        [MonitoringDescription("ProcessPrivilegedProcessorTime")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeSpan PrivilegedProcessorTime { get; }
        //
        // Summary:
        //     Gets the name of the process.
        //
        // Returns:
        //     The name that the system uses to identify the process to the user.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The process does not have an identifier, or no process is associated with
        //     the System.Diagnostics.Process.-or- The associated process has exited.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     System.Diagnostics.ProcessStartInfo.UseShellExecute to false to access this
        //     property on Windows 98 and Windows Me.
        //
        //   System.NotSupportedException:
        //     The process is not on this computer.
        [MonitoringDescription("ProcessProcessName")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ProcessName { get; }
        //
        // Summary:
        //     Gets or sets the processors on which the threads in this process can be scheduled
        //     to run.
        //
        // Returns:
        //     A bitmask representing the processors that the threads in the associated
        //     process can run on. The default depends on the number of processors on the
        //     computer. The default value is 2 n -1, where n is the number of processors.
        //
        // Exceptions:
        //   System.ComponentModel.Win32Exception:
        //     System.Diagnostics.Process.ProcessorAffinity information could not be set
        //     or retrieved from the associated process resource.-or- The process identifier
        //     or process handle is zero. (The process has not been started.)
        //
        //   System.NotSupportedException:
        //     You are attempting to access the System.Diagnostics.Process.ProcessorAffinity
        //     property for a process that is running on a remote computer. This property
        //     is available only for processes that are running on the local computer.
        //
        //   System.InvalidOperationException:
        //     The process System.Diagnostics.Process.Id was not available.-or- The process
        //     has exited.
        [MonitoringDescription("ProcessProcessorAffinity")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IntPtr ProcessorAffinity { get; set; }
        //
        // Summary:
        //     Gets a value indicating whether the user interface of the process is responding.
        //
        // Returns:
        //     true if the user interface of the associated process is responding to the
        //     system; otherwise, false.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     System.Diagnostics.ProcessStartInfo.UseShellExecute to false to access this
        //     property on Windows 98 and Windows Me.
        //
        //   System.InvalidOperationException:
        //     There is no process associated with this System.Diagnostics.Process object.
        //
        //   System.NotSupportedException:
        //     You are attempting to access the System.Diagnostics.Process.Responding property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessResponding")]
        public bool Responding { get; }
        //
        // Summary:
        //     Gets the Terminal Services session identifier for the associated process.
        //
        // Returns:
        //     The Terminal Services session identifier for the associated process.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     There is no process associated with this session identifier.-or-The associated
        //     process is not on this machine.
        //
        //   System.PlatformNotSupportedException:
        //     The System.Diagnostics.Process.SessionId property is not supported on Windows 98.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessSessionId")]
        public int SessionId { get; }
        //
        // Summary:
        //     Gets a stream used to read the error output of the application.
        //
        // Returns:
        //     A System.IO.StreamReader that can be used to read the standard error stream
        //     of the application.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Diagnostics.Process.StandardError stream has not been defined
        //     for redirection; ensure System.Diagnostics.ProcessStartInfo.RedirectStandardError
        //     is set to true and System.Diagnostics.ProcessStartInfo.UseShellExecute is
        //     set to false.- or - The System.Diagnostics.Process.StandardError stream has
        //     been opened for asynchronous read operations with System.Diagnostics.Process.BeginErrorReadLine().
        [MonitoringDescription("ProcessStandardError")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StreamReader StandardError { get; }
        //
        // Summary:
        //     Gets a stream used to write the input of the application.
        //
        // Returns:
        //     A System.IO.StreamWriter that can be used to write the standard input stream
        //     of the application.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Diagnostics.Process.StandardInput stream has not been defined
        //     because System.Diagnostics.ProcessStartInfo.RedirectStandardInput is set
        //     to false.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        [MonitoringDescription("ProcessStandardInput")]
        public StreamWriter StandardInput { get; }
        //
        // Summary:
        //     Gets a stream used to read the output of the application.
        //
        // Returns:
        //     A System.IO.StreamReader that can be used to read the standard output stream
        //     of the application.
        //
        // Exceptions:
        //   System.InvalidOperationException:
        //     The System.Diagnostics.Process.StandardOutput stream has not been defined
        //     for redirection; ensure System.Diagnostics.ProcessStartInfo.RedirectStandardOutput
        //     is set to true and System.Diagnostics.ProcessStartInfo.UseShellExecute is
        //     set to false.- or - The System.Diagnostics.Process.StandardOutput stream
        //     has been opened for asynchronous read operations with System.Diagnostics.Process.BeginOutputReadLine().
        [Browsable(false)]
        [MonitoringDescription("ProcessStandardOutput")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StreamReader StandardOutput { get; }
        //
        // Summary:
        //     Gets or sets the properties to pass to the System.Diagnostics.Process.Start()
        //     method of the System.Diagnostics.Process.
        //
        // Returns:
        //     The System.Diagnostics.ProcessStartInfo that represents the data with which
        //     to start the process. These arguments include the name of the executable
        //     file or document used to start the process.
        //
        // Exceptions:
        //   System.ArgumentNullException:
        //     The value that specifies the System.Diagnostics.Process.StartInfo is null.
        [MonitoringDescription("ProcessStartInfo")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ProcessStartInfo StartInfo { get; set; }
        //
        // Summary:
        //     Gets the time that the associated process was started.
        //
        // Returns:
        //     A System.DateTime that indicates when the process started. This only has
        //     meaning for started processes.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        //
        //   System.NotSupportedException:
        //     You are attempting to access the System.Diagnostics.Process.StartTime property
        //     for a process that is running on a remote computer. This property is available
        //     only for processes that are running on the local computer.
        //
        //   System.InvalidOperationException:
        //     The process has exited.
        //
        //   System.ComponentModel.Win32Exception:
        //     An error occurred in the call to the Windows function.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessStartTime")]
        public DateTime StartTime { get; }
        //
        // Summary:
        //     Gets or sets the object used to marshal the event handler calls that are
        //     issued as a result of a process exit event.
        //
        // Returns:
        //     The System.ComponentModel.ISynchronizeInvoke used to marshal event handler
        //     calls that are issued as a result of an System.Diagnostics.Process.Exited
        //     event on the process.
        [DefaultValue("")]
        [Browsable(false)]
        [MonitoringDescription("ProcessSynchronizingObject")]
        public ISynchronizeInvoke SynchronizingObject { get; set; }
        //
        // Summary:
        //     Gets the set of threads that are running in the associated process.
        //
        // Returns:
        //     An array of type System.Diagnostics.ProcessThread representing the operating
        //     system threads currently running in the associated process.
        //
        // Exceptions:
        //   System.SystemException:
        //     The process does not have an System.Diagnostics.Process.Id, or no process
        //     is associated with the System.Diagnostics.Process instance.-or- The associated
        //     process has exited.
        //
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me); set
        //     System.Diagnostics.ProcessStartInfo.UseShellExecute to false to access this
        //     property on Windows 98 and Windows Me.
        [MonitoringDescription("ProcessThreads")]
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ProcessThreadCollection Threads { get; }
        //
        // Summary:
        //     Gets the total processor time for this process.
        //
        // Returns:
        //     A System.TimeSpan that indicates the amount of time that the associated process
        //     has spent utilizing the CPU. This value is the sum of the System.Diagnostics.Process.UserProcessorTime
        //     and the System.Diagnostics.Process.PrivilegedProcessorTime.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        //
        //   System.NotSupportedException:
        //     You are attempting to access the System.Diagnostics.Process.TotalProcessorTime
        //     property for a process that is running on a remote computer. This property
        //     is available only for processes that are running on the local computer.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessTotalProcessorTime")]
        public TimeSpan TotalProcessorTime { get; }
        //
        // Summary:
        //     Gets the user processor time for this process.
        //
        // Returns:
        //     A System.TimeSpan that indicates the amount of time that the associated process
        //     has spent running code inside the application portion of the process (not
        //     inside the operating system core).
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        //
        //   System.NotSupportedException:
        //     You are attempting to access the System.Diagnostics.Process.UserProcessorTime
        //     property for a process that is running on a remote computer. This property
        //     is available only for processes that are running on the local computer.
        [MonitoringDescription("ProcessUserProcessorTime")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TimeSpan UserProcessorTime { get; }
        //
        // Summary:
        //     Gets the size of the process's virtual memory.
        //
        // Returns:
        //     The amount of virtual memory, in bytes, that the associated process has requested.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.VirtualMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        [MonitoringDescription("ProcessVirtualMemorySize")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VirtualMemorySize { get; }
        //
        // Summary:
        //     Gets the amount of the virtual memory allocated for the associated process.
        //
        // Returns:
        //     The amount of virtual memory, in bytes, allocated for the associated process.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessVirtualMemorySize")]
        [ComVisible(false)]
        public long VirtualMemorySize64 { get; }
        //
        // Summary:
        //     Gets the associated process's physical memory usage.
        //
        // Returns:
        //     The total amount of physical memory the associated process is using, in bytes.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.WorkingSet64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
        [MonitoringDescription("ProcessWorkingSet")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int WorkingSet { get; }
        //
        // Summary:
        //     Gets the amount of physical memory allocated for the associated process.
        //
        // Returns:
        //     The amount of physical memory, in bytes, allocated for the associated process.
        //
        // Exceptions:
        //   System.PlatformNotSupportedException:
        //     The platform is Windows 98 or Windows Millennium Edition (Windows Me), which
        //     does not support this property.
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [MonitoringDescription("ProcessWorkingSet")]
        [ComVisible(false)]
        public long WorkingSet64 { get; }
        */

    }
}
