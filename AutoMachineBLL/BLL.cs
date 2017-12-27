using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMachineDAL;
using AutoMachineModel;

namespace AutoMachineBLL
{
    public class BLL
    {
        private DAL  dal;

        public  BLL()
        {
            dal = new AutoMachineDAL.DAL();
        }

        public void BLL_Init()
        {
       
            
        }

        public void BLL_LoadProjectInfo()
        {


        }

        public void BLL_Play()
        {

        }

        public void BLL_Test()
        {

        }

        public void BLL_Stop()
        {
     

        }

        public void BLL_Exit()
        {
     
        }

        public void BLL_DisplayLog(string Message)
        {
    
        }

        public DAL BLL_GetDalLayer()
        {
            return dal;
        }

    }
}
