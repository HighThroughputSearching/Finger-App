<%@ WebService Language="C#" Class="WebFinger1.webservice" %>
using System;
using System.Web.Services;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using log4net;

namespace WebFinger1
{
	class webservice : System.Web.Services.WebService
	{
		ILog logger = LogManager.GetLogger("RollingLogFileAppender");  

		[WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string Load_PersonScore(string emailUser)
        {
        	log4net.Config.XmlConfigurator.Configure();
            Stopwatch stopwatch = new Stopwatch ();
			stopwatch.Start ();
       		Program p = new Program();
       		//p.loadCandidateDB();
       		p.identifyPersonMatch("admin@admin.com");
			
			stopwatch.Stop ();
			logger.Debug("Elapsed Time :"+ stopwatch.Elapsed);

		   string json = JsonConvert.SerializeObject(p.getPersonMatch(), Formatting.Indented);
           return json;
        }


        [WebMethod]
        public float Load_AFingerScore(string FingerDBArray, string ProbeArray)
        {
        	log4net.Config.XmlConfigurator.Configure();
	        logger.Debug("++++++++++++++  A finger ++++++++++++++");
         
         	
            logger.Debug(FingerDBArray);
            logger.Debug(ProbeArray);    
          
            FingerScore fingerScore = new FingerScore();
             fingerScore.verfiyAFingerScore(FingerDBArray, ProbeArray);
            
        	float score = fingerScore.getScores();
        
            return score;
            
          
        }

        [WebMethod]
        public void AddNewFingerPrints(int person_id)
        {
            Program p = new Program();
            p.addPersonDB(person_id);
         
        }
	}
}

