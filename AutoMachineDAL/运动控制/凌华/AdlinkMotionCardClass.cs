using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AutoMachineDAL
{
    public class AdlinkMotionCardDll
    {
        [DllImport("AMP208C.dll", EntryPoint = "InitMotionCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool InitMotionCard();


        [DllImport("AMP208C.dll", EntryPoint = "CloseMotionCard", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool CloseMotionCard();


        [DllImport("AMP208C.dll", EntryPoint = "Reset_Motion_IO_Single", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Reset_Motion_IO_Single(string Board_ID, string Axis_ID);

        [DllImport("AMP208C.dll", EntryPoint = "WaitStop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void WaitStop(string DelayTime, string Axis_ID);


        [DllImport("AMP208C.dll", EntryPoint = "OriginSearch", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool OriginSearch(string Axis_ID, string OriginMode, string OriginDirect, string Curve, string AccSpeed, string DecSpeed, string StartSpeed, string OriginSearchSpeed, string OriginLocationSpeed, bool Synchronous, string DelayTime, string EZA, string SHIFT, string POSITION);


        [DllImport("AMP208C.dll", EntryPoint = "MotionAbsolution", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MotionAbsolution(string Axis_ID, string Curve, string StartSpeed, string AccSpeed, string DecSpeed, string MaxSpeed, string StopSpeed, string Position, bool Synchronous, string DelayTime);


        [DllImport("AMP208C.dll", EntryPoint = "MotionRelative", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MotionRelative(string Axis_ID, string Curve, string StartSpeed, string AccSpeed, string DecSpeed, string MaxSpeed, string StopSpeed, string Position, bool Synchronous, string DelayTime);


        [DllImport("AMP208C.dll", EntryPoint = "Set_Current_Positon", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Set_Current_Positon(string Axis_ID, string Position);


        [DllImport("AMP208C.dll", EntryPoint = "Velocity_Move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void Velocity_Move(string Axis_ID, string MaxSpeed, string StopSpedDec, string Curve, string AccSpeed, string DecSpeed, string Direct);



        [DllImport("AMP208C.dll", EntryPoint = "DecStopMove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void DecStopMove(string Axis_ID, string StopSpedDec);


        [DllImport("AMP208C.dll", EntryPoint = "ImmediatelyStopMove", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ImmediatelyStopMove(string Axis_ID);



        [DllImport("AMP208C.dll", EntryPoint = "PortOut", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void PortOut(string Board_ID, string OutPortNumber);


        [DllImport("AMP208C.dll", EntryPoint = "ComeInPort", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ComeInPort(string Board_ID, string OutPortNumber);



        [DllImport("AMP208C.dll", EntryPoint = "ContinueVelocity_Move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int ContinueVelocity_Move(string Axis_ID, string MaxSpeed, string StopSpedDec, string Curve, string AccSpeed, string DecSpeed, string Direct);


        [DllImport("AMP208C.dll", EntryPoint = "ReadAxisCurrentPosition", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void ReadAxisCurrentPosition(string Axis_ID, ref  double command_position, ref  double feedback_position, ref  double target_position, ref  double command_velocity, ref  double error_position);


        [DllImport("AMP208C.dll", EntryPoint = "TwoAxisInterpolationAbsolute", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void TwoAxisInterpolationAbsolute(string Axis_ID1, string Axis_ID2, string Curve, string StartSpeed, string AccSpeed, string DecSpeed, string MaxSpeed, string StopSpeed, string Position1, string Position2, bool Synchronous, string DelayTime);


        [DllImport("AMP208C.dll", EntryPoint = "StartGear", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int StartGear(string SlaveAxis_ID, string MasterAxis_ID, string PRA_GANTRY_PROTECT_1_Enable, string PRA_GANTRY_PROTECT_2_Enable, string PRA_GEAR_RATIO_Value, string PRA_GEAR_ENGAGE_RATE_Value);

        [DllImport("AMP208C.dll", EntryPoint = "test", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void test(string p);
    }

    public class AdlinkMotionCardClass:MotionAbstractClass
    {

        //初始化运动控制卡
        public override bool InitMotionCard()
        {
            bool nCard = AdlinkMotionCardDll.InitMotionCard();
            if (!nCard)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //关闭运动控制卡
        public override bool CloseMotionCard()
        {
            bool State = AdlinkMotionCardDll.CloseMotionCard();
            if (!State)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //相对点到点运动
        public override bool AxisRelativeMovePTP_T(int Axis, double Curve,int StartSpeed, double AccSpeed,double DecSpeed,int  MaxSpeed,int  StopSpeed,int Distance,bool Synchronous,double DelayTime,int AccEnable, int PulseMode, out string ErrorMessage)
        {

            AdlinkMotionCardDll.MotionRelative(Axis.ToString(), Curve.ToString(), StartSpeed.ToString(), AccSpeed.ToString(), DecSpeed.ToString(), MaxSpeed.ToString(), StopSpeed.ToString(), Distance.ToString(), Synchronous,DelayTime.ToString());

            ErrorMessage = "OK";
            return true;

        }


        //绝对点到点运动
        public override bool AxisAbsolutionMovePTP_T(int Axis, double Curve, int StartSpeed, double AccSpeed, double DecSpeed, int MaxSpeed, int StopSpeed, int Position, bool Synchronous, double DelayTime, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            AdlinkMotionCardDll.MotionAbsolution(Axis.ToString(), Curve.ToString(), StartSpeed.ToString(), AccSpeed.ToString(), DecSpeed.ToString(), MaxSpeed.ToString(), StopSpeed.ToString(), Position.ToString(), Synchronous, DelayTime.ToString());

            ErrorMessage = "OK";
            return true;

        }


        //相对点到点运动(S梯形速度曲线)
        public override bool AxisRelativeMovePTP_S(int Axis, int Distance, int StartSpeed, int MaxSpeed, Double AccSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            ErrorMessage = "OK";
            return true;
        }


        //绝对点到点运动(S梯形速度曲线)
        public override bool AxisAbsolutionMovePTP_S(int Axis, int Position, int StartSpeed, int MaxSpeed, Double AccSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            ErrorMessage = "OK";
            return true;
        }


        //梯形速度运动(T梯形速度曲线)
        public override bool AxisVelocityMove_T(int Axis,int MaxSpeed,int StopSpedDec,double Curve,  Double AccSpeed ,string DecSpeed,string Direct,int Position, int StartSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            AdlinkMotionCardDll.Velocity_Move(Axis.ToString(), MaxSpeed.ToString(), StopSpedDec.ToString(), Curve.ToString(), AccSpeed.ToString(), DecSpeed.ToString(), Direct.ToString());
            ErrorMessage = "OK";
            return true;
        }


        //S形速度运动(S梯形速度曲线)
        public override bool AxisVelocityMove_S(int Axis, int Position, int StartSpeed, int MaxSpeed, Double AccSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            ErrorMessage = "OK";
            return true;
        }


        //减速停止
        public override bool DecSpeedStop(int Axis, int StopSpedDec,int Position, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            AdlinkMotionCardDll.DecStopMove(Axis.ToString(), StopSpedDec.ToString());
            ErrorMessage = "OK";
            return true;
        }


        //紧急停止
        public override bool Immediate_Stop(int Axis, int Position, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            AdlinkMotionCardDll.ImmediatelyStopMove(Axis.ToString());
            ErrorMessage = "OK";
            return true;
        }



       //设置轴当前位置
        public override bool SetAxisCurrestPosition(int Axis, int Position, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            AdlinkMotionCardDll.Set_Current_Positon(Axis.ToString(), Position.ToString());
            ErrorMessage = "OK";
            return true;
        }


         //获取指定轴的运动状态
        public override void GetAxisRunState(int Axis, out int State, out string ErrorMessage)
        {
            State = 0;
            ErrorMessage = "OK";

        }


        //读取指定轴的专用接口信号状态,取低字节;
        public override void GetAxisSpecialState(int Axis, out int State)
        {
            State = 0;
        }


        //原点归位
        public override bool AxisReturnHome(int Axis, int OriginMode,int OriginDirect,double Curve,double AccSpeed,int DecSpeed,int StartSpeed,int OriginLocationSpeed,bool Synchronous,double DelayTime, int EZA,int SHIFT,int POSITION,int MaxSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            AdlinkMotionCardDll.OriginSearch(Axis.ToString(),OriginMode.ToString(),OriginDirect.ToString(), Curve.ToString(), AccSpeed.ToString(), DecSpeed.ToString(), StartSpeed.ToString(), MaxSpeed.ToString(), OriginLocationSpeed.ToString(), Synchronous, DelayTime.ToString(), EZA.ToString(), SHIFT.ToString(), POSITION.ToString());          
            ErrorMessage = "OK";
            return true;
        }


        //写输出端口
        public override bool WriteOutputPortBit(int Axis,int Board_ID, int PortNumber, int PortBitData, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            AdlinkMotionCardDll.PortOut(Board_ID.ToString(), PortNumber.ToString());
            ErrorMessage = "OK";
            return true;
        }


        //读输出端口
        public override bool ReadOutputPortBit(int Axis, int PortNumber, out int PortBitData, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            ErrorMessage = "OK";
            PortBitData = 0;
            return true;
        }


        //读输入端口
        public override bool ReadInputPortBit(int Axis, int Board_ID,int PortNumber, out int PortBitData, int AccEnable, int PulseMode, out string ErrorMessage)
        {
            PortBitData = AdlinkMotionCardDll.ComeInPort(Board_ID.ToString(), PortNumber.ToString());
            ErrorMessage = "OK";
            return true;
        }


    }
}
