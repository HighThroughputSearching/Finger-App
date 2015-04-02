<%@ WebService Language="C#" Class="FingerprintApp.webservice" %>
using System;
using System.Web.Services;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using log4net;

namespace FingerprintApp
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
       	//	p.loadCandidateDB();
       		p.identifyPersonMatch("admin@admin.com");
			
			stopwatch.Stop ();
			logger.Debug("Elapsed Time :"+ stopwatch.Elapsed);

		   string json = JsonConvert.SerializeObject(p.getPersonMatch(), Formatting.Indented);
           return json;
        }


        [WebMethod]
        public string Load_AFingerScore(string id, string ProbeArray)
        {
        	log4net.Config.XmlConfigurator.Configure();
	        logger.Debug("++++++++++++++  A finger ++++++++++++++");
         
         	
           logger.Debug("id : "+id );
            logger.Debug(ProbeArray);    
          
            FingerScore fingerScore = new FingerScore();
             fingerScore.verfiyAFingerScore(id, ProbeArray);
            
//        	float score = fingerScore.getScores();
 			string json = JsonConvert.SerializeObject(fingerScore.getFingerInfo(), Formatting.Indented);
        
            return json;
            
          
        }

        [WebMethod]
        public void AddNewFingerPrints(int person_id,string organisation)
        {
            Program p = new Program();
            p.addPersonDB(person_id,organisation);
         
        }
	}
}

