using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoMachineDAL
{
    public abstract class CameraAbstractClass
    {

        public bool CameraWorkMode = false;//CameraWorkMode=true:软件触发;CameraWorkMode=flase:硬件触发;


        public CameraOperator m_pOperator = new CameraOperator();

        public abstract void OpenCamera();

        public abstract void EnableSoftTrigger();
        public abstract void ContinuousAcquisiton();         
        
        public abstract void EnableHardTrigger();

        public abstract void CloseCamera();

    }
}
