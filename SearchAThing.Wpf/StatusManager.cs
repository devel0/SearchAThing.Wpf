using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SearchAThing.Wpf
{

    /// <summary>
    /// Manage concurrent status set, with detect of the last status release.
    /// Example of usage : https://searchathing.com/?p=1424
    /// </summary>
    public class StatusManager : IDisposable, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        uint statusId;
        object statusIdLck;

        HashSet<uint> statusIdSet;
        Dictionary<uint, string> statusIdMsgDict;

        object autoclearLck;
        CancellationTokenSource cts = new CancellationTokenSource();
        int autoclearUnassignedIdMs = 0;
        DateTime? lastStatusChange = null;

        /// <summary>
        /// specify in autoclearUnassignedIdMs a value in ms greather than 0 to autoclear status if directly assigned ( ie not using NewStatus, ReleaseStatus )
        /// </summary>
        public StatusManager(int _autoclearUnassignedIdMs = 0)
        {
            statusId = 0;
            statusIdSet = new HashSet<uint>();
            statusIdLck = new object();
            statusIdMsgDict = new Dictionary<uint, string>();
            this.autoclearUnassignedIdMs = _autoclearUnassignedIdMs;

            if (_autoclearUnassignedIdMs > 0)
            {
                autoclearLck = new object();

                Task.Run(async () =>
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        if (lastStatusChange.HasValue && (DateTime.Now - lastStatusChange.Value).TotalMilliseconds >= _autoclearUnassignedIdMs)
                        {
                            lock (autoclearLck)
                            {
                                lastStatusChange = null;
                                lock (statusIdLck)
                                {
                                    if (statusIdSet.Count > 0)
                                    {
                                        var id = statusIdSet.Last();
                                        var str = statusIdMsgDict[id];
                                        ManagedStatus = str;
                                    }
                                    else
                                        ManagedStatus = "";
                                }
                            }
                        }
                        await Task.Delay(500, cts.Token);
                    }
                });
            }
        }

        public void Dispose()
        {
            cts.Cancel();
        }

        bool _progress_visible;
        public bool ProgressVisible
        {
            get { return _progress_visible; }
            set
            {
                if (_progress_visible != value)
                {
                    _progress_visible = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProgressVisible"));
                }
            }
        }

        double _progress_value;
        public double ProgressValue
        {
            get { return _progress_value; }
            set
            {
                if (_progress_value != value)
                {
                    _progress_value = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ProgressValue"));
                }
            }
        }

        string _status;
        /// <summary>
        /// Bind your textblock to this property.
        /// You can change the status simply by changing this property value.
        /// </summary>
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
                    if (autoclearUnassignedIdMs > 0)
                    {
                        lock (autoclearLck)
                        {
                            lastStatusChange = DateTime.Now;
                        }
                    }
                }
            }
        }

        private string ManagedStatus
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Status"));
                }
            }
        }

        public void Clear(string defaultMessage = "Ready.")
        {
            ManagedStatus = defaultMessage;
        }

        /// <summary>
        /// For long-running task in order to release the status
        /// displaying some ready message, use the NewStatus as first
        /// then release at the end with the id this function return.
        /// </summary>        
        public uint NewStatus(string msg)
        {
            var id = 0u;

            lock (statusIdLck)
            {
                id = ++statusId;
                statusIdSet.Add(statusId);
                statusIdMsgDict.Add(statusId, msg);
            }

            ManagedStatus = msg;

            return id;
        }

        /// <summary>
        /// Set the given msg ready status if no other status are actually running.
        /// </summary>        
        public void ReleaseStatus(uint id, string msg = "Ready.")
        {
            var empty = false;
            var idMsg = "";

            string back_msg = null;

            lock (statusIdLck)
            {
                statusIdSet.Remove(id);
                empty = statusIdSet.Count == 0;
                if (!empty)
                {
                    back_msg = statusIdMsgDict[statusIdSet.Max()];
                }
#if DEBUG
                if (!statusIdMsgDict.ContainsKey(id)) Debugger.Break();
                idMsg = statusIdMsgDict[id];
                statusIdMsgDict.Remove(id); // avoid app crash if any
#endif
            }

            if (empty)
                ManagedStatus = msg;
            else
            {
                if (back_msg != null)
                    ManagedStatus = back_msg;
                else
                    ManagedStatus = $"{idMsg} [done]";
            }
        }

    }

}
