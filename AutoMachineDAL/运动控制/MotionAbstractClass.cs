using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMachineDAL
{
    public abstract class MotionAbstractClass
    {

        //初始化运动控制卡
        public abstract bool InitMotionCard();

        //关闭运动控制卡
        public abstract bool CloseMotionCard();

        //相对点到点运动(T梯形速度曲线)
        public abstract bool AxisRelativeMovePTP_T(int Axis, double Curve, int StartSpeed, double AccSpeed, double DecSpeed, int MaxSpeed, int StopSpeed, int Distance, bool Synchronous, double DelayTime, int AccEnable, int PulseMode, out string ErrorMessage);

        //绝对点到点运动(T梯形速度曲线)
        public abstract bool AxisAbsolutionMovePTP_T(int Axis, double Curve, int StartSpeed, double AccSpeed, double DecSpeed, int MaxSpeed, int StopSpeed, int Position, bool Synchronous, double DelayTime, int AccEnable, int PulseMode, out string ErrorMessage);

        //相对点到点运动(S梯形速度曲线)
        public abstract bool AxisRelativeMovePTP_S(int Axis, int Distance, int StartSpeed, int MaxSpeed, Double AccSpeed, int AccEnable, int PulseMode, out string ErrorMessage);

        //绝对点到点运动(S梯形速度曲线)
        public abstract bool AxisAbsolutionMovePTP_S(int Axis, int Position, int StartSpeed, int MaxSpeed, Double AccSpeed, int AccEnable, int PulseMode, out string ErrorMessage);

        //梯形速度运动(T梯形速度曲线)
        public abstract bool AxisVelocityMove_T(int Axis, int MaxSpeed, int StopSpedDec, double Curve, Double AccSpeed, string DecSpeed, string Direct, int Position, int StartSpeed, int AccEnable, int PulseMode, out string ErrorMessage);

        //S形速度运动(S梯形速度曲线)
        public abstract bool AxisVelocityMove_S(int Axis, int Position, int StartSpeed, int MaxSpeed, Double AccSpeed, int AccEnable, int PulseMode, out string ErrorMessage);

        //减速停止
        public abstract bool DecSpeedStop(int Axis, int StopSpedDec, int Position, int AccEnable, int PulseMode, out string ErrorMessage);

        //紧急停止
        public abstract bool Immediate_Stop(int Axis, int Position, int AccEnable, int PulseMode, out string ErrorMessage);

        //设置轴当前位置
        public abstract bool SetAxisCurrestPosition(int Axis, int Position, int AccEnable, int PulseMode, out string ErrorMessage);

        //获取指定轴的运动状态
        public abstract void GetAxisRunState(int Axis, out int State, out string ErrorMessage);

        //读取指定轴的专用接口信号状态,取低字节;
        public abstract void GetAxisSpecialState(int Axis, out int State);

        //原点归位
        public abstract bool AxisReturnHome(int Axis, int OriginMode, int OriginDirect, double Curve, double AccSpeed, int DecSpeed, int StartSpeed, int OriginLocationSpeed, bool Synchronous, double DelayTime, int EZA, int SHIFT, int POSITION, int MaxSpeed, int AccEnable, int PulseMode, out string ErrorMessage);

        //写输出端口
        public abstract bool WriteOutputPortBit(int Axis, int Board_ID, int PortNumber, int PortBitData, int AccEnable, int PulseMode, out string ErrorMessage);

        //读输出端口
        public abstract bool ReadOutputPortBit(int Axis, int PortNumber, out int PortBitData, int AccEnable, int PulseMode, out string ErrorMessage);

        //读输入端口
        public abstract bool ReadInputPortBit(int Axis, int Board_ID, int PortNumber, out int PortBitData, int AccEnable, int PulseMode, out string ErrorMessage);
      

    }//类结束

}//命名空间结束
