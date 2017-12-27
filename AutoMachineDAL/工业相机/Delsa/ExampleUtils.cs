using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Collections;
using DALSA.SaperaLT.SapClassBasic;
using System.Windows.Forms;

namespace DALSA.SaperaLT.Examples.NET.Utils
{
    public class ExampleUtils
    {

        // Static Variables
        public  static string    configPath = "";
        const int GAMMA_FACTOR = 10000;
        const int MAX_CONFIG_FILES = 36;       // 10 numbers + 26 letters
        //static char configFileNames[MAX_CONFIG_FILES][MAX_PATH] = {0};


        static public bool GetOptionsFromCommandLine(string[] argv, MyAcquisitionParams acqParams)
        {
            // Check the command line for user commands
            if (argv[1].Equals("/?") || argv[1].Equals("-?"))
            {
                // print help
                Console.WriteLine("Usage:\n");
                Console.WriteLine("Grab  [<acquisition server name> <acquisition device index> <config filename>]\n");
                return false;
            }

            // Check if enough arguments were passed
            if (argv.Length < 4)
            {
                Console.WriteLine("Invalid command line!\n");
                return false;
            }

            // Validate server name
            if (SapManager.GetServerIndex(argv[1]) < 0)
            {
                Console.WriteLine("Invalid acquisition server name!\n");
                return false;
            }

            // Does the server support acquisition?
            int deviceCount = SapManager.GetResourceCount(argv[1], SapManager.ResourceType.Acq);
            int cameraCount = SapManager.GetResourceCount(argv[1], SapManager.ResourceType.AcqDevice);

            if (deviceCount + cameraCount == 0)
            {
                Console.WriteLine("This server does not support acquisition!\n");
                return false;
            }

            // Validate device index
            if (int.Parse(argv[2]) < 0 || int.Parse(argv[2]) >= deviceCount + cameraCount)
            {
                Console.WriteLine("Invalid acquisition device index!\n");
                return false;
            }

            if (cameraCount == 0)
            {
                if (!File.Exists(argv[3]))
                {
                    Console.WriteLine("The specified config file (" + argv[3] + "is invalid!\n");
                    return false;
                }
            }

            // Fill-in output variables
            acqParams.ServerName = argv[1];
            acqParams.ResourceIndex = int.Parse(argv[2]);
            if (cameraCount == 0)
                acqParams.ConfigFileName = argv[3];

            return true;
        }

        //////// Ask questions to user to select acquisition board/device and config file ////////
        static public bool GetOptionsFromQuestions(MyAcquisitionParams acqParams)
        {
           
            // Get total number of boards in the system
            string[] configFileNames = new string[MAX_CONFIG_FILES];
            int serverCount = SapManager.GetServerCount();
            //string configFileIndexToPrint;

            if (serverCount == 0)
            {
                MessageBox.Show("No device found!");
                return  false;
            }


            int serverIndex = serverCount - 1;          


            // Scan the boards to find those that support acquisition
            bool serverFound =  false;
            bool cameraFound =  false;
            for (serverIndex = 0; serverIndex < serverCount; serverIndex++)
            {
                if (SapManager.GetResourceCount(serverIndex, SapManager.ResourceType.Acq) != 0)
                {
                    string serverName = SapManager.GetServerName(serverIndex);
                    serverFound = true;
                }
                if (SapManager.GetResourceCount(serverIndex, SapManager.ResourceType.AcqDevice) != 0)
                {
                    string serverName = SapManager.GetServerName(serverIndex);
                    cameraFound = true;
                }
            }

            // At least one acquisition server must be available
            if (!serverFound && !cameraFound)
            {
                MessageBox.Show("No acquisition server found!");
                return  false;
            }


            int serverNum = 1; // char-to-int conversion     
            if ((serverNum >= 1) && (serverNum < serverCount))
            {
                acqParams.ServerName = SapManager.GetServerName(serverNum);
            }
            else
            {
                MessageBox.Show("Invalid selection!");
                return false;
            }
         
            // Scan all the acquisition devices on that server and show menu to user
            int deviceCount = SapManager.GetResourceCount(acqParams.ServerName, SapManager.ResourceType.Acq);
            int cameraCount = SapManager.GetResourceCount(acqParams.ServerName, SapManager.ResourceType.AcqDevice);
            int allDeviceCount = deviceCount + cameraCount;


           

            for (int deviceIndex = 0; deviceIndex < deviceCount; deviceIndex++)
            {
                string deviceName = SapManager.GetResourceName(acqParams.ServerName, SapManager.ResourceType.Acq, deviceIndex);

            }

            for (int cameraIndex = 0; cameraIndex < cameraCount; cameraIndex++)
            {
                string cameraName = SapManager.GetResourceName(acqParams.ServerName, SapManager.ResourceType.AcqDevice, cameraIndex);
      
            }


            int deviceNum = 1;
            if ((deviceNum >= 1) && (deviceNum <= allDeviceCount))
            {
                acqParams.ResourceIndex = deviceNum - 1;
            }
            else
            {
                MessageBox.Show("Invalid selection!");
                return  false;
            }
          
            ////////////////////////////////////////////////////////////

            // List all files in the config directory
            if (!Directory.Exists(configPath))
            {
                MessageBox.Show("文件不存在!");
                return false;
            }
            string[] ccffiles = Directory.GetFiles(configPath, "*.ccf");
            int configFileCount = ccffiles.Length;
            if (configFileCount == 0)
            {
                if (cameraCount == 0)
                {
                    return  false;
                }
                else
                {
                    configFileCount = 1;
                }
            }
            else
            {
               

                if (cameraCount == 0)
                {
                    if (configFileCount >= 2)
                    {
                        Console.Write(" to ");
                        Console.Write(configFileCount.ToString());
                    }
                    configFileCount = 0;

                }
                else
                {                  
                    configFileCount += 1;

                    configFileCount = 1;

                }
               
                int configFileShow = 0;
                foreach (string ccfFileName in ccffiles)
                {
                    string fileName = ccfFileName.Replace(configPath, "");
                    if (configFileCount < 9)
                    {
                        configFileShow = configFileCount + 1;

                    }
                    else
                    {
                        configFileShow = configFileCount - 9 + 'a';

                    }
                    configFileNames[configFileCount] = ccfFileName;
                    configFileCount++;
                }

            }
         

            int configNum = 1;
 
            if ((configNum >= 1) && (configNum <= configFileCount))
            {             
                acqParams.ConfigFileName = configFileNames[configNum - 1];
            }
            else
            {
                MessageBox.Show("Invalid selection!");
                return  false;
            }
           
            return true;
        }

        static public bool GetAcqDeviceOptionsFromCommandLine(string[] args, MyAcquisitionParams acparams)
        {
            // Check the command line for user commands
            if (args[1].Equals("/?") || args[1].Equals("-?"))
            {
                // print help
                Console.WriteLine("Usage:\n");
                Console.WriteLine("GigECameraLut [<acquisition server name> <acquisition device index>]\n");
                return false;
            }

            // Check if enough arguments were passed
            if (args.Length < 3)
            {
                Console.WriteLine("Invalid command line!\n");
                return false;
            }

            // Validate server name
            if (SapManager.GetServerIndex(args[1]) < 0)
            {
                Console.WriteLine("Invalid acquisition server name!\n");
                return false;
            }

            // Does the server support acquisition?
            int deviceCount = SapManager.GetResourceCount(args[1], SapManager.ResourceType.AcqDevice);
            if (deviceCount == 0)
            {
                Console.WriteLine("This server does not support acquisition!\n");
                return false;
            }

            // Validate device index
            if (int.Parse(args[2]) < 0 || int.Parse(args[2]) >= deviceCount)
            {
                Console.WriteLine("Invalid acquisition device index!\n");
                return false;
            }

            // Fill-in output variables
            acparams.ServerName = args[1];
            acparams.ResourceIndex = int.Parse(args[2]);

            return true;
        }


        static public bool GetCorAcqDeviceOptionsFromQuestions(MyAcquisitionParams acqParams)
        {
            int serverCount = SapManager.GetServerCount();
            int GenieIndex = 0;
            ArrayList listServerNames = new System.Collections.ArrayList();

            if (serverCount == 0)
            {
                Console.WriteLine("No device found!\n");
                return  false;
            }
            
            bool cameraFound =  false;
            for (int serverIndex = 0; serverIndex < serverCount; serverIndex++)
            {
                if (SapManager.GetResourceCount(serverIndex, SapManager.ResourceType.AcqDevice) != 0)
                {
                    string serverName = SapManager.GetServerName(serverIndex);
                    listServerNames.Add(serverName);
                    GenieIndex++;
                    cameraFound = true;
                }
            }

            // At least one acquisition server must be available
            if (!cameraFound)
            {
                Console.WriteLine("No GigE camera found!\n");
                return  false;
            }
            #if GRAB_CAMERA_LINK
            Console.WriteLine("\nNote:\nOnly CameraLink cameras will work with this example !\nBehavior is undefined for any other devices.\n");
            #endif
            Console.WriteLine("\nSelect one of the camera(s) detected: Press ");

            Console.Write("1");

            if (GenieIndex >= 2)
            {
                Console.Write(" to ");
                Console.Write(GenieIndex.ToString());
            }
            Console.Write(" or 'q' to quit.");
            int count = 1;
            foreach (string serverName in listServerNames)
            {
                Console.WriteLine("\n........................................");
                Console.WriteLine(Convert.ToString(count) + ": " + serverName);

                string deviceName = SapManager.GetResourceName(serverName, SapManager.ResourceType.AcqDevice, 0);
                Console.WriteLine("User defined Name : " + deviceName);
                Console.Write("........................................\n");
                count++;
            }
            
            ConsoleKeyInfo info = Console.ReadKey(true);
            char key = info.KeyChar;
            if (key == 'q')
                return  false;
            int serverNum = key - '0'; // char-to-int conversion
            if ((serverNum >= 1) && (serverNum <= GenieIndex))
            {   
                acqParams.ServerName = Convert.ToString(listServerNames[serverNum - 1]);
                acqParams.ResourceIndex = 0;
            }
            else
            {
                Console.WriteLine("Invalid selection!\n");
                return  false;
            }
      
            Console.WriteLine("\n");
            return true;
        }

        static public bool GetCorAcquisitionOptionsFromQuestions(MyAcquisitionParams acqParams)
        {

            // Get total number of boards in the system
            string[] configFileNames = new string[MAX_CONFIG_FILES];
            int serverCount = SapManager.GetServerCount();
            int GrabberIndex = 0;

            if (serverCount == 0)
            {
                Console.WriteLine("No device found!\n");
                return  false;
            }

            // Scan the boards to find those that support acquisition
            bool serverFound = false;
            for (int serverIndex = 0; serverIndex < serverCount; serverIndex++)
            {
                if (SapManager.GetResourceCount(serverIndex, SapManager.ResourceType.Acq) != 0)
                {
                    GrabberIndex++;
                    serverFound = true;
                }
            }

            // At least one acquisition server must be available
            if (!serverFound)
            {
                Console.WriteLine("No acquisition server found!\n");
                return false;
            }

            Console.Write("\nSelect the acquisition server: Press ");
            Console.Write("1");
            if (GrabberIndex >= 2)
            {
                Console.Write(" to ");
                Console.Write(GrabberIndex.ToString());
            }
            Console.Write(" or 'q' to quit.");
            Console.WriteLine("........................................");
            string serverName = "";
            for (int serverIndex = 0; serverIndex < serverCount; serverIndex++)
            {
                if (SapManager.GetResourceCount(serverIndex, SapManager.ResourceType.Acq) != 0)
                {
                    serverName = SapManager.GetServerName(serverIndex);
                    Console.WriteLine(serverIndex.ToString() + ": " + serverName);            
                }
            }
           
            ConsoleKeyInfo info = Console.ReadKey(true);
            char key = info.KeyChar;            
            if (key == 'q')
                return false;
            int serverNum = key - '0'; // char-to-int conversion     
            if ((serverNum >= 1) && (serverNum < serverCount))
            {
                acqParams.ServerName = SapManager.GetServerName(serverNum);
            }
            else
            {
                Console.WriteLine("Invalid selection!\n");
                return false;
            }     

            // Scan all the acquisition devices on that server and show menu to user
            int deviceCount = SapManager.GetResourceCount(acqParams.ServerName, SapManager.ResourceType.Acq);  

            #if GRAB_CAMERA_LINK
                Console.Write("\nSelect the device you wish to use on this server: Press ");
            #else
                Console.Write("\nSelect the acquisition device: Press ");
            #endif

                if (deviceCount >= 2)
            {
                Console.Write(" to ");
                Console.Write(deviceCount.ToString());
            }
            Console.Write(" or 'q' to quit.");
            Console.WriteLine("........................................");
            string deviceName = "";
            for (int deviceIndex = 0; deviceIndex < deviceCount; deviceIndex++)
            {
                deviceName = SapManager.GetResourceName(acqParams.ServerName, SapManager.ResourceType.Acq, deviceIndex);
                Console.WriteLine(((int)(deviceIndex + 1)).ToString() + ": " + deviceName);
            }
       
            info = Console.ReadKey(true);
            key = info.KeyChar;          
            if (key == 'q')
                return false;
            int deviceNum = key - '0';
            if ((deviceNum >= 1) && (deviceNum <= deviceCount))
            {
                acqParams.ResourceIndex = deviceNum - 1;
            }
            else
            {
                Console.WriteLine("Invalid selection!\n");
                return false;
            }
          
            ////////////////////////////////////////////////////////////

            // List all files in the config directory


            if (!Directory.Exists(configPath))
            {
                Console.WriteLine("Directory : {0} Does not exist", configPath);
                return false;
            }

            string[] ccffiles = Directory.GetFiles(configPath, "*.ccf");

            if (ccffiles.Length == 0)
            {
               Console.WriteLine("No config file found.\nUse CamExpert to generate a config file before running this example.\n");
               return false;
            }
            else
            {
                Console.Write("\nSelect the config file: Press ");
                Console.Write("1");
                if (ccffiles.Length >= 2)
                {
                    Console.Write(" to ");
                    Console.Write(ccffiles.Length.ToString());    
                }
                Console.Write(" or 'q' to quit.");
                Console.WriteLine("........................................");
                int configFileCount = 0;
                int configFileShow = 0;
                foreach (string ccfFileName in ccffiles)
                {
                    string fileName = ccfFileName.Replace(configPath, "");
                    if (configFileCount < 9)
                    {
                        configFileShow = configFileCount + 1;
                        Console.WriteLine(configFileShow.ToString() + ": " + fileName);
                    }
                    else
                    {
                        configFileShow = configFileCount - 9 + 'a';
                        Console.WriteLine(Convert.ToChar(configFileShow) + ": " + fileName);
                    }
                    configFileNames[configFileCount] = ccfFileName;
                    configFileCount++;
                }

                info = Console.ReadKey(true);
                key = info.KeyChar;
                if (key == 'q')
                    return false;
                int configNum = 0;
                // Use numbers 0 to 9, then lowercase letters if there are more than 10 files
                if (key >= '1' && key <= '9')
                    configNum = key - '0'; // char-to-int conversion
                else
                    configNum = key - 'a' + 10; // char-to-int conversion

                if ((configNum >= 1) && (configNum <= configFileCount))
                {
                    acqParams.ConfigFileName = configFileNames[configNum - 1];
                }
                else
                {
                    Console.WriteLine("\nInvalid selection!\n");
                    return false;
                }             
            }
            Console.WriteLine("\n");
            return true;
        }

        static bool IsMonoBuffer(SapBuffer pBuffers)
        {
            SapFormat format = pBuffers.Format;

            if (format == SapFormat.Uint8 || format == SapFormat.Int8 || format == SapFormat.Int16 || format == SapFormat.Uint16 ||
                   format==SapFormat.Int24 || format==SapFormat.Uint24 || format==SapFormat.Int32 || format==SapFormat.Uint32 
                    || format==SapFormat.Int64 || format==SapFormat.Uint64)
                return true;
            else
                return false;
        }

        static SapData SetDataValue(SapBuffer pBuffers, UInt32 pPrmIndex)
        {
	        if(IsMonoBuffer(pBuffers))
	        {
		        SapDataMono mono = new SapDataMono(128);
		        return mono;
	        }
	        else
	        {
		        SapDataRGB rgb = new SapDataRGB();
		        //SapDataRGB rgb(m_pInfoList->GetValueAt(*pPrmIndex), m_pInfoList->GetValueAt(*pPrmIndex+1), m_pInfoList->GetValueAt(*pPrmIndex+2));
		        pPrmIndex = pPrmIndex+3;
		        return rgb;
	        }
        }

        static public string GetLUTOptionsFromQuestions(SapBuffer pBuffers, SapLut pLut)
        {
           //////// Ask questions to user to select LUT mode ////////
	       UInt32 prmIndex = 1;
	       string acqLutFileName= "";
           string chAcqLutName = "";

           while (string.IsNullOrEmpty(chAcqLutName))
           {
                Console.WriteLine("\nSelect the LookUpTable mode you want to apply: \n");

               Console.WriteLine("a : Normal mode");
               Console.WriteLine("b : Arithmetic mode");
               Console.WriteLine("c : Binary mode");
               Console.WriteLine("d : Boolean mode");
               Console.WriteLine("e : Gamma mode");
               Console.WriteLine("f : Reverse mode");
               Console.WriteLine("g : Roll mode");
               Console.WriteLine("h : Shift mode");
               Console.WriteLine("i : Slope mode");
               Console.WriteLine("j : Threshold single mode");
               Console.WriteLine("k : Threshold double mode");
                
               ConsoleKeyInfo info = Console.ReadKey(true);
               char key = info.KeyChar;	        
               switch (key)
	           {
		            case 'a':
		            {
			            pLut.Normal();
                        acqLutFileName = "Normal_Lut_Mode.lut";
                        chAcqLutName = "Normal Lut";
			            break;
		            }
		            case 'b':
		            {
			            int operationMode = 0;//Linear plus offset with clip
			            /*
				            Others operations available
			            */
			            //int operation = 1;//Linear minus offset(absolute)
			            //int operation = 2;//Linear minus offset(with clip)
			            //int operation = 3;//Linear with lower clip
			            //int operation = 4;//Linear with upper clip
			            //int operation = 5;//Scale to maximum limit
						
			            SapData offSet;
			            offSet = SetDataValue(pBuffers, prmIndex);
			            pLut.Arithmetic((SapLut.ArithmeticOp)operationMode, offSet);
                        acqLutFileName = "Arithmetic_Lut_Mode.lut";
                        chAcqLutName = "Arithmetic Lut"; 
			            break;
		            }
		            case 'c':
		            {
			            SapData clipValue;
			            clipValue = SetDataValue(pBuffers, prmIndex);
			            pLut.BinaryPattern(0, clipValue);
                        acqLutFileName = "Binary_Lut_Mode.lut";
                        chAcqLutName = "Binary Lut";
			            break;
		            }
		            case 'd':
		            {
			            SapData booleanFunction;
			            booleanFunction = SetDataValue(pBuffers, prmIndex);
			            pLut.Boolean((SapLut.BooleanOp)0, booleanFunction);
			            /*
				            Others operations available
			            */
			            // AND
			            //pLut->Boolean((SapLut::BooleanOp)1, booleanFunction);
			            // OR
			            //pLut->Boolean((SapLut::BooleanOp)2, booleanFunction);
			            // XOR
                        acqLutFileName = "Boolean_Lut_Mode.lut";
                        chAcqLutName = "Boolean Lut";
			            break;
		            }
		            case 'e':
		            {
			            int gammaFactor = (int)(2*GAMMA_FACTOR);
			            pLut.Gamma((float)gammaFactor/GAMMA_FACTOR);
			            acqLutFileName = "Gamma_Lut_Mode.lut";
                        chAcqLutName = "Gamma Lut";
			            break;
		            }
		            case 'f':
		            {
			            pLut.Reverse();
			            acqLutFileName =  "Reverse_Lut_Mode.lut";
                        chAcqLutName = "Reverse Lut";
			            break;
		            }
		            case 'g':
		            {
			            int numEntries = 128;
			            pLut.Roll(numEntries);
                        acqLutFileName = "Roll_Lut_Mode.lut";
                        chAcqLutName ="Roll Lut";
			            break;
		            }
		            case 'h':
		            {
			            int bitsToShift = 3;
			            pLut.Shift(bitsToShift);
                        acqLutFileName = "Shift_Lut_Mode.lut";
                        chAcqLutName ="Shift Lut";
			            break;
		            }
		            case 'i':
		            {
			            int startIndex1 = 76;
			            int endIndex1 = 179;
			            bool clipOutSide = false;//TRUE
			            SapData minValue;
			            SapData maxValue;
			            minValue = SetDataValue(pBuffers, prmIndex);
			            maxValue = SetDataValue(pBuffers, prmIndex);
			            pLut.Slope(startIndex1, endIndex1, minValue, maxValue, clipOutSide);
			            acqLutFileName = "Slope_With_Range_Lut_Mode.lut";
                        chAcqLutName = "Slope With Range Lut";
			            break;
		            }
		            case 'j':
		            {
			            SapData treshValue;
			            treshValue = SetDataValue(pBuffers, prmIndex);
			            pLut.Threshold(treshValue);
                        acqLutFileName = "Threshold_Single_Mode.lut";
                        chAcqLutName = "Threshold Single Lut";
			            break;
		            }
		            case 'k':
		            {
			            SapData treshValue1;
			            SapData treshValue2;
			            treshValue1 = SetDataValue(pBuffers, prmIndex);
			            treshValue2 = SetDataValue(pBuffers,  prmIndex);
			            pLut.Threshold(treshValue1, treshValue2);
                        acqLutFileName = "Threshold_Double_Mode.lut";
                        chAcqLutName = "Threshold Double Lut";
			            break;
		            }
                    default :
                    {
                    Console.WriteLine("\nInvalid selection!\n");
                    break;
                    }
	          }               
           }
	       pLut.Save(acqLutFileName);		// Save LUT to file (can be reloaded in the main demo)
           Console.WriteLine("\n");
           return chAcqLutName;
        }

        static public bool GetLoadLUTFiles(SapLut pLut, ArrayList FileSave_list)
        {
           //////// Ask questions to user to select LUT file ////////
	        string fileName = "";
            Console.WriteLine("\nDo you want to load an existing LUT file? y/n (Yes/No)  \n");
	        ConsoleKeyInfo keyQuestion = Console.ReadKey(true);
            char key = keyQuestion.KeyChar;          
            if (key == 'n')
	        {					       
		        return false;
	        }
            else if (key == 'y')
            {
               Console.WriteLine("\nSelect the LUT file available: \n");
               GetLUTFilesSaved(".", FileSave_list);
               if (FileSave_list.Count != 0)
               {
                   keyQuestion = Console.ReadKey(true);
                   key = keyQuestion.KeyChar;
                
                       int fileNum = key - '0';
                       if ((fileNum >= 1) && (fileNum <= FileSave_list.Count))
                       {
                           fileName = (string)FileSave_list[fileNum - 1];
                           // Load LUT (saved before in the main demo)
                           if (pLut.Load(fileName))
                           {
                               Console.WriteLine("\n................");
                               Console.WriteLine(fileName + " loaded.");
                               Console.WriteLine("..................\n");
                               return true;
                           }
                       }
                       else
                       {
                           Console.WriteLine("\nInvalid selection!\n");
                           return false;
                       }
                   }
                   else
                   {
                       Console.WriteLine("\nInvalid selection!\n");
                       return false;
                   }
           }
           else
           {
               Console.WriteLine("\nNo Lut File Avaible. Please load an existing one\n");
               return false;
           }        
           return false;          
        }

        static void GetLUTFilesSaved(String Path, ArrayList FileSave_list)
        {
           
            string[] lutfiles = Directory.GetFiles(Path, "*.lut");
            int configFileCount = 1;

            foreach (string lutFileName in lutfiles)
            {
                FileSave_list.Add(lutFileName);
                Console.WriteLine(configFileCount.ToString() + ": " + lutFileName);
                configFileCount++;
            }
        }
    }

    public class MyAcquisitionParams
    {
        public MyAcquisitionParams()
        {
            m_ServerName = "";
            m_ResourceIndex = 0;
            m_ConfigFileName = "";
        }
        
        public MyAcquisitionParams(string ServerName, int ResourceIndex)
        {
            m_ServerName = ServerName;
            m_ResourceIndex = ResourceIndex;
            m_ConfigFileName = "";
        }

        public MyAcquisitionParams(string ServerName, int ResourceIndex, string ConfigFileName)
        {
            m_ServerName = ServerName;
            m_ResourceIndex = ResourceIndex;
            m_ConfigFileName = ConfigFileName;
        }

        public string ConfigFileName
        {
            get { return m_ConfigFileName; }
            set { m_ConfigFileName = value; }
        }

        public string ServerName
        {
            get { return m_ServerName; }
            set { m_ServerName = value; }
        }

        public int ResourceIndex
        {
            get { return m_ResourceIndex; }
            set { m_ResourceIndex = value; }
        }

        protected string m_ServerName;
        protected int m_ResourceIndex;
        protected string m_ConfigFileName;
    }

}