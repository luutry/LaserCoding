using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modbus;
using System.Net;
using System.Threading;
using System.ComponentModel;
using System.Diagnostics;

namespace QuickCoding
{
    public class ModbusManager 
    {
        ModbusMasterTCP _ModbusTCP;
        ModbusMasterSerial _ModbusRTU;
        ModbusMaster _Master;
        Task _T;
        CancellationTokenSource _CTS;
        object _Locker = new object();

        #region 属性

        //string _Version;
        //public string Version
        //{
        //    get { return _Version; }
        //    private set
        //    {
        //        _Version = value;
        //        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Version"));
        //    }
        //}

        //string _SamlightVersion;
        //public string SamlightVersion
        //{
        //    get { return _SamlightVersion; }
        //    private set
        //    {
        //        _SamlightVersion = value;
        //        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("SamlightVersion"));
        //    }
        //}


        public bool Connected { get { return TCPConnected || RTUConnected; } }

        bool _TCPConnected;
        public bool TCPConnected
        {
            get { return _TCPConnected; }
            private set
            {
                _TCPConnected = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TCPConnected"));
            }
        }

        bool _RTUConnected;
        public bool RTUConnected
        {
            get { return _RTUConnected; }
            private set
            {
                _RTUConnected = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("RTUConnected"));
            }
        }


        string _Interlock;
        public string Interlock
        {
            get { return _Interlock; }
            private set
            {
                _Interlock = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Interlock"));
            }
        }

        string _USBStatus;
        public string USBStatus
        {
            get { return _USBStatus; }
            private set
            {
                _USBStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("USBStatus"));
            }
        }

        string _USCEStop;
        public string USCEStop
        {
            get { return _USCEStop; }
            private set
            {
                _USCEStop = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("USCEStop"));
            }
        }

        string _ScannerError;
        public string ScannerError
        {
            get { return _ScannerError; }
            private set
            {
                _ScannerError = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ScannerError"));
            }
        }

        string _SystemTime;
        public string SystemTime
        {
            get { return _SystemTime; }
            private set
            {
                _SystemTime = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("SystemTime"));
            }
        }

        string _TotalMarkTime;
        public string TotalMarkTime
        {
            get { return _TotalMarkTime; }
            private set
            {
                _TotalMarkTime = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TotalMarkTime"));
            }
        }

        string _ActiveGood;
        public string ActiveGood
        {
            get { return _ActiveGood; }
            private set
            {
                _ActiveGood = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ActiveGood"));
            }
        }

        ushort _TemplateStatus;
        public ushort TemplateStatus
        {
            get { return _TemplateStatus; }
            private set
            {
                _TemplateStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TemplateStatus"));
            }
        }

        ushort _TextStatus;
        public ushort TextStatus
        {
            get { return _TextStatus; }
            private set
            {
                _TextStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TextStatus"));
            }
        }

        ushort _DateStatus;
        public ushort DateStatus
        {
            get { return _DateStatus; }
            private set
            {
                _DateStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DateStatus"));
            }
        }

        ushort _SerialStatus;
        public ushort SerialStatus
        {
            get { return _SerialStatus; }
            private set
            {
                _SerialStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("SerialStatus"));
            }
        }

        ushort _DataMatrixStatus;
        public ushort DataMatrixStatus
        {
            get { return _DataMatrixStatus; }
            private set
            {
                _DataMatrixStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("DataMatrixStatus"));
            }
        }

        ushort _SystemStatus;
        public ushort SystemStatus
        {
            get { return _SystemStatus; }
            private set
            {
                _SystemStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("SystemStatus"));
            }
        }

        ushort _ExecutionCount;
        public ushort ExecutionCount
        {
            get { return _ExecutionCount; }
            private set
            {
                _ExecutionCount = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("ExecutionCount"));
            }
        }

        string _Executing;
        public string Executing
        {
            get { return _Executing; }
            private set
            {
                _Executing = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Executing"));
            }
        }

        string _MarkBeginStatus;
        public string MarkBeginStatus
        {
            get { return _MarkBeginStatus; }
            private set
            {
                _MarkBeginStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MarkBeginStatus"));
            }
        }

        string _MarkEndStatus;
        public string MarkEndStatus
        {
            get { return _MarkEndStatus; }
            private set
            {
                _MarkEndStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MarkEndStatus"));
            }
        }

        string _TotalMarkCount;
        public string TotalMarkCount
        {
            get { return _TotalMarkCount; }
            private set
            {
                _TotalMarkCount = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("TotalMarkCount"));
            }
        }

        string _MarkDelay;
        public string MarkDelay
        {
            get { return _MarkDelay; }
            private set
            {
                _MarkDelay = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("MarkDelay"));
            }
        }

        string _CO2_LaserReady;
        public string CO2_LaserReady
        {
            get { return _CO2_LaserReady; }
            private set
            {
                _CO2_LaserReady = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CO2_LaserReady"));
            }
        }


        string _CO2_OverTemp;
        public string CO2_OverTemp
        {
            get { return _CO2_OverTemp; }
            private set
            {
                _CO2_OverTemp = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CO2_OverTemp"));
            }
        }

        string _CO2_DCFault;
        public string CO2_DCFault
        {
            get { return _CO2_DCFault; }
            private set
            {
                _CO2_DCFault = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CO2_DCFault"));
            }
        }

        string _FiberStatus;
        public string FiberStatus
        {
            get { return _FiberStatus; }
            private set
            {
                _FiberStatus = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("FiberStatus"));
            }
        }


        #endregion

        public void TCPConnect(string ipaddr)
        {
            if (_ModbusTCP == null || !_ModbusTCP.Connected)
            {
                _ModbusTCP = new ModbusMasterTCP(ipaddr, 502);
                _ModbusTCP.Connect();

                //读版本号 
                //ushort[] version;
                //ushort[] samlightversion;
                //lock (_Locker)
                //{
                //    version = _ModbusTCP.ReadInputRegisters(1, 52000, 100);
                //    samlightversion = _ModbusTCP.ReadInputRegisters(1, 52100, 100);
                //}

                //string ver = Class1.StringFromUShortArray(version);
                //string samver = Class1.StringFromUShortArray(samlightversion);

                //if (!string.IsNullOrEmpty(ver) && !string.IsNullOrEmpty(samver))
                //{
                //    Version = ver;
                //    SamlightVersion = samver;

                //    //线程开始
                //    if (_T == null || _T.Status == TaskStatus.RanToCompletion)
                //        Start();
                //}
                //else
                //    throw new Exception("不是东一打码机");

                TCPConnected = _ModbusTCP.Connected;
            }
        }

        public void TCPDisconnect()
        {
            if (_ModbusTCP != null && _ModbusTCP.Connected)
            {
                if (_T != null || _T.Status == TaskStatus.Running && !RTUConnected)
                    Stop();

                _ModbusTCP.Disconnect();
                TCPConnected = _ModbusTCP.Connected;
            }
        }

        public void RTUConnect(string comport)
        {

        }

        public void RTUDisconnect()
        {

        }

        public void Start()
        {
            _CTS = new CancellationTokenSource();
            _T = Task.Factory.StartNew(() =>
            {
                while (!_CTS.IsCancellationRequested)
                {
                    if (_ModbusTCP != null && _ModbusTCP.Connected)
                        _Master = _ModbusTCP;
                    else if (_ModbusRTU != null && _ModbusRTU.Connected)
                        _Master = _ModbusRTU;

                    ushort[] sr;
                    string status = string.Empty;
                    lock (_Locker)
                        sr = _Master.ReadInputRegisters(1, 50000, 120);
                        
                    if (sr == null)//连接已断开
                    {

                    }
                    else
                    {
                        //安全互锁
                        switch (sr[0])
                        {
                            case 1:
                                status = "已连接";
                                break;
                            case 2:
                                status = "断开";
                                break;
                            default:
                                break;
                        }
                        Interlock = string.Format("{0} ({1})", sr[0], status);

                        //USB连接状态
                        switch (sr[1])
                        {
                            case 1:
                                status = "已连接";
                                break;
                            case 2:
                                status = "关闭";
                                break;
                            case 3:
                                status = "正在启动";
                                break;
                            case 4:
                                status = "丢失";
                                break;
                            default:
                                status = string.Empty;
                                break;
                        }
                        USBStatus = string.Format("{0} ({1})", sr[1], status);

                        //打标卡急停
                        switch (sr[2])
                        {
                            case 2:
                                status = "急停";
                                break;
                            case 1:
                                status = "正常";
                                break;
                            default:
                                status = string.Empty;
                                break;
                        }
                        USCEStop = string.Format("{0} ({1})", sr[2], status);


                        //振镜头报错
                        switch (sr[3])
                        {
                            case 2:
                                status = "报错";
                                break;
                            case 1:
                                status = "正常";
                                break;
                            default:
                                status = string.Empty;
                                break;
                        }
                        ScannerError = string.Format("{0} ({1})", sr[3], status);

                        SystemTime = new DateTime(sr[6], sr[7], sr[8], sr[9], sr[10], sr[11]).ToString();
                        TotalMarkTime = new TimeSpan(sr[12], sr[13], sr[14], sr[15]).ToString();

                        switch (sr[16])
                        {
                            case 1:
                                status = "正常";
                                break;
                            case 2:
                                status = "错误";
                                break;
                            default:
                                status = string.Empty;
                                break;
                        }
                        ActiveGood = string.Format("{0} ({1})", sr[16], status);

                        TemplateStatus = sr[17];
                        TextStatus = sr[18];
                        DateStatus = sr[19];
                        SerialStatus = sr[20];
                        DataMatrixStatus = sr[21];
                        SystemStatus = sr[22];
                        ExecutionCount = sr[23];

                        switch (sr[22])
                        {
                            case 1:
                                status = "空闲";
                                break;
                            case 2:
                                status = "正在执行";
                                break;
                            default:
                                status = string.Empty;
                                break;
                        }
                        Executing = string.Format("{0} ({1})", sr[24], status);

                        MarkBeginStatus = string.Format("{0} ({1})", sr[25], (QuickCoding.StatusCode)sr[25]);
                        MarkEndStatus = string.Format("{0} ({1})", sr[26], (QuickCoding.StatusCode)sr[26]);
                        TotalMarkCount = sr[27].ToString();
                        MarkDelay = string.Format("{0} (ms)", sr[28]);

                        switch (sr[29])
                        {
                            case 0:
                                status = "NA";
                                break;
                            case 1:
                                status = "正常";
                                break;
                            case 2:
                                status = "报警";
                                break;
                            default:
                                break;
                        }
                        CO2_LaserReady = string.Format("{0} ({1})", sr[29], status);

                        switch (sr[30])
                        {
                            case 0:
                                status = "NA";
                                break;
                            case 1:
                                status = "正常";
                                break;
                            case 2:
                                status = "报警";
                                break;
                            default:
                                break;
                        }
                        CO2_OverTemp = string.Format("{0} ({1})", sr[30], status);

                        switch (sr[31])
                        {
                            case 0:
                                status = "NA";
                                break;
                            case 1:
                                status = "正常";
                                break;
                            case 2:
                                status = "报警";
                                break;
                            default:
                                break;
                        }
                        CO2_DCFault = string.Format("{0} ({1})", sr[31], status);


                        switch (sr[32])
                        {
                            case 0:
                                status = "NA";
                                break;
                            case 1:
                                status = "温度异常";
                                break;
                            case 2:
                                status = "高反";
                                break;
                            case 3:
                                status = "正常";
                                break;
                            case 4:
                                status = "MO失败";
                                break;
                            default:
                                break;
                        }
                        FiberStatus = string.Format("{0} ({1})", sr[32], status);
                    }
                    SpinWait.SpinUntil(() => false, 100);
                }
            }, _CTS.Token);

   
        }

        public void Stop()
        {
            _CTS.Cancel();
            _T.Wait();
        }

        public ushort[] ReadInputRegisters(ushort start, ushort len)
        {

                
           ushort[] result = null;
            if (RTUConnected || TCPConnected)
            {
               lock (_Locker)
                       result = _Master.ReadInputRegisters(1, start, len);

            }
           return result;
        }

        public ushort[] ReadHoldingRegisters(ushort start, ushort len)
        {
            ushort[] result = null;
            if (RTUConnected || TCPConnected)
            {
                lock (_Locker)
                {
                    result = _Master.ReadHoldingRegisters(1, start, len);
                }    
            }
            return result;
        }

        public void WriteMultipleRegisters(ushort start, ushort[] values)
        {
            if (RTUConnected || TCPConnected)
            {
                lock (_Locker)
                    _Master.WriteMultipleRegisters(1, start, values);
            }
        }

        public void WriteSingleCoil(ushort address, bool value)
        {
            if (RTUConnected || TCPConnected)
            {
                lock (_Locker)
                    _Master.WriteSingleCoil(1, address, value);
            }
        }

        short _OffsetX;
        public short OffsetX
        {
            get { return _OffsetX; }
            private set
            {
                if (_OffsetX != value)
                {
                    _OffsetX = value;
                    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("OffsetX"));
                }
            }
        }

        short _OffsetY;
        public short OffsetY
        {
            get { return _OffsetY; }
            private set
            {
                if (_OffsetY != value)
                {
                    _OffsetY = value;
                    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("OffsetY"));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }
}