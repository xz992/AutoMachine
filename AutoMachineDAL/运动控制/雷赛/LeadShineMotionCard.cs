using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AutoMachineDAL
{

    public class Dmc1000_Dll
    {
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_board_init", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_board_init();
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_board_close", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_board_close();

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_set_pls_outmode", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_set_pls_outmode(int axis, int pls_outmode);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_get_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_speed(int axis);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_change_speed", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_change_speed(int axis, int NewVel);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_decel_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_decel_stop(int axis);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_immediate_stop", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_immediate_stop(int axis);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_start_t_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_t_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_start_ta_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_ta_move(int axis, int Pos, int StrVel, int MaxVel, double Tacc);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_start_s_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_s_move(int axis, int Dist, int StrVel, int MaxVel, double Tacc);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_start_sa_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_sa_move(int axis, int Pos, int StrVel, int MaxVel, double Tacc);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_start_tv_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_tv_move(int axis, int StrVel, int MaxVel, double Tacc);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_start_sv_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_sv_move(int axis, int StrVel, int MaxVel, double Tacc);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_set_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_set_s_profile(int axis, double s_para);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_get_s_profile", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_s_profile(int axis, ref double s_para);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_start_t_line", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_t_line(int TotalAxis, ref int AxisArray, ref int DistArray, int StrVel, int MaxVel, double Tacc);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_start_ta_line", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_start_ta_line(int TotalAxis, ref int AxisArray, ref int DistArray, int StrVel, int MaxVel, double Tacc);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_home_move", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_home_move(int axis, int StrVel, int MaxVel, double Tacc);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_check_done", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_check_done(int axis);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_get_command_pos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_command_pos(int axis);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_set_command_pos", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_set_command_pos(int axis, double Pos);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_out_bit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_out_bit(int BitNo, int BitData);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_in_bit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_in_bit(int BitNo);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_get_outbit", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_outbit(int BitNo);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_in_enable", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_in_enable(int CardNo, int InputEn);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_set_sd", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_set_sd(int axis, int SdMode);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_get_axis_status", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_get_axis_status(int axis);

        [DllImport("Dmc1000.dll", EntryPoint = "d1000_WriteDWord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_WriteDWord(int addr, int data);
        [DllImport("Dmc1000.dll", EntryPoint = "d1000_ReadDWord", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern int d1000_ReadDWord(int addr);
    }
    
      
    public class LeadShineMotionCard : MotionAbstractClass
    {

        const int ERR_NoError = 0;
        const int ERR_BoardNumber = 0;
        const int ERR_ParaAxis = 0;
        const int ERR_ParaData = 0;


        //初始化运动控制卡
        public override bool InitMotionCard()
        {
            int nCard = Dmc1000_Dll.d1000_board_init();
            if (nCard <= 0)
            {
                if (nCard <= 0)//控制卡初始化
                    MessageBox.Show("未找到控制卡!", "警告");
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
            int State = Dmc1000_Dll.d1000_board_close();
            if (State != ERR_NoError)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        //相对点到点运动(T梯形速度曲线)
        public override bool AxisRelativeMovePTP_T(int Axis, double Curve, int StartSpeed, double AccSpeed, double DecSpeed, int MaxSpeed, int StopSpeed, int Distance, bool Synchronous, double DelayTime, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }

            State = Dmc1000_Dll.d1000_start_t_move(Axis, Distance, StartSpeed, MaxSpeed, AccSpeed);
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴相对运动失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴相对运动成功";

            }

            return true;

        }


        //绝对点到点运动(T梯形速度曲线)
        public override bool AxisAbsolutionMovePTP_T(int Axis, double Curve, int StartSpeed, double AccSpeed, double DecSpeed, int MaxSpeed, int StopSpeed, int Position, bool Synchronous, double DelayTime, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }

            State = Dmc1000_Dll.d1000_start_ta_move(Axis, Position, StartSpeed, MaxSpeed, AccSpeed);
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴绝对运动失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴绝对运动成功";

            }

            return true;

        }


        //相对点到点运动(S梯形速度曲线)
        public override bool AxisRelativeMovePTP_S(int Axis, int Distance, int StartSpeed, int MaxSpeed, Double AccSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }

            State = Dmc1000_Dll.d1000_start_s_move(Axis, Distance, StartSpeed, MaxSpeed, AccSpeed);
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴相对运动失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴相对运动成功";

            }

            return true;

        }


        //绝对点到点运动(S梯形速度曲线)
        public override bool AxisAbsolutionMovePTP_S(int Axis, int Position, int StartSpeed, int MaxSpeed, Double AccSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }

            State = Dmc1000_Dll.d1000_start_sa_move(Axis, Position, StartSpeed, MaxSpeed, AccSpeed);
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴绝对运动失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴绝对运动成功";

            }

            return true;

        }


        //梯形速度运动(T梯形速度曲线)
        public override bool AxisVelocityMove_T(int Axis, int MaxSpeed, int StopSpedDec, double Curve, Double AccSpeed, string DecSpeed, string Direct, int Position, int StartSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }

            State = Dmc1000_Dll.d1000_start_tv_move(Axis, StartSpeed, MaxSpeed, AccSpeed);
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴T型速度运动失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴T型速度运动成功";

            }

            return true;

        }


        //S形速度运动(S梯形速度曲线)
        public override bool AxisVelocityMove_S(int Axis, int Position, int StartSpeed, int MaxSpeed, Double AccSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }
            State = Dmc1000_Dll.d1000_start_sv_move(Axis, StartSpeed, MaxSpeed, AccSpeed);
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴S型速度运动失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴S型速度运动成功";

            }

            return true;

        }


        //减速停止
        public override bool DecSpeedStop(int Axis, int StopSpedDec, int Position, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }


            State = Dmc1000_Dll.d1000_decel_stop(Axis);
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴减速停止设置失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴减速停止设置成功";

            }

            return true;

        }


        //紧急停止
        public override bool Immediate_Stop(int Axis, int Position, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }


            State = Dmc1000_Dll.d1000_immediate_stop(Axis);
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴紧急停止失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴紧急停止成功";

            }

            return true;

        }


        //设置轴当前位置
        public override bool SetAxisCurrestPosition(int Axis, int Position, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }

            State = Dmc1000_Dll.d1000_set_command_pos(Axis, Position);
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴当前位置设置失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴当前位置设置成功";

            }

            return true;

        }


        //获取指定轴的运动状态
        public override void GetAxisRunState(int Axis, out int State, out string ErrorMessage)
        {

            State = -1;
            string TempErrorMessage = "";

            State = Dmc1000_Dll.d1000_check_done(Axis);
            switch (State)
            {
                case 0:
                    TempErrorMessage = "正在运行";
                    break;
                case 1:
                    TempErrorMessage = "脉冲输出完毕停止";
                    break;
                case 2:
                    TempErrorMessage = "指令停止";
                    break;
                case 3:
                    TempErrorMessage = "遇限位停止";
                    break;
                case 4:
                    TempErrorMessage = "遇原点停止";
                    break;
                default:
                    break;
            }

            ErrorMessage = TempErrorMessage;

        }


        //读取指定轴的专用接口信号状态,取低字节;
        public override void GetAxisSpecialState(int Axis, out int State)
        {

            State = -1;

            State = Dmc1000_Dll.d1000_get_axis_status(Axis);

            State = State & 0xff;


        }


        //原点归位
        public override bool AxisReturnHome(int Axis, int OriginMode, int OriginDirect, double Curve, double AccSpeed, int DecSpeed, int StartSpeed, int OriginLocationSpeed, bool Synchronous, double DelayTime, int EZA, int SHIFT, int POSITION, int MaxSpeed, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }


            State = Dmc1000_Dll.d1000_home_move(Axis, StartSpeed, MaxSpeed, AccSpeed);//原点归位
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴原点归位失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴指令运行正常";

            }

            return true;

        }


        //写输出端口
        public override bool WriteOutputPortBit(int Axis, int Board_ID, int PortNumber, int PortBitData, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }



            State = Dmc1000_Dll.d1000_out_bit(PortNumber, PortBitData);


            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "输出端口写入失败";
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "输出端口写入正常";

            }

            return true;

        }


        //读输出端口
        public override bool ReadOutputPortBit(int Axis, int PortNumber, out int PortBitData, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                PortBitData = 2;
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                PortBitData = 2;
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                PortBitData = 2;
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }



            PortBitData = Dmc1000_Dll.d1000_get_outbit(PortNumber);

            return true;


        }


        //读输入端口
        public override bool ReadInputPortBit(int Axis, int Board_ID, int PortNumber, out int PortBitData, int AccEnable, int PulseMode, out string ErrorMessage)
        {

            int State = -1;

            if (Dmc1000_Dll.d1000_check_done(Axis) == 0)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴正在运行";
                PortBitData = 2;
                return false;
            }


            State = Dmc1000_Dll.d1000_set_sd(Axis, AccEnable);//设置减速开关是否有效
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能失败";
                PortBitData = 2;
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置减速信号使能成功";

            }


            State = Dmc1000_Dll.d1000_set_pls_outmode(Axis, PulseMode);//设置控制卡脉冲输出模式
            if (State != ERR_NoError)
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式失败";
                PortBitData = 2;
                return false;
            }
            else
            {
                ErrorMessage = Convert.ToString(Axis) + "轴设置控制卡脉冲输出模式成功";

            }



            PortBitData = Dmc1000_Dll.d1000_in_bit(PortNumber);

            return true;


        }


    }
}
